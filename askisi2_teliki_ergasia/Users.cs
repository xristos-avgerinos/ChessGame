using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace askisi2_teliki_ergasia
{
    //εχουμε φτιαξει μια κλαση Users ωστε οι δυο παικτες να δημιουργουνται ως αντικειμενα της
    public class Users
    {
        //ολες οι ιδιοτητες(properties) ειναι public ωστε να εχουν προσβαση οι φορμες 
        public List<Point> Capture = new List<Point>(); //λιστα που θα περιεχει ολα τα points(x,y) δηλαδη τα σημεια στα οποια θα μεταφερεται το καθε πιονι οταν θα γινεται capture δεξια η αριστερα αναλογα αν εχει τα μαυρα η ασπρα πιονια
        public List<PictureBox> Pieces = new List<PictureBox>();//λιστα που θα περιεχει ολα τα points(x,y) δηλαδη ολα τα κελια των μαυρων η ασπρων πιονιων αναλογα το χρωμα που θα εχει ο καθε χρηστης
        
        public string Username;
        public int CountDown;//μεταβλητη για το χρονο που απομενει στον καθε user
        public string Pieces_color;//το χρωμα των πιονιων του παικτη(user)
        public bool turn;//η σειρα του παικτη ως boolean μεταβλτητη
        public static string DateTime;//η ημερομηνια και ωρα που αρχιζει η παρτιδα σκακι.Ειναι static μεταβλητη αφου για καθε instance της κλασης Users ειναι ιδια δηκλαδη ανηκει στη κλαση

        public Users(string Username, int CountDown, string Pieces_color)//δημιουργια constuctor ετσι ωστε καθε user να εχει υποχρεωτικα username,countdown και pieces color οταν δημιουργειται
        {
            this.Username = Username;
            this.CountDown = CountDown;
            this.Pieces_color = Pieces_color;
        }

    }
}
