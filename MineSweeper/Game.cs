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

        public Difficulty difficulty = Difficulty.easy;

        public int MineCount;
        public int sizeX;
        public int sizeY;

        public bool GameOver = false;
        public bool GameWon = false;
        public bool generated = false;

        private int totalPoints;
        public int RevealedPoints;

        Random random = new Random();
        public Game(int x, int y, Difficulty diff)
        {
            difficulty = diff;
            sizeX = x;
            sizeY = y;
            double xd = x * y / (int)difficulty;
            if (xd < 1)
            {
                xd = 1;
            }
            MineCount = (int)Math.Round(xd);
            totalPoints = x * y - MineCount;
            GenerateField();
        }
        public void GenerateField()
        {
            for (int x = 0; x < sizeX; x++)
            {
                MineField.Add(new List<Mine>());
                for (int y = 0; y < sizeY; y++)
                {
                    MineField[x].Add(new Mine(false));
                }
            }
        }
        public void GenerateMineField(int SaveX, int SaveY)
        {
            generated = true;
            int counter = 0;

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    MineField[x][y] = new Mine(false);
                }
            }

            while (counter < MineCount)
            {
                int y = random.Next(0, sizeY);
                int x = random.Next(0, sizeX);
                if (counter < MineCount)
                {
                    if (!MineField[x][y].explosive)
                    {
                        if (random.Next(0, 50) < 10)
                        {
                            if (SaveX != x && SaveY != y)
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
            }
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    Mine mine = MineField[x][y];
                    if (!mine.explosive)
                    {
                        mine.number = SetPoint(x, y);
                    }
                }
            }
            DebugMineField(SaveX,SaveY);
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
            if (x + 1 <= sizeX - 1 && y + 1 <= sizeY - 1)
            {
                if (MineField[x + 1][y + 1].explosive)
                {
                    Counter++;
                }
            }
            if (y + 1 <= sizeX - 1)
            {
                if (MineField[x][y + 1].explosive)
                {
                    Counter++;
                }
            }
            if (x + 1 <= sizeX - 1)
            {
                if (MineField[x + 1][y].explosive)
                {
                    Counter++;
                }
            }
            if (x + 1 <= sizeX - 1 && y - 1 >= 0)
            {
                if (MineField[x + 1][y - 1].explosive)
                {
                    Counter++;
                }
            }
            if (x - 1 >= 0 && y + 1 <= sizeY - 1)
            {
                if (MineField[x - 1][y + 1].explosive)
                {
                    Counter++;
                }
            }


            return Counter;
        }
        public Mine GetPoint(int x,int y)
        {
            return MineField[x][y];
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
        public void isGameOver()
        {
            GameOver = true;
        }
        public void isGameWon()
        {
            //Debug.WriteLine(RevealedPoints >= totalPoints);
            if(RevealedPoints >= totalPoints)
            {
                GameWon = true;
            }
        }
        public void DebugMineField(int x,int y)
        {
            Debug.WriteLine(" ");
            Debug.WriteLine(" ");
            string log = " ";
            for (int i = 0; i < sizeX; i++)
            {
                for (int b = 0; b < sizeY; b++)
                {
                    if(i == x && b == y)
                    {
                        if (MineField[i][b].explosive)
                        {
                            log += "{x} ";
                        }
                        else
                        {
                            log += "{" + MineField[i][b].number + "} ";
                        }
                    }
                    else
                    {
                        if (MineField[i][b].explosive)
                        {
                            log += "[x] ";
                        }
                        else
                        {
                            log += "[" + MineField[i][b].number + "] ";
                        }
                    }
                    
                }
                Debug.WriteLine(log);
                log = " ";
            }
        }
    }
}
