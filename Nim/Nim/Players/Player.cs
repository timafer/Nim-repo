using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim.Players
{
    public abstract class Player
    {
        public virtual int[] ChooseMove()
        {
            int[] move = new int[2];

            Random randomGen = new Random();

            int rowChoice = randomGen.Next(0, 3);
            int removeAmount = 0;

            switch (rowChoice)
            {
                case 0:
                    removeAmount = randomGen.Next(1, 4);
                    break;
                case 1:
                    removeAmount = randomGen.Next(1, 6);
                    break;
                case 2:
                    removeAmount = randomGen.Next(1, 8);
                    break;
                default:
                    Console.WriteLine("PROGRAMMER ERROR: Invalid rowChoice made");
                    break;
            }

            move = new int[] { rowChoice, removeAmount };

            return move;
        }
    }
}
