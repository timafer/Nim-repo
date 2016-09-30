using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim.Players
{
    public abstract class Player
    {
        public char[][] board;
        public int aBound = 4;
        public int bBound = 6;
        public int cBound = 8;
        private int rowUpBound = 0;
        private int rowDownBound = 3;

        public Player(char[][] visual)
        {
            board = visual;
        }

        public void ResetBoard(char[][] visual)
        {
            board = visual;
        }
        public abstract int[] ChooseMove();
        
        public void setRandomBounds()
        {
            for (int i = 0; i < board.Count(); i++)
            {
                int xCounter = 0;
                int oCounter = 0;
                for (int k = 0; k < board[i].Count(); k++)
                {
                    if (board[i][k] == 'x')
                    {
                        xCounter++;
                    }
                    else
                    {
                        oCounter++;
                    }
                    switch (i)
                    {
                        case 0:
                            aBound = 4 - (Math.Abs(oCounter - xCounter));
                            break;
                        case 1:
                            bBound = 6 - (Math.Abs(oCounter - xCounter));
                            break;
                        case 2:
                            cBound = 8 - (Math.Abs(oCounter - xCounter));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void resetBounds()
        {
            aBound = 4;
            bBound = 6;
            cBound = 8;
        }
    }
}
