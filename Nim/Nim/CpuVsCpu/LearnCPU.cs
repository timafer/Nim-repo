using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim.CpuVsCpu
{
    public class LearnCPU
    {
        public List<State> learnedMoves { get; private set; } = new List<State>();

        public void AddMove(State state)
        {
            learnedMoves.Add(state);
        }

        /// <summary>
        /// Sorts the learned moves by value
        /// </summary>
        public void SortMovesByValue()
        {
            for (int i = learnedMoves.Count() - 1; i > 0; i--)
            {
                for (int k = 1; k < i; k++)
                {
                    if (learnedMoves[k - 1].ValueOfWorth < learnedMoves[k].ValueOfWorth)
                    {
                        State temp = learnedMoves[k - 1];
                        learnedMoves[k - 1] = learnedMoves[i];
                        learnedMoves[i] = temp;
                    }
                }
            }
        }

        public int[] ChooseMove()
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
