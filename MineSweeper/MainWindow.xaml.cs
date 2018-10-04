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
        public Game Game = new Game();
        public MainWindow()
        {
            InitializeComponent();
            GenerateGrid(10, 10);
            Game.GenerateMineField();
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
                    MineButton.Click += RevealPoint;
                    MineButton.MouseRightButtonUp += FlagPoint;
                }
            }
        }
        private void RevealPoint(object sender, RoutedEventArgs e)
        {
            var element = (Button)e.Source;

            int x = Grid.GetColumn(element);
            int y = Grid.GetRow(element);
            if (!Game.MineField[x][y].explosive)
            {
                if (Game.MineField[x][y].number > 0)
                {
                    element.Content = Game.MineField[x][y].number.ToString();
                }
            }
            else
            {
                Game.GameOver();
                RevealAll();
                element.Content = "x";
            }
            element.Foreground = setColor(Game.MineField[x][y].number);
            element.Background = Brushes.White;
        }
        private void FlagPoint(object sender, RoutedEventArgs e)
        {
            var element = (Button)e.Source;

            int x = Grid.GetColumn(element);
            int y = Grid.GetRow(element);
            if (Game.MineField[x][y].flagged)
            {
                Game.MineField[x][y].flagged = false;
                element.Content = " ";
            }
            else
            {
                Game.MineField[x][y].flagged = true;
                element.Content = "F";
            }
        }
        public void RevealAll()
        {
            Button uIElement;
            for (int i = 0; i < 10; i++)
            {
                for (int b = 0; b < 10; b++)
                {
                    uIElement = MineGrid.Children
                      .Cast<Button>()
                      .First(Button => Grid.GetRow(Button) == i && Grid.GetColumn(Button) == b);
                    if (!Game.MineField[i][b].explosive)
                    {
                        if (Game.MineField[i][b].number > 0)
                        {
                            uIElement.Content = Game.MineField[i][b].number.ToString();
                        }
                    }
                    else
                    {
                        if (Game.MineField[i][b].flagged)
                        {
                            uIElement.Content = "+";
                        }
                        uIElement.Content = "x";
                    }
                    uIElement.Foreground = setColor(Game.MineField[i][b].number);
                    uIElement.Background = Brushes.White;
                }
            }
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
        public void RightClick(int x, int y)
        {
            Game.Flag(x,y);
        }
        public void LeftClick(int x, int y)
        {
            
        }
    }
}
