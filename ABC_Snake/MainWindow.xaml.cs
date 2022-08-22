using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ABC_Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //Timer
        DispatcherTimer timer = new DispatcherTimer(); //Timer namens timer wird erstellt

        //Objekt
        public Quadrat Koerperteil = new Quadrat(); //Objekt wird aus der Klasse Quadrat erstellt

        //Kooardinaten
        public Point _startPunkt = new Point(5, 5); //Startkoordinate bei 5X und 5Y
        public Point _aktuellePos = new Point(); //Aktuelle Koordinate der Snake

        
        //Sounds
        SoundPlayer deathSound = new SoundPlayer(@"C:\Users\gereo\Documents\ProgrammeC#\ABC_Snake\ABC_Snake_Game\Sounds\missionfailed.wav"); //Soundfile für SnakeOver
        SoundPlayer foodSound = new SoundPlayer(@"C:\Users\gereo\Documents\ProgrammeC#\ABC_Snake\ABC_Snake_Game\Sounds\munch.wav"); //Soundfile für CheckCollision

        public int X; //X-Koordinate auf dem Spielfeld
        public int Y; //Y-Koordinate auf dem Spielfeld
        public int score = 0; //Integer namens Score wird erstellt
        public int richtung = 0; //Integer namens richtung wird erstellt
        private int vorhaerigeRichtung = 0; //Integer namens vorhaerigeRichtung wird erstellt

        //Food
        public Rectangle blockFood = new Rectangle(); //Foodobjekt wird erstellt
        public Random rnd = new Random();   //Random Zahlgenerator namens rnd wird erstellt
        public List<Point> _foodPoint = new List<Point>(); //Point Liste für die rnd Koordinate des Foods
        public int Breite = 10; //Breite des Foods
        public int Hoehe = 10; //Höhe des Foods
        public Color Farbe = Colors.Red; //Farbe des Foods

        public MainWindow() //Konstruktor (haben keinen Rückgabewert)
        {

            InitializeComponent();

            for (int n = 0; n < 1; n++) //Solange n kleiner als 1 ist wird folgenes ausgeführt
            {
                ZeichnenFood(n); //Methode ZeichnenFood wird ausgeführt
            }

            Koerperteil.Zeichnen(Spielfeld, _startPunkt); //Snake wird auf dem Canvas, auf der aktuellen Koordinate gezeichnet
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Animation; //Methode Animation wird kontinuierlich abgefragt
            timer.Tick += Check; //Methode Check wird kontinuierlich abgefragt
            timer.Start(); //Timer startet bei Start der Anwendung
            _aktuellePos = _startPunkt; //Aktuelle Position mit Startpunkt gleich setzten

        }


        private enum Bewgungsrichtung // Aufzählung
        {
            ObenW = 4, //Mit Werten gesetz weil sonst automatisch ObenW = 0 wäre und sofort ausgeführt werden würde
            UntenS = 3,
            LinksA = 2,
            RechtsD = 1
        };

        void Check(object sender, EventArgs e)
        {
            CheckCollision(); //Methode CheckCollision wird aufgerufen
            labelScore.Content = "" + score; //Aktueller Score wird im Label angezeigt

        }

        void Animation(object sender, EventArgs e)
        {

            switch (richtung)
            {
                case (int)Bewgungsrichtung.UntenS:
                    _aktuellePos.Y -= 5; //Aktuelle Y-Koordinate wird um 5 subtrahiert
                    Koerperteil.Zeichnen(Spielfeld, _aktuellePos); //Snake wird auf Canvas(Spielfeld) und der aktuellen Koordinate gezichnet
                    break; //Springt aus dem switch statement
                case (int)Bewgungsrichtung.ObenW:
                    _aktuellePos.Y += 5; //Aktuelle Y-Koordinate wird um 5 addiert
                    Koerperteil.Zeichnen(Spielfeld, _aktuellePos); //Snake wird auf Canvas(Spielfeld) und der aktuellen Koordinate gezichnet
                    break; //Springt aus dem switch statement
                case (int)Bewgungsrichtung.RechtsD:
                    _aktuellePos.X += 5; //Aktuelle X-Koordinate wird um 5 addiert
                    Koerperteil.Zeichnen(Spielfeld, _aktuellePos); //Snake wird auf Canvas(Spielfeld) und der aktuellen Koordinate gezichnet
                    break; //Springt aus dem switch statement
                case (int)Bewgungsrichtung.LinksA:
                    _aktuellePos.X -= 5; //Aktuelle X-Koordinate wird um 5 subtrahiert
                    Koerperteil.Zeichnen(Spielfeld, _aktuellePos); //Snake wird auf Canvas(Spielfeld) und der aktuellen Koordinate gezichnet
                    break; //Springt aus dem switch statement
            }

            if ((_aktuellePos.X < 0) || (_aktuellePos.X > 370) || (_aktuellePos.Y < 0) || (_aktuellePos.Y > 345)) //Wenn Snakes aktuelle Koordinate größer/kleiner ist, als die Koordinaten der Border, wird die Methode SnakeOver ausgeführt
            {
                SnakeOver(); //SnakeOver Methode wird ausgeführt
            }

            
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.W: //Im Fall das W gedrückt wird, wird folgendes ausgeführt:
                    if (vorhaerigeRichtung != (int)Bewgungsrichtung.ObenW) //Prüfen ob die aktuelle Bewgungsrichtung nicht genau entgegengesetzt zur neuen richtung ist
                        richtung = (int)Bewgungsrichtung.UntenS;
                    break; //Springt aus dem switch statement
                case Key.S: //Im Fall das S gedrückt wird, wird folgendes ausgeführt:
                    if (vorhaerigeRichtung != (int)Bewgungsrichtung.UntenS) //Prüfen ob die aktuelle Bewgungsrichtung nicht genau entgegengesetzt zur neuen richtung ist
                        richtung = (int)Bewgungsrichtung.ObenW;
                    break; //Springt aus dem switch statement
                case Key.A: //Im Fall das A gedrückt wird, wird folgendes ausgeführt:
                    if (vorhaerigeRichtung != (int)Bewgungsrichtung.RechtsD) //Prüfen ob die aktuelle Bewgungsrichtung nicht genau entgegengesetzt zur neuen richtung ist
                        richtung = (int)Bewgungsrichtung.LinksA;
                    break; //Springt aus dem switch statement
                case Key.D: //Im Fall das D gedrückt wird, wird folgendes ausgeführt:
                    if (vorhaerigeRichtung != (int)Bewgungsrichtung.LinksA) //Prüfen ob die aktuelle Bewgungsrichtung nicht genau entgegengesetzt zur neuen richtung ist
                        richtung = (int)Bewgungsrichtung.RechtsD;
                    break; //Springt aus dem switch statement
            }

            vorhaerigeRichtung = richtung;

        }

        private void SnakeOver() //Game Over Methode
        {
            timer.Stop(); //Timer stopt
            deathSound.Play(); //DethSound wird abgespielt
            MessageBox.Show($@"Game Over!" + " This is your score: " + score); //MessageBox mit aktuellem Score wird angezeigt
            exit(); //Methode exit wird ausgeführt
        }

        public void exit() //exit Methode für Game Over
        {
            gameover window = new gameover(true); //Ist true wird die if abfrage im Konstruktor ausgeführt(Sound wird abgespielt)
            window.Show(); //gameover Fenster wird ausgeführt
            this.Close(); //Alles andere schließt sich
        }

        public void ZeichnenFood(int index) //Methode für die Zeichnung des Foods auf der entsprechenden random X und Y Koordinate, auf dem Canvas(Spielfeld)
        {

            blockFood.Height = Hoehe;
            blockFood.Width = Breite;

            SolidColorBrush pinsel = new SolidColorBrush(Colors.Red);
            pinsel.Color = Farbe;
            blockFood.Fill = pinsel;

            Point foodPoint = new Point(rnd.Next(5, 365), rnd.Next(5, 340)); //365X, 340Y  //Random Koordinate zwichen 5-365X und 5-340Y für Food 

            Canvas.SetTop(blockFood, foodPoint.Y); //Y-Position des blockfood's auf Canvas(Speilfeld)
            Canvas.SetLeft(blockFood, foodPoint.X); //X-Position des blockfood's auf Canvas(Spielfeld)
            Spielfeld.Children.Insert(index, blockFood); //blockfood wird auf Canvas(Speilfeld) gezeichnet
            _foodPoint.Insert(index, foodPoint); //foodpoint wird der Lsite _foodPoint hinzugefügt

        }

        public void CheckCollision() //Kollision der Snake mit dem Food und sich selbst
        {
            int n = 0;
            foreach (Point foodpos in _foodPoint) //Kollision der Snake mit Food //definiere die Point variable neu, aus der Point list z.43
            {

                if ((Math.Abs(foodpos.X - _aktuellePos.X) < 10) && (Math.Abs(foodpos.Y - _aktuellePos.Y) < 10)) //Die differenz zwieschen abstand des Snakekopfs und dem Food wird ermittelt, wenn die differenz kleiner ist als der Snakekopf(10) wird das Food "gegessen"
                {
                    Koerperteil.laenge += 5; //Snakelänge wird um 5 addiert
                    score += 1; //Score wird um 1 addiert

                    foodSound.Play(); //Sound wird abgespielt
                    _foodPoint.RemoveAt(n); //Point in der Lsite wird entfernt
                    Spielfeld.Children.RemoveAt(n); //Food Objekt wird entfernt
                    ZeichnenFood(n); //Methode ZeichnenFood wird ausgeführt
                    break; //Springt aus dem loop 
                }
                n++;
            }

            for (int q = 0; q < (Koerperteil._snakePoints.Count - 1*2); q++) //Kollision der Snake mit sich selbst
            {
                Point point = new Point(Koerperteil._snakePoints[q].X, Koerperteil._snakePoints[q].Y);
                if ((Math.Abs(point.X - _aktuellePos.X) < (1)) && (Math.Abs(point.Y - _aktuellePos.Y) < (1))) //Die differenz der Abstände von X und Y zwischen dem Kopf und den weiteren Punkten der Snake
                {
                    SnakeOver(); //Methode SnakeOver wird ausgeführt
                }

            }

        }

    }

}