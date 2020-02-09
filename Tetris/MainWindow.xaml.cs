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

namespace Tetris
{
    //Klasse für Spielfeld
    public class Board
    {
        private int Rows;
        private int Cols;
        private int Score;
        private int LinesFilled;
        private Tetramino currTetramino;
        private Label[,] BlockControls;
        static private Brush NoBrush = Brushes.Transparent;
        static private Brush SilverBrush = Brushes.Gray;

        //Konstruktor
        public Board(Grid TetrisGrid)
        {
            Rows = TetrisGrid.RowDefinitions.Count;
            Cols = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            LinesFilled = 0;

            BlockControls = new Label[Cols, Rows];

            for(int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
        }
    }

    public class Tetramino
    {
        private Point currPosition;
        private Point[] currShape;
        private Brush currColor;
        private bool rotate;

        //Konstruktor
        public Tetramino() 
        {
            currPosition = new Point(0, 0);
            currColor = Brushes.Transparent;
            currShape = setRandomShape();
        }

        public Brush getCurrColor()
        {
            return currColor;
        }

        public Point getCurrPosition()
        {
            return currPosition;
        }

        public Point[] getCurrShape()
        {
            return currShape;
        }

        public void movLeft()
        {
            currPosition.X -= 1;
        }

        public void movRight()
        {
            currPosition.X += 1;
        }

        public void movDown()
        {
            currPosition.Y += 1;
        }

        public void movRotate()
        {
            //Drehen der Form durch Koordinatentausch mittels Hilfsvariable
            if(rotate)
            {
                for(int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }
            }
        }

        private Point[] setRandomShape()
        {
            // legt Rotation, Farbe und Form der 7 möglichen Steine fest
            Random rand = new Random();
            switch(rand.Next() % 7)
            {
                case 0: //I
                    rotate = true;
                    currColor = Brushes.Cyan;
                    return new Point[] { new Point(0, 0), new Point(-1, 0), new Point(1, 0), new Point(2, 0) };
                case 1: //J
                    rotate = true;
                    currColor = Brushes.Blue;
                    return new Point[] { new Point(1, -1), new Point(-1, 0), new Point(0, 0), new Point(1, 0) };
                case 2: //L
                    rotate = true;
                    currColor = Brushes.Orange;
                    return new Point[] { new Point(0, 0), new Point(-1, 0), new Point(1, 0), new Point(1, -1) };
                case 3: //O
                    rotate = false;
                    currColor = Brushes.Yellow;
                    return new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 0), new Point(1, 1) };
                case 4: //S
                    rotate = true;
                    currColor = Brushes.Green;
                    return new Point[] { new Point(0, 0), new Point(-1, 0), new Point(0, -1), new Point(1, 0) };
                case 5: //T
                    rotate = true;
                    currColor = Brushes.Purple;
                    return new Point[] { new Point(0, 0), new Point(-1, 0), new Point(0, -1), new Point(1, 1) };
                case 6: //Z
                    rotate = true;
                    currColor = Brushes.Red;
                    return new Point[] { new Point(0, 0), new Point(-1, 0), new Point(0, 1), new Point(1, 1) };
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
