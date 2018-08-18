using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hub
{
    /// <summary>
    /// Logique d'interaction pour BubbledBackground.xaml
    /// </summary>
    public partial class BubbledBackground : UserControl
    {
        /* SETTINGS */
        private static readonly double FPS = 30;

        private static readonly int MinSpeed = 10;   // Speed is in pixel.sec^-1
        private static readonly int MaxSpeed = 100;

        private static readonly int MinDiameter = 10;   // In pixels
        private static readonly int MaxDiameter = 200;

        private static readonly double SpawnFrequency = 3;  // In bubble.sec^-1
        // TODO: Doesn't seem to be working very good (too many spawn, check formula)

        private static readonly Color[] BrightColors = { Colors.Red, Colors.Orange, Colors.Yellow, Colors.LightGreen, Colors.Green, Colors.Cyan, Colors.LightBlue, Colors.Blue, Colors.Purple, Colors.Magenta, Colors.Pink };

        private Random Random = new Random();
        private List<Bubble> Bubbles = new List<Bubble>();


        public BubbledBackground()
        {
            InitializeComponent();

            // Start a timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(OnTick);
            timer.Interval = TimeSpan.FromSeconds(1 / FPS);
            timer.Start();
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (Random.Next(1, (int)Math.Round(FPS / SpawnFrequency) + 1) == 1)
                CreateBubble();

            for (int i = 0; i < Bubbles.Count; i++)
            {
                Bubbles[i].Y += Bubbles[i].Speed * (1 / FPS);   // Move the bubble up
                if (Bubbles[i].Y > Canvas.ActualHeight + Bubbles[i].View.ActualHeight)
                {
                    Canvas.Children.Remove(Bubbles[i].View);  // And remove it if ot goes out of the screen
                    Bubbles.Remove(Bubbles[i]);
                }
                else
                    Canvas.SetTop(Bubbles[i].View, Canvas.ActualHeight - Bubbles[i].Y);
            }
        }

        private void CreateBubble()
        {
            int radius = Random.Next(MinDiameter, MaxDiameter);

            Ellipse ellipse = new Ellipse
            {
                Fill = new SolidColorBrush(BrightColors[Random.Next(BrightColors.Length)]),
                Height = radius,
                Width = radius
            };

            double x = Random.NextDouble() * Canvas.ActualWidth;   // Choose a random horizontal position
            Bubble bubble = new Bubble(ellipse, Random.Next(MinSpeed, MaxSpeed));
            Bubbles.Add(bubble);

            Canvas.SetLeft(bubble.View, x);
            Canvas.SetTop(bubble.View, Canvas.ActualHeight - bubble.Y);
            Canvas.Children.Add(bubble.View);
        }
    }

    class Bubble
    {
        public Ellipse View;
        public int Speed;
        public double Y;

        public Bubble(Ellipse view, int speed)
        {
            View = view;
            Speed = speed;
            Y = -view.ActualHeight;  // Hide the bubble at the bottom of the screen
        }
    }
}
