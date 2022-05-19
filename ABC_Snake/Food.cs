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
    public class Food
    {
        // Eigenschaft
        public Rectangle blockFood = new Rectangle();
        private readonly List<Point> _foodPoint = new List<Point>();
        public Random rnd = new Random();
        public int Breite = 10;
        public int Hoehe = 10;
        public int X;
        public int Y;
        public Color Farbe = Colors.Red;

        public void ZeichnenFood(Canvas Spielfeld)
        {
            blockFood.Height = Hoehe;
            blockFood.Width = Breite;

            SolidColorBrush pinsel = new SolidColorBrush(Colors.Red);
            pinsel.Color = Farbe;
            blockFood.Fill = pinsel;

            Point foodPoint = new Point(rnd.Next(5, 680), rnd.Next(5, 380));

            Canvas.SetTop(blockFood, foodPoint.Y);
            Canvas.SetLeft(blockFood, foodPoint.X);
            Spielfeld.Children.Add(blockFood);
            _foodPoint.Add(foodPoint);
        }

    }
}
