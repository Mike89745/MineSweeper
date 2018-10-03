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
        public MainWindow()
        {
            InitializeComponent();
            GenerateGrid(10, 10, 30);
        }

        public void GenerateGrid(int x, int y, int mines)
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
                }
            }
        }
        public void RightClick()
        {

        }
        public void LeftClick()
        {

        }
    }
}
