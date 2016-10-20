using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nim.Players;

namespace Nim.CpuVsCpu
{
    public class MoveControl
    {
        Player p2;
        List<State>previousStates = new List<State>();
        public MoveControl(Player player2)
        {
            p2 = player2;
        }
        public List<State> PlayerTakeTurn(Player player,int[] visual)
        {
            Console.WriteLine(player.playername + " Turn");
            bool isVaildMove = false;
            int[] move = null;
            do
            {
                move = player.ChooseMove();
                isVaildMove = CheckRow(move[0], move[1],visual);
            }
            while (!isVaildMove);
            if (p2.GetType() == typeof(LearnCPU))
            {
                int[] arrayCopy = CopyArray(visual);
                previousStates.Add(new State(move, 0, arrayCopy));
            }
            int row = move[0];
            int removeAmount = move[1];
            visual[row] -= removeAmount;

            Console.WriteLine(player.playername + " removed " + removeAmount + " from row " + (char)(row+65) + ".");
            return previousStates;
        }
        public int[] CopyArray(int[] array)
        {
            int[] copy = new int[3];
            Array.Copy(array, copy, 3);
            return copy;
        }
        /// <summary>
        /// Checks if the row contians the amount to be removed
        /// </summary>
        /// <param name="row">Row that is checked</param>
        /// <param name="removeAmount">Amount the player wished to remove</param>
        /// <returns>If row contains enough to be removed</returns>
        public bool CheckRow(int row, int removeAmount,int[] visual)
        {
            int counter = visual[row];


            if (counter >= removeAmount && removeAmount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void RateMoves()
        {
            int totalMoves = previousStates.Count;

            if (totalMoves % 2 == 0)
            {
                for (int i = totalMoves - 1; i > 0; i--)
                {
                    int mtplr = -1;

                    if (i % 2 == 0)
                    {
                        mtplr = 1;
                    }

                    previousStates[i].ValueOfWorth = ((i + 1) / (double)totalMoves) * mtplr;
                }
            }
            else
            {
                for (int i = totalMoves - 1; i > 0; i--)
                {
                    int mtplr = -1;

                    if (i % 2 != 0)
                    {
                        mtplr = 1;
                    }

                    previousStates[i].ValueOfWorth = ((i + 1) / (double)totalMoves) * mtplr;
                }
            }

            foreach (State temp in previousStates)
            {
                ((LearnCPU)p2).AddMove(temp);
            }
        }

    }
}
