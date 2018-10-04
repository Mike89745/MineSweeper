using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MineSweeper
{
    public class Game
    {
        public List<List<Mine>> MineField = new List<List<Mine>>();
        public int MineCount = 30;
        public int size = 10;
        public bool isGameOver = false;
        
        Random random = new Random();
        public void GenerateMineField()
        {
            int counter = 0;
            for (int x = 0; x < size; x++)
            {
                MineField.Add(new List<Mine>());
                for (int y = 0; y < size; y++)
                {
                    MineField[x].Add(new Mine(false));
                }
            }

            while (counter < MineCount)
            {
                int y = random.Next(0, size);
                int x = random.Next(0, size);
                if (counter < MineCount)
                {
                    if (!MineField[x][y].explosive)
                    {
                        if (random.Next(0, 50) < 10)
                        {
                            MineField[x][y].explosive = true;
                            counter++;
                            if (counter >= MineCount)
                            {
                                break;
                            }
                        }

                    }
                }
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Mine mine = MineField[x][y];
                    if (!mine.explosive)
                    {
                        mine.number = SetPoint(x, y);
                    }
                }
            }
            DebugMineField();
        }
        public void Reveal(int x, int y)
        {
            MineField[x][y].reaveled = true;
        }
        public int SetPoint(int x, int y)
        {
            int Counter = 0;
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                if (MineField[x - 1][y - 1].explosive)
                {
                    Counter++;
                }
            }
            if (x - 1 >= 0)
            {
                if (MineField[x - 1][y].explosive)
                {
                    Counter++;
                }
            }
            if (y - 1 >= 0)
            {
                if (MineField[x][y - 1].explosive)
                {
                    Counter++;
                }
            }
            if (x + 1 <= size - 1 && y + 1 <= size - 1)
            {
                if (MineField[x + 1][y + 1].explosive)
                {
                    Counter++;
                }
            }
            if (y + 1 <= size - 1)
            {
                if (MineField[x][y + 1].explosive)
                {
                    Counter++;
                }
            }
            if (x + 1 <= size - 1)
            {
                if (MineField[x + 1][y].explosive)
                {
                    Counter++;
                }
            }
            if (x + 1 <= size - 1 && y - 1 >= 0)
            {
                if (MineField[x + 1][y - 1].explosive)
                {
                    Counter++;
                }
            }
            if (x - 1 >= 0 && y + 1 <= size - 1)
            {
                if (MineField[x - 1][y + 1].explosive)
                {
                    Counter++;
                }
            }
           
            
            return Counter;
        }
       
        public void Flag(int x, int y)
        {
            Mine mine = MineField[x][y];
            if (mine.flagged)
            {
                mine.flagged = false;
            }
            else
            {
                mine.flagged = true;
            }
        }
        public void GameOver()
        {
            isGameOver = true;
        }
        public void DebugMineField()
        {
            string log = " ";
            for (int i = 0; i < size; i++)
            {
                for (int b = 0; b < size; b++)
                {

                    if (MineField[i][b].explosive)
                    {
                        log += "[x] ";
                    }
                    else
                    {
                        log += "["+ MineField[i][b].number + "] ";
                    }
                }
                Debug.WriteLine(log);
                log = " ";
            }
        }
    }
}
