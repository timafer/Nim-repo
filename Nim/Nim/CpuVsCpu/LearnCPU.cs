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
        private char[][] board;

        public LearnCPU(char[][] visual)
        {
            board = visual;
        }

        public void ResetBoard(char[][] visual)
        {
            board = visual;
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
            int[] move = new int[2];

            List<State> possibleMoves = CheckForBoardInstance();
            double currentMoveWorth = 0;
            if (possibleMoves.Count() > 0)
            {
                foreach (State s in possibleMoves)
                {
                    if (currentMoveWorth < s.ValueOfWorth && s.ValueOfWorth > 0)
                    {
                        move = s.MoveMade;
                        currentMoveWorth = s.ValueOfWorth;
                        Console.WriteLine(move[0] + ", " + move[1]);
                    }
                }
            }
            else
            {
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
            }
            return move;
        }
    }
}
