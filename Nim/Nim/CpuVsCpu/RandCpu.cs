using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nim.Players;

namespace Nim.CpuVsCpu
{
    public class RandCpu: Player
    {

        public RandCpu(char[][] visual) : base(visual)
        {
        }

        public override int[] ChooseMove()
        {
            int[] move = new int[2];

            Random randomGen = new Random();

            int rowChoice = randomGen.Next(0, 3);
            int removeAmount = 0;

            switch (rowChoice)
            {
                case 0:
                    removeAmount = randomGen.Next(1, aBound);
                    break;
                case 1:
                    removeAmount = randomGen.Next(1, bBound);
                    break;
                case 2:
                    removeAmount = randomGen.Next(1, cBound);
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
