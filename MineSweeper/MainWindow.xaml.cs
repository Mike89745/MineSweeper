using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MineSweeper
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Game Game;
        public int time;
        public int sizeX;
        public int sizeY;
        public MainWindow()
        {
            InitializeComponent();
            StartGameButton.Click += StartGame;
        }
        private void StartGame(object sender, RoutedEventArgs e)
        { 
            sizeX = int.Parse(InputX.Text);
            sizeY = int.Parse(InputY.Text);
            Game = new Game(sizeX, sizeY, Difficulty.easy);
            GenerateGrid(sizeX, sizeY);
            Game.GenerateField();
            InputGrid.Visibility = Visibility.Hidden;
            Reset.Click += ResetGame;
        }
        public void GenerateGrid(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                MineGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < y; i++)
            {
                MineGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < x; i++)
            {
                for (int b = 0; b < y; b++)
                {
                    Button MineButton = new Button();

                    MineGrid.Children.Add(MineButton);
                    Grid.SetRow(MineButton, i);
                    Grid.SetColumn(MineButton, b);
                    MineButton.Click += LeftClick;
                    MineButton.MouseRightButtonUp += RightClick;
                }
            }
            TotalMines.Content = Game.MineCount.ToString();
        }
        private void LeftClick(object sender, RoutedEventArgs e)
        {
            if (!Game.GameOver || Game.GameWon)
            {
                Button button = (Button)e.Source;
                int y = Grid.GetColumn(button);
                int x = Grid.GetRow(button);
                if (!Game.generated)
                {
                    Game.GenerateMineField(x, y);
                    RevealAround(Game.MineField[x][y], button);
                }
                // button.Background = Brushes.Yellow;
                //  Debug.WriteLine(x + " " + y);
                Game.DebugMineField(x, y);
                RevealPoint(Game.MineField[x][y], button);
                Game.isGameWon();
            }
            if (Game.GameWon)
            {
                GameWonLabel.Visibility = Visibility.Visible;
            }
        }
        private void RightClick(object sender, RoutedEventArgs e)
        {
            if (!Game.GameOver || Game.GameWon)
            {
                Button button = (Button)e.Source;
                int y = Grid.GetColumn(button);
                int x = Grid.GetRow(button);
                Flag(Game.MineField[x][y], button);
                Game.isGameWon();
            }
            if (Game.GameWon)
            {
                GameWonLabel.Visibility = Visibility.Visible;
            }
        }
        public void Flag(Mine mine, Button button)
        {
            if (mine.flagged && !mine.reaveled)
            {
                mine.flagged = false;
                button.Content = " ";
            }
            else
            {
                mine.flagged = true;
                button.Content = "F";
            }
        }
        public void RevealPoint(Mine mine, Button button)
        {
           /* Debug.WriteLine("Explosive:" + mine.explosive);
            Debug.WriteLine("Revealed:" + mine.reaveled);
            Debug.WriteLine("Number:" + mine.number);
            Debug.WriteLine("Flagged:" + mine.flagged);*/
            if (!mine.explosive)
            {
                if (!mine.reaveled)
                {
                    mine.reaveled = true;
                    Game.RevealedPoints++;
                    if (mine.number > 0)
                    {
                        button.Content = mine.number.ToString();
                        button.Background = Brushes.White;
                    }
                    else
                    {
                        button.Background = Brushes.White;
                        RevealAround(mine, button);
                    }
                }
            }
            else
            {
                Game.isGameOver();
                RevealAll();
                button.Content = "x";
                button.Background = Brushes.Red;
            }
            button.Foreground = setColor(mine.number);
        }
        public void RevealAround(Mine mine, Button button)
        {
            int y = Grid.GetColumn(button);
            int x = Grid.GetRow(button);
            Mine getMine;
            if (x - 1 >= 0)
            {
                getMine = Game.GetPoint(x - 1, y);
                if (!getMine.explosive && !getMine.reaveled)
                {
                    RevealPoint(getMine, GetButton(x - 1, y));
                 //   GetButton(x - 1, y).Background = Brushes.MediumAquamarine;
                }
            }
            if (y - 1 >= 0)
            {
                getMine = Game.GetPoint(x, y - 1);
                if (!getMine.explosive && !getMine.reaveled)
                {
                    RevealPoint(getMine, GetButton(x, y - 1));
                 //   GetButton(x, y - 1).Background = Brushes.MediumAquamarine;
                }
            }
            if (y + 1 <= sizeY - 1)
            {
                getMine = Game.GetPoint(x, y + 1);
                if (!getMine.explosive && !getMine.reaveled)
                {
                    
                    RevealPoint(getMine, GetButton(x, y + 1));
                  //  GetButton(x, y + 1).Background = Brushes.MediumAquamarine;
                }
            }
            if (x + 1 <= sizeX - 1)
            {
                getMine = Game.GetPoint(x + 1, y);
                if (!getMine.explosive && !getMine.reaveled)
                {
                    RevealPoint(getMine, GetButton(x + 1, y));
                   // GetButton(x + 1, y).Background = Brushes.MediumAquamarine;
                }
            }
        }
        public Button GetButton(int x, int y)
        {
            return MineGrid.Children.Cast<Button>().First(Button => Grid.GetRow(Button) == x && Grid.GetColumn(Button) == y);
        }
       
        public void RevealAll()
        {
            Button button;
            for (int i = 0; i < 10; i++)
            {
                for (int b = 0; b < 10; b++)
                {
                    button = MineGrid.Children
                      .Cast<Button>()
                      .First(Button => Grid.GetRow(Button) == i && Grid.GetColumn(Button) == b);
                    if (!Game.MineField[i][b].explosive)
                    {
                        if (Game.MineField[i][b].number > 0)
                        {
                            button.Content = Game.MineField[i][b].number.ToString();
                        }
                    }
                    else
                    {
                        if (Game.MineField[i][b].flagged)
                        {
                            button.Content = "+";
                        }
                        button.Content = "x";
                    }
                    button.Foreground = setColor(Game.MineField[i][b].number);
                    button.Background = Brushes.White;
                }
            }
        }
        public void ResetGame(object sender, RoutedEventArgs e)
        {
            Button button;
            for (int i = 0; i < 10; i++)
            {
                for (int b = 0; b < 10; b++)
                {
                    button = MineGrid.Children
                      .Cast<Button>()
                      .First(Button => Grid.GetRow(Button) == i && Grid.GetColumn(Button) == b);
                    button.Content = "";
                    button.Background = Brushes.LightGray;
                }
            }
            Game.generated = false;
            Game.GameOver = false;
            Game.GameWon = false;
            Game.RevealedPoints = 0;
            GameWonLabel.Visibility = Visibility.Hidden;
        }
        public SolidColorBrush setColor(int number)
        {
            switch (number)
            {
                case 1:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#0019ff"));
                case 2:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#347f00"));
                case 3:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff0000"));
                case 4:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#00047f"));
                case 5:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f0000"));
                case 6:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f0000"));
                case 7:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f0000"));
                case 8:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f0000"));
                case 9:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#7f0000"));
                default:
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#000"));
            }
        }
    }
}
