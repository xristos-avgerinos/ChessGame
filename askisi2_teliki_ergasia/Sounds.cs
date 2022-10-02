using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;


namespace askisi2_teliki_ergasia
{
    //δημιουργια κλασης sound που θα περιεχει ολες τις μεθοδους που εχουν να κανουν με τους ηχους που θα ακουγονται στην εφαρμογη
    public class Sounds
    {
        AxWMPLib.AxWindowsMediaPlayer mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
        
        public void BackGround_Music()//μεθοδος που ενεργοποιει την μουσικη που θα ακουγεται οταν ξεκιναει η εφαρμογη
        {
            mediaPlayer.CreateControl();//δημιουργουμε το control
            mediaPlayer.URL = "BackGroundMusic.wav";
            mediaPlayer.settings.setMode("loop", true);//επαναλαμβανεται η μουσικη
            mediaPlayer.settings.volume = 25;
        }
        public void PauseMusic()//μεθοδος που σταματαει(pause) τη μουσικη
        {
            mediaPlayer.Ctlcontrols.pause();
        }
        public void PlayMusic()//μεθοδος που ξανα ενεργοποιει τη μουσικη
        {
            mediaPlayer.Ctlcontrols.play();
        }
       
        public void Capture_Sound()//μεθοδος για τον ηχο που θα ακουγεται οταν θα "τρωμε" ενα πιονι
        {
            SoundPlayer player = new SoundPlayer("Capture.wav");
            player.Play();
        }
        public void Move_Sound()//μεθοδος για τον ηχο που θα ακουγεται οταν θα μετακινουμε ενα πιονι
        {
            SoundPlayer player = new SoundPlayer("Move.wav");
            player.Play();
        }
        public void say_goodLuck()//μεθοδος που χρησιμοποιει εναν SpeechSynthesizer ωστε να λεει καλη επιτυχια οταν δωσουν τα στοιχεια τους οι δυο παικτες και ειναι ετοιμο να ξεκινησει το παιχνιδι 
        {
            SpeechSynthesizer engine = new SpeechSynthesizer();
            engine.SpeakAsync("Good luck to both of you");
        }
    }
}
