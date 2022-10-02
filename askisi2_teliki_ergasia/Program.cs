using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace askisi2_teliki_ergasia
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
            if (Users.DateTime != null)
            {
                String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
                OleDbConnection connection = new OleDbConnection(connectionString);
                connection.Open();
                String insertQuery = "Insert into chess_stats(User1, Pieces_User1, User2, Pieces_User2, Duration, DTime) values(@User1,@Pieces_User1,@User2,@Pieces_User2,@Duration,@DTime)";
                OleDbCommand command = new OleDbCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@User1", Form1.User1.Username);
                command.Parameters.AddWithValue("@Pieces_User1", Form1.User1.Pieces_color);
                command.Parameters.AddWithValue("@User2", Form1.User2.Username);
                command.Parameters.AddWithValue("@Pieces_User2", Form1.User2.Pieces_color);
                TimeSpan t = TimeSpan.FromSeconds(1200 - Form1.User1.CountDown + 1200 - Form1.User2.CountDown);
                command.Parameters.AddWithValue("@Duration", t.Minutes.ToString() + " minutes and " + t.Seconds.ToString() + " seconds");
                command.Parameters.AddWithValue("@DTime", Users.DateTime);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
