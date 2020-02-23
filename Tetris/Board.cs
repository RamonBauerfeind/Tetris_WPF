using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            for (int i = 0; i < Cols; i++)
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
            //Spielstein erzeugen
            currTetramino = new Tetramino();
            currTetraminoDraw();
        }
        public int getScore()
        {
            return Score;
        }
        public int getLines()
        {
            return LinesFilled;
        }
        //aktuellen Stein zeichnen
        private void currTetraminoDraw()
        {
            //Position zum Zeichnen
            Point Position = currTetramino.getCurrPosition();
            //was zeichnen?
            Point[] Shape = currTetramino.getCurrShape();
            //Farbe
            Brush Color = currTetramino.getCurrColor();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1), (int)(S.Y + Position.Y) + 2].Background = Color;
            }
        }
        //aktuellen Stein löschen
        public void currTetraminoErase()
        {
            //Position zum Zeichnen
            Point Position = currTetramino.getCurrPosition();
            //was zeichnen?
            Point[] Shape = currTetramino.getCurrShape();

            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1), (int)(S.Y + Position.Y) + 2].Background = NoBrush;
            }
        }
        //Zeile prüfen
        private void CheckRows()
        {
            bool full;
            for(int i = 0; i < Rows; i++)
            {
                //Zeile voll?
                full = true;
                for (int j = 0; j < Cols; j++)
                {
                    //wenn keine Farbe -> Zeile nicht voll
                    if (BlockControls[j, i].Background == NoBrush)
                    {
                        full = false;
                    }
                }
                //wenn Zeile voll
                if (full)
                {
                    RemoveRow(i);
                    Score += 100;
                    LinesFilled += 1;
                }
            }
        }
        //Zeile löschen
        private void RemoveRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    //obere Zeile um 1 nach unten versetzen
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }
        //Bewegung links
        public void CurrTetraminoMoveLeft()
        {
            Point Position = currTetramino.getCurrPosition();
            Point[] Shape = currTetramino.getCurrShape();
            bool move = true;
            currTetraminoErase();
            foreach (Point S in Shape)
            {
                //Bewegung aus Spielfeld heraus?
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                //anderer Stein im Weg (Prüfung anhand der Farbe)?
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1), (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            //wenn Bewegung möglich -> bewegen
            if (move)
            {
                currTetramino.movLeft();
                currTetraminoDraw();
            }
            //ansonsten Stein unverändert darstellen
            else
            {
                currTetraminoDraw();
            }
        }
        //Bewegung rechts
        public void CurrTetraminoMoveReight()
        {
            Point Position = currTetramino.getCurrPosition();
            Point[] Shape = currTetramino.getCurrShape();
            bool move = true;
            currTetraminoErase();
            foreach (Point S in Shape)
            {
                //Bewegung aus Spielfeld heraus?
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1) >= Cols)
                {
                    move = false;
                }
                //anderer Stein im Weg (Prüfung anhand der Farbe)?
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1), (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            //wenn Bewegung möglich -> bewegen
            if (move)
            {
                currTetramino.movRight();
                currTetraminoDraw();
            }
            //ansonsten Stein unverändert darstellen
            else
            {
                currTetraminoDraw();
            }
        }
        //Bewegung nach unten
        public void CurrTetraminoMoveDown()
        {
            Point Position = currTetramino.getCurrPosition();
            Point[] Shape = currTetramino.getCurrShape();
            bool move = true;
            currTetraminoErase();
            foreach (Point S in Shape)
            {
                //Bewegung aus Spielfeld heraus?
                if (((int)(S.Y + Position.Y) + 3) >= Rows)
                {
                    move = false;
                }
                //anderer Stein im Weg (Prüfung anhand der Farbe)?
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1)), (int)(S.Y + Position.Y) + 3].Background != NoBrush)
                {
                    move = false;
                }
            }
            //wenn Bewegung möglich -> bewegen
            if (move)
            {
                currTetramino.movDown();
                currTetraminoDraw();
            }
            //ansonsten liegt Stein bereits unten
            else
            {
                //neuen Stein erzeugen
                currTetraminoDraw();
                CheckRows();
                currTetramino = new Tetramino();
            }
        }
        //Stein drehen
        public void CurrTetraminoMoveRotate()
        {
            Point Position = currTetramino.getCurrPosition();
            Point[] Shape = currTetramino.getCurrShape();
            Point[] S = new Point[4];
            bool move = true;
            Shape.CopyTo(S, 0); //Form in Point Array S kopieren
            currTetraminoErase(); //Stein löschen

            for (int i = 0; i < S.Length; i++)
            {
                //simulieren der Rotation
                double x = S[i].X; //temporäre Variable
                S[i].X = S[i].Y * -1; // X-Koordinate durch Y-Koordinate * -1 ersetzen
                S[i].Y = x;
                //Prüfung der Y-Koordinaten
                if (((int)((S[i].Y + Position.Y) + 2)) >= Rows)
                {
                    move = false;
                }
                //Prüfung der X-Koordinaten nach links
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) < 0)
                {
                    move = false;
                }
                //Prüfung der X-Koordinaten nach rechts
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) >= Rows)
                {
                    move = false;
                }
                //Blockade im Weg, d.h. Feld farbig?
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Cols / 2) - 1)), (int)(S[i].Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            //wenn move == true -> drehen
            if (move)
            {
                currTetramino.movRotate();
                currTetraminoDraw();
            }
            //ansonsten Stein neu zeichnen, d.h. nicht drehen
            else
            {
                currTetraminoDraw();
            }
        }
    }
}
