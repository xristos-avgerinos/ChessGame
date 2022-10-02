using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace askisi2_teliki_ergasia
{
    public partial class Form2 : Form
    {
        public static Sounds snd = new Sounds();//δημιουργια αντικειμενου της κλασης sounds.Ειναι public και static ωστε να εχει προσβαση και η Form1(να χρησιμοποιει τις μεθοδους της για να κανει pause play κλπ)
        public Form2()
        {
            InitializeComponent();
        }
     
        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //με 2 checkedListBoxes επιλεγει ο καθε παικτης το χρωμα που θελει για πιονια και μεσω αυτου του event ελεγχουμε οτι και οι δυο πηραν διαφορετικα χρωματα
            CheckedListBox box = (CheckedListBox)sender;
            CheckedListBox other;
            if (box.Name == "checkedListBox1")
            {
                other = checkedListBox2;
            }
            else {
                other = checkedListBox1;
            }
            
            if (1 == e.Index)
            {
                box.SetItemChecked(0, false);
                other.SetItemChecked(1, false);              
            }
            else if (0 == e.Index)
            {
                box.SetItemChecked(1, false);
                other.SetItemChecked(0, false);                
            }
           
        }
        
        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //οταν οι δυο παικτες πατησουν το κουμπι lets play ελεγχουμε οτι εχουν δωσει usernames και εχουν επιλεξει πιονια.Επισης ελεγχουμε οτι τα usernames δεν ειναι κενα,δεν περιεχουν κενα και ειναι πανω απο 4 χαρακτηρες.
            if (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text.Contains(" ") || string.IsNullOrWhiteSpace(textBox2.Text) || textBox2.Text.Contains(" "))
            {
                MessageBox.Show("Username of user 1 and user 2 cannot be null or contain white spaces");
            }
            else
            {
                if ((textBox1.Text.Length < 5) || (textBox2.Text.Length < 5))
                {
                    MessageBox.Show("Username of user 1 and user 2 must be at least 5 characters.");
                }
                else
                { 
                    if((checkedListBox1.CheckedItems.Count == 0)||(checkedListBox2.CheckedItems.Count == 0)){
                        MessageBox.Show("Please select black or white pieces for both user 1 and user 2");
                    }
                    else
                    {
                        //αν δεν υπαρχει κανενα προβλημα στους ελεγχους λεμε καλη τυχη και στους δυο και προχωραμε στη Form1 δινοντας της ως παραμετρους στον constuctor τα usernames και τα πιονια που επελεξαν οι παικτες
                        snd.say_goodLuck();
                        this.Hide();
                        Form1 form1 = new Form1(textBox1.Text,textBox2.Text,checkedListBox1.CheckedItems[0].ToString(),checkedListBox2.CheckedItems[0].ToString());
                        form1.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            snd.BackGround_Music();//στην φορτωση της φορμας 2 ξεκιναμε και την background μουσικη 
        }
    }
}
