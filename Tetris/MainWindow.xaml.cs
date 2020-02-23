using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Tetris
{
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer;
        Board myBoard;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Timer anlegen
        void MainWindow_Initialized(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GameTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            CreateGrid();
        }

        //Spielfeld erzeugen
        private void CreateGrid()
        {
            MainGrid.Children.Clear(); // Grid leeren
            myBoard = new Board(MainGrid);
            myBoard.currTetraminoErase();
        }
        //Spiel starten
        private void GameStart()
        {
            MainGrid.Children.Clear(); // Grid leeren
            myBoard = new Board(MainGrid);
            Timer.Start();
        }

        private void GameTick(object sender, EventArgs e)
        {
            Score.Content = myBoard.getScore().ToString("0000000000"); //Score darstellen
            Lines.Content = myBoard.getLines().ToString("0000000000"); //Lines darstellen
            myBoard.CurrTetraminoMoveDown(); //Stein fallen lassen
        }

        //Spiel Pause
        private void GamePause()
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }

        // Spiel Ende
        private void GameEnd()
        {
            CreateGrid();
            Timer.Stop();
        }

        //Tastenbelegung
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left:
                    if (Timer.IsEnabled) myBoard.CurrTetraminoMoveLeft();
                    break;
                case Key.Right:
                    if (Timer.IsEnabled) myBoard.CurrTetraminoMoveReight();
                    break;
                case Key.Down:
                    if (Timer.IsEnabled) myBoard.CurrTetraminoMoveDown();
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) myBoard.CurrTetraminoMoveRotate();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F3:
                    GamePause();
                    break;
                case Key.F4:
                    GameEnd();
                    break;
                default:
                    break;
            }
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            GamePause();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
        }

        private void end_Click(object sender, RoutedEventArgs e)
        {
            GameEnd();
        }
    }
}
