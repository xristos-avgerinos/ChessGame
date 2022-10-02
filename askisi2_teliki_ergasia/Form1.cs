using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace askisi2_teliki_ergasia
{
    public partial class Form1 : Form
    {
        private Point point;//δηλωση ενος point που θα ειναι το location που βρισκεται το ποντικι τη στιγμη που καποιος παικτης θα κανει κλικ(mouse down) πανω σε ενα picturebox.Το location αυτο ειναι για τη περιοχη του τετραγωνου δηλ του picturebox που ειναι συνολικα 77x77 και οχι στη περιοχη της φορμας 
        private Point mouseDown_point;//δηλωση ενος point που θα ειναι το location που βρισκεται ενα picturebox(δηλαδη ενα πιονι) τη στιγμη που καποιος παικτης θα κανει κλικ(mouse down) πανω του 

        bool move;//δηλωση boolean μεταβλητης που μας βοηθαει να ξερουμε αν εχει το δικαιωμα να κινηθει ενα πιονι
        List<Point> Points_list = new List<Point>();//δημιουργια λιστας που θα περιεχει και τα 64 σημεια-τετραγωνα των πιονιων στη σκακιερα

        //δηλωση των δυο αντικειμενων(παικτων) user1 και user2 της κλασης Users.Ειναι public static για να εχει προσβαση ο program.cs
        public static Users User1;
        public static Users User2;

        public Form1(string username1, string username2, string Pieces_color1, string Pieces_color2) //δημιουργια constructor με παραμετρους τα δυο usernames και τα πιονια που επελεξε ο καθε παικτης
        {
            InitializeComponent();
            //instantiation
            User1 = new Users(username1, 1200, Pieces_color1); 
            User2 = new Users(username2, 1200, Pieces_color2);
        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            mouseDown_point = box.Location; 

            point = e.Location;
            move = true;//μπορει να μετακινηθει το πιονι(picturebox)
        }

        double distance(Point a, Point b)//συναρτηση που υπολογιζει τη μαθηματικη διαφορα δυο σημειων
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox box = (PictureBox)sender;

            //εδω βρισκουμε ποιο τετραγωνο της σκακιερας εχει τη πιο κοντινη αποσταση απο το σημειο που εκανε ο χρηστης το mouse up για ενα picturebox.Αφου βρουμε το κοντινοτερο σημειο της 
            //σκακιερας τοτε το συγκεκριμενο picture box μεταφερεται σε αυτο το σημειο ωστε να "κουμπωνει" ακριβως στο τετραγωνο το πιονι(picturebox)
            Point pb = box.Location;
            double min_dist = float.MaxValue;
            Point closestPoint = new Point(0, 0);//αρχικοποιηση του κοντινοτερου σημειου με (0,0)
            foreach (Point p in Points_list)
            {
                double d = distance(p, pb);
                if (d < min_dist)
                {
                    min_dist = d;
                    closestPoint = p;
                }
            }
            box.Location = closestPoint;//μεταφερεται το πιονι στο κοντινοτερο τετραγωνο απο το σημειο που το αφηνει ο χρηστης πανω στη σκακιερα

            move = false; //σταματαμε τη μετακινηση(picturebox_MouseMove) του πιονιου
           
            if (closestPoint != mouseDown_point)//αν αλλαξε location το πιονι δηλαδη αν δν βρισκεται πλεον στη θεση ππυ εγινε το mouse down για το picturebox του τοτε:
            {
                Form2.snd.Move_Sound();//ακουγεται ηχος μετακινησης πιονιου
                if (User1.turn)//αν η σειρα ηταν του user1 τοτε αλλαζουμε σειρα σε user2
                {
                    User1.turn = false;
                    label3.BackColor = Color.Transparent;//ξεκιτρινιζουμε το label3 που αναφερεται στον user1 ωστε να δειξουμε οτι τελειωσε η σειρα του
                    timer1.Stop();//σταματαει ο timer του user1
                    timer2.Start();//και συνεχιζει να μετραει ο timer του user2
                    User2.turn = true;
                    label2.BackColor = Color.Yellow;//κιτρινιζουμε το label2 που αναφερεται στον user2 ωστε να δειξουμε οτι ειναι η σειρα του
                }
                else if (User2.turn)//αν η σειρα ηταν του user2 τοτε αλλαζουμε σειρα σε user1
                {
                    User2.turn = false;
                    label2.BackColor = Color.Transparent;//ξεκιτρινιζουμε το label2 που αναφερεται στον user2 ωστε να δειξουμε οτι τελειωσε η σειρα του
                    timer2.Stop();//σταματαει ο timer του user2
                    timer1.Start();//και συνεχιζει να μετραει ο timer του user1
                    User1.turn = true;
                    label3.BackColor = Color.Yellow;//κιτρινιζουμε το label3 που αναφερεται στον user1 ωστε να δειξουμε οτι ειναι η σειρα του
                }
                
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            PictureBox box = (PictureBox)sender;

            if (move && User1.turn)//αν ειναι η σειρα του user1 και μπορει να μετακινηθει το πιονι που εχει πατηθει τοτε:
            {
                if (User1.Pieces.Contains(box))//αν ο user1 περιεχει στη λιστα με τα πιονια του το συγκεκριμενο πιονι του(picturebox) τοτε μετακινει το πιονι κανονικα μεσα στη φορμα μεχρι να γινει mouse up
                    box.Location = new Point(box.Left + e.X - point.X, box.Top + e.Y - point.Y);
            }
            else if (move && User2.turn)//αντιστοιχα αν ειναι η σειρα του user2 και μπορει να μετακινηθει το πιονι που εχει πατηθει τοτε:
            {
                if (User2.Pieces.Contains(box))//αν ο user2 περιεχει στη λιστα με τα πιονια του το συγκεκριμενο πιονι του(picturebox) τοτε μετακινει το πιονι κανονικα μεσα στη φορμα μεχρι να γινει mouse up
                    box.Location = new Point(box.Left + e.X - point.X, box.Top + e.Y - point.Y);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1083, 800);//εκκινουμε τη φορμα με το συγκεκριμενο size ωστε να μην εχουμε προβληματα αν εκκινηθει με αλλο size

            if (User1.Pieces_color.Equals("WHITE PIECES"))//αν ο user1 εχει τα ασπρα πιονια τοτε αποθηκευουμε ολα τα ασπρα πιονια(και τα 16) στη λιστα με τα πιονια του.Αντιστοιχα και για τον user2 με τα μαυρα πιονια 
            {
                int count = 1;
                foreach ( PictureBox pictureBox in Controls.OfType<PictureBox>())//ξερουμε το index απο καθε control οποτε με ενα loop κανουμε ευκολα την αποθηκευση χωρις να προσθεσουμε ενα ενα τα πιονια στη λιστα
                {
                    if ((count >= 3 && count <= 9) || (count >= 26 && count <= 34))
                    {
                        User1.Pieces.Add(pictureBox);
                    }
                    else if (count >= 10 && count <= 25)
                    {
                        User2.Pieces.Add(pictureBox);
                    }
                    count++;
                }
            }
            else //διαφορετικα αν ο user2 εχει τα ασπρα πιονια τοτε αποθηκευουμε ολα τα ασπρα πιονια(και τα 16) στη λιστα με τα πιονια του.Αντιστοιχα και για τον user1 με τα μαυρα πιονια 
            {
                int count = 1;
                foreach (PictureBox pictureBox in Controls.OfType<PictureBox>())
                {
                    if ((count >= 3 && count <= 9) || (count >= 26 && count <= 34))
                    {
                        User2.Pieces.Add(pictureBox);
                    }
                    else if (count >= 10 && count <= 25)
                    {
                        User1.Pieces.Add(pictureBox);
                    }
                    count++;
                }
            }
            /*int h = 1;
            foreach (var pictureBox in Controls.OfType<PictureBox>())
            {
                MessageBox.Show(pictureBox.Name + "   " + h.ToString());
                h++;
            }*/
            label5.Text = User1.Username;
            label4.Text = User2.Username;
            label9.Text = User1.Pieces_color;
            label6.Text = User2.Pieces_color;


            for (int k = 144; k <= 683; k += 77)
            {
                for (int l = 0; l <= 77; l += 77)
                {
                    User1.Capture.Add(new Point(l, k)); //αποθηκευουμε στη λιστα capture(που θα περιεχει ολα τα σημεια στην αριστερη μερια της σκακιερας οταν θα γινεται ενα capture απο τα πιονια του user1) ολα τα points 
                }
            }

            for (int a = 144; a <= 683; a += 77)
            {
                for (int b = 909; b <= 986; b += 77)
                {
                    User2.Capture.Add(new Point(b, a));
                }
            }


            for (int i = 611; i >= 72; i -= 77)
            {
                for (int j = 224; j <= 763; j += 77)
                {
                    Points_list.Add(new Point(j, i));//αποθηκευουμε στη λιστα capture(που θα περιεχει ολα τα σημεια στην αριστερη μερια της σκακιερας οταν θα γινεται ενα capture απο τα πιονια του user2) ολα τα points 
                }
            }
        }

        private void deToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ToolStripItem item = (ToolStripItem)sender;
            ContextMenuStrip owner = (ContextMenuStrip)item.Owner;
            Control usedControl = owner.SourceControl;//κανουμε ενα casting μεχρι να φτασουμε στο συγκεκριμενο κοντρολ δηλαδη το picturebox(πιονι) που εγινε δεξι κλικ και μετα capture
            usedControl.Location = User1.Capture.ElementAt(0);//το μετακινουμε το πιονι σε μια θεση στο αριστερο μερος της σκακιερας δηλαδη το μερος οπου πανε τα "φαγωμενα" πιονια του user1.Η θεση που παιρνει απο τα σημεια της λιστας capture ειναι η πρωτη  
            User1.Capture.RemoveAt(0);//στη συνεχεια αφαιρουμε το πρωτο στοιχειο της λιστας capture ωστε την επομενη φορα που θα γινει capture να παει στη δευτερη θεση που πανε τα "φαγωμενα" πιονια
            usedControl.Enabled = false;//κανουμε disabled το πιονι που "φαγωθηκε"
            Form2.snd.Capture_Sound();//ακουγεται ο ηχος οταν "τρωγεται" ενα πιονι
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Ιδια ακριβως διαδικασια με το πρωτο menustrip αλλα αυτη τη φορα απευθυνεται στον user2
            ToolStripItem item = (ToolStripItem)sender;
            ContextMenuStrip owner = (ContextMenuStrip)item.Owner;
            Control usedControl = owner.SourceControl;
            usedControl.Location = User2.Capture.ElementAt(0);
            usedControl.Enabled = false;
            User2.Capture.RemoveAt(0);
            Form2.snd.Capture_Sound();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            User1.CountDown--;//σε καθε δευτερολεπτο μειωνουμε τον countdown του user1 οταν ο timer του τρεχει
            TimeSpan t = TimeSpan.FromSeconds(User1.CountDown);//μετατρεπουμε τον countdown σε μεταβλητη τυπου Timespan δηλαδη αναπαριστα χρονο
            label8.Text = t.Minutes.ToString() + ":" + t.Seconds.ToString();//το label8 αναπαριστα το χρονο που απομενει σε λεπτα και δευτερολεπτα για τον user1
            if (User1.CountDown == 0)//αν τελειωσει ο χρονος του user1 κανουμε disabled τους δυο timers και εμφανιζουμε μηνυμα οτι ο user2 ειναι ο νικητης της παρτιδας
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                MessageBox.Show(User2.Username + " is the winner of the game.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //οταν πατηθει αυτο το κουμπι ξεκιναει η παρτιδα 
            button1.Hide();//κρυβουμε το κουμπι
            Users.DateTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();//η στατικη μεταβλητη DateTime της κλασης Users παιρνει την ημερομηνια που ξεκιναει η παρτιδα
            timer1.Start();//ξεκιναει να παιζει ο user1
            User1.turn = true;
            label3.BackColor = Color.Yellow;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //ισχυει το ιδιο με τον timer1
            User2.CountDown--;
            TimeSpan t = TimeSpan.FromSeconds(User2.CountDown);
            label7.Text = t.Minutes.ToString() + ":" + t.Seconds.ToString();
            if (User2.CountDown == 0)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                MessageBox.Show(User1.Username + " is the winner of the game.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Με αυτο το κουμπι σταματαμε και συνεχιζουμε τη background music
            if (button4.Text.Equals("🔊"))
            {
                Form2.snd.PauseMusic();
                button4.Text = "🔇";
            }
            else
            {
                Form2.snd.PlayMusic();
                button4.Text = "🔊";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //επιστρεφουμε στη Form2
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }
    }
}
