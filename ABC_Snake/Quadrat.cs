using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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


namespace ABC_Snake
{
    public class Quadrat
    {
        // Eigenschaft
        public List<Point> _snakePoints = new List<Point>(); //X und Y Koordinaten-Liste für Snake

        public int Breite = 10;
        public int Hoehe = 10;
        public int X;
        public int Y;
        public int laenge = 1; //Länge der Snake bei Start

        public Color Farbe = Colors.White;

        public void Zeichnen(Canvas Spielfeld, Point point)
        {

            Rectangle block = new Rectangle();
            block.Height = Hoehe;
            block.Width = Breite;

            SolidColorBrush pinsel = new SolidColorBrush(Colors.White);
            pinsel.Color = Farbe;
            block.Fill = pinsel;

            Canvas.SetTop(block, point.Y);
            Canvas.SetLeft(block, point.X);

            int count = Spielfeld.Children.Count;
            Spielfeld.Children.Add(block);
            _snakePoints.Add(point);

            if (count > laenge) //Letztes Stück(Schwanz) wird gelöscht, Kombination mit Dispatchtimer-Effekt entsteht die Illusion der Bewgung der Schlange
            {
                Spielfeld.Children.RemoveAt(count - laenge + 0);
                _snakePoints.RemoveAt(count - laenge);
            }

        }

    }
}
