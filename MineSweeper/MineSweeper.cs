using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MineSweeper
{
    public class MineSweeper
    {
        public List<List<Mine>> MineField;
        public int MineCount = 30;
        public int size = 10;
        public bool isGameOver = false;
        public int Flags = 0;
        public int ReaveledTiles = 0;

        Random random = new Random();
        public void GenerateMineField()
        {
            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                for (int b = 0; b < size; b++)
                {
                    MineField[i].Add(new Mine(false));
                }
            }

            while (counter < MineCount)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int b = 0; b < size; b++)
                    {
                        if (counter < MineCount)
                        {
                            if (random.Next(0, 30) < 10)
                            {
                                MineField[i][b].explosive = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int b = 0; b < size; b++)
                {
                    Mine mine = MineField[i][b];
                    if (!mine.explosive)
                    {
                        if(MineField[i][b])
                    }
                    
                }
            }
        }
        public bool isMine(int x, int y)
        {
            if (MineField[x][y].explosive)
            {
                GameOver();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Reveal(int x, int y)
        {

        }
        public void ReavealAll()
        {

        }
        public void Flag(int x, int y)
        {
            Mine mine = MineField[x][y];
            if (mine.flagged)
            {
                mine.flagged = false;
                Flags--;
            }
            else
            {
                mine.flagged = true;
                Flags++;
            }
        }
        public void GameOver()
        {
            isGameOver = true;
        }
    }
}
