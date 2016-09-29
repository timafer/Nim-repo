using Nim.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim.CpuVsCpu
{
    public class LearnCPU : Player
    {
        public List<State> learnedMoves { get; private set; } = new List<State>();


        public LearnCPU(char[][] visual) : base(visual)
        {
        }

        public void AddMove(State state)
        {
            if (learnedMoves.Count() > 0)
            {
                bool hasLearned = false;
                State temp = null;
                for (int i = 0; i < learnedMoves.Count(); i++)
                {
                    if (learnedMoves[i].StateOfBoard == state.StateOfBoard)
                    {
                        if (learnedMoves[i].MoveMade == state.MoveMade)
                        {
                            hasLearned = true;
                            temp = learnedMoves[i];
                        }
                    }
                }

                if (hasLearned)
                {
                    temp.ValueOfWorth += state.ValueOfWorth;
                }
                else
                {
                    learnedMoves.Add(state);
                }
            }
            else
            {
                learnedMoves.Add(state);
            }

        }

        public List<State> CheckForBoardInstance()
        {
            List<State> possibleStates = new List<State>();

            foreach (State s in learnedMoves)
            {
                bool isMatch = true;
                char[][] temp = s.StateOfBoard;

                for (int i = 0; i < temp.Count(); i++)
                {
                    for (int k = 0; k < temp[i].Count(); k++)
                    {
                        if (board[i][k] != temp[i][k])
                        {
                            isMatch = false;
                        }
                    }
                }

                if (isMatch)
                {
                    possibleStates.Add(s);
                }
            }

            return possibleStates;
        }

        public override int[] ChooseMove()
        {
            int[] move = null;

            List<State> possibleMoves = CheckForBoardInstance();
            if (possibleMoves.Count() > 0)
            {
                double currentMoveWorth = possibleMoves[0].ValueOfWorth;
                foreach (State s in possibleMoves)
                {
                    if (s.ValueOfWorth > 0)
                    {
                        if (s.ValueOfWorth > currentMoveWorth)
                        {
                            move = s.MoveMade;
                            currentMoveWorth = s.ValueOfWorth;
                        }
                        else
                        {
                            move = s.MoveMade;
                        }
                    }
                    else
                    {
                        move = CreateRandomMove();
                    }

                }

            }
            else
            {
                move = CreateRandomMove();
            }

            return move;
        }

        private int[] CreateRandomMove()
        {
            int[] move = null;

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
