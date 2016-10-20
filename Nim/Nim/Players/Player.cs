using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim.Players
{
    public abstract class Player
    {
        public int[] board;
        public int aBound = 4;
        public int bBound = 6;
        public int cBound = 8;
        private int rowUpBound = 0;
        private int rowDownBound = 3;
        public string playername = "";
        public int wincount=0;

        public Player(int[] visual,string name)
        {
            board = visual;
            playername = name;
        }

        public void ResetBoard(int[] visual)
        {
            board = visual;
        }
        public abstract int[] ChooseMove();

        public void setRandomBounds()
        {
            for (int i = 0; i < board.Count(); i++)
            {
                for (int k = 0; k < board[i]; k++)
                {
                    switch (i)
                    {
                        case 0:
                            aBound = board[i];
                            break;
                        case 1:
                            bBound = board[i];
                            break;
                        case 2:
                            cBound = board[i];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public int countRow(int row)
        {
            return board[row];
        }

        public void resetBounds()
        {
            aBound = 4;
            bBound = 6;
            cBound = 8;
        }
    }
}
