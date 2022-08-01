using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Media;

namespace ABC_Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public Quadrat Koerperteil = new Quadrat();
        public Food Foodteil = new Food();

        SoundPlayer sound = new SoundPlayer(@"C:\Users\gereo\Documents\ProgrammeC#\ABC_Snake\windows_xp_shutdown.wav");

        public int X;
        public int Y;
        public int richtung = 0;
        private int vorhaerigeRichtung = 0;

        public MainWindow()
        {
            InitializeComponent();

            GameState.itemCount = 1;                                         //Muss angepasst werden!!!
            DrawFood();

            Koerperteil.Zeichnen(Spielfeld);
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Animation; // Delegate
            timer.Start();

        }

        private enum Movingdirection
        {
            ObenW = 10,
            UntenS = 8,
            LinksA = 9,
            RechtsD = 7
        };

        private void DrawFood()
        {
            Foodteil.ZeichnenFood(Spielfeld);
            //Collision


        }

        void Animation(object sender, EventArgs e)
        {

            switch (richtung)
            {
                case (int)Movingdirection.UntenS:
                    Koerperteil.Y -= 5;
                    Koerperteil.Zeichnen(Spielfeld);
                    Koerperteil.remove(Spielfeld);
                    break;
                case (int)Movingdirection.ObenW:
                    Koerperteil.Y += 5;
                    Koerperteil.Zeichnen(Spielfeld);
                    Koerperteil.remove(Spielfeld);
                    break;
                case (int)Movingdirection.RechtsD:
                    Koerperteil.X += 5;
                    Koerperteil.Zeichnen(Spielfeld);
                    Koerperteil.remove(Spielfeld);
                    break;
                case (int)Movingdirection.LinksA:
                    Koerperteil.X -= 5;
                    Koerperteil.Zeichnen(Spielfeld);
                    Koerperteil.remove(Spielfeld);
                    break;
            }

            if ((Koerperteil.X < 0) || (Koerperteil.X > 670) || (Koerperteil.Y < 0) || (Koerperteil.Y > 345))
            {
                SnakeOver();    
            }
            
        }
       
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.W:
                    if (vorhaerigeRichtung != (int)Movingdirection.ObenW)
                        richtung = (int)Movingdirection.UntenS;
                    break;
                case Key.S:
                    if (vorhaerigeRichtung != (int)Movingdirection.UntenS)
                        richtung = (int)Movingdirection.ObenW;
                    break;
                case Key.A:
                    if (vorhaerigeRichtung != (int)Movingdirection.RechtsD)
                        richtung = (int)Movingdirection.LinksA;
                    break;
                case Key.D:
                    if (vorhaerigeRichtung != (int)Movingdirection.LinksA)
                        richtung = (int)Movingdirection.RechtsD;
                    break;
                case Key.Enter:
                    DrawFood();
                    break;
            }

            vorhaerigeRichtung = richtung;
 
        }

        public void SnakePos()
        {

        }

        private void Spielfeld_KeyDown(object sender, KeyEventArgs e)
        {

        }
        
        private void SnakeOver()
        {
            timer.Stop();
            PlaySound();
            MessageBox.Show($@"You Lose!");
            this.Close();
        }

        private void PlaySound()
        {
            sound.Play();
        }

    }

}