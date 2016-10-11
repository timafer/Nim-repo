using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nim.Players;

namespace Nim.CpuVsCpu
{
    public class Game
    {
        public char[][] visual = new char[3][]
        {
            new char[] {'o', 'o', 'o'},
            new char[] {'o', 'o', 'o', 'o', 'o'},
            new char[] {'o', 'o', 'o', 'o', 'o', 'o', 'o'}
        };
        private Player player1;
        private Player player2;
        private LearnCPU learningCPU;
        private List<State> previousStates;
        public int p1Count { get; private set; }
        public int p2Count { get; private set; }

        /// <summary>
        /// Method that starts the CPU game
        /// </summary>
        /// 

        public Game()
        {
            learningCPU = new LearnCPU(visual);
        }

        private void SetGameMode(int s)
        {
            switch (s)
            {
                case 1:
                    player1 = new UserPlayer(visual);
                    player2 = new UserPlayer(visual);
                    break;
                case 2:
                    player1 = new UserPlayer(visual);
                    player2 = new RandCpu(visual);
                    break;
                case 3:
                    player1 = new RandCpu(visual);
                    player2 = new RandCpu(visual);
                    break;
                case 4:
                    player1 = new RandCpu(visual);
                    player2 = learningCPU;
                    break;
                case 5:
                    player1 = new UserPlayer(visual);
                    player2 = learningCPU;
                    break;
            }
        }

        public void Start(int selection, int sleepCounter)
        {
            bool isP1Turn = true;
            previousStates = new List<State>();
            SetGameMode(selection);
            Console.WriteLine(PrintBoard());
            bool gameOver = false;
            do
            {
                if (player1.GetType() != typeof(UserPlayer))
                {
                    Console.Clear();
                }
                MakeMoves(isP1Turn);
                isP1Turn = !isP1Turn;
                player1.setRandomBounds();
                player2.setRandomBounds();
                gameOver = CheckGameOver();
                Console.WriteLine(PrintBoard());
                System.Threading.Thread.Sleep(sleepCounter);
            }
            while (!gameOver);

            bool p1IsWinner = isP1Turn;

            if (player2.GetType() == typeof(LearnCPU))
            {
                RateMoves();
            }

            if (p1IsWinner)
            {
                Console.WriteLine("Player 1 is the winner");
                p1Count++;
            }
            else
            {
                Console.WriteLine("Player 2 is the winner");
                p2Count++;
            }

            visual = new char[3][]
            {
            new char[] {'o', 'o', 'o'},
            new char[] {'o', 'o', 'o', 'o', 'o'},
            new char[] {'o', 'o', 'o', 'o', 'o', 'o', 'o'}
            };

            player1.ResetBoard(visual);
            player2.ResetBoard(visual);
            player1.resetBounds();
            player2.resetBounds();
        }

        /// <summary>
        /// Checks if the game over was triggered
        /// </summary>
        /// <returns>returns true if game is over otherwise false</returns>
        public bool CheckGameOver()
        {
            int counter = 0;

            foreach (char[] row in visual)
            {
                foreach (char peg in row)
                {
                    if (peg == 'o')
                    {
                        counter++;
                    }
                }
            }

            if (counter > 0)
            {
                return false;
            }
            else
            {
                return true;
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
                ((LearnCPU)player2).AddMove(temp);
            }
        }

        public char[][] CopyArray(char[][] array)
        {
            return array.Select(s => s.ToArray()).ToArray();
        }

        /// <summary>
        /// Checks if the row contians the amount to be removed
        /// </summary>
        /// <param name="row">Row that is checked</param>
        /// <param name="removeAmount">Amount the player wished to remove</param>
        /// <returns>If row contains enough to be removed</returns>
        public bool CheckRow(int row, int removeAmount)
        {
            int counter = 0;
            foreach (char peg in visual[row])
            {
                if (peg == 'o')
                {
                    counter++;
                }
            }

            if (counter >= removeAmount && removeAmount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void PlayerTakeTurn(string playerName, Player player)
        {
            Console.WriteLine(playerName + " Turn");
            bool isVaildMove = false;
            int[] move = null;
            do
            {
                move = player.ChooseMove();
                isVaildMove = CheckRow(move[0], move[1]);
            }
            while (!isVaildMove);

            if (player2.GetType() == typeof(LearnCPU))
            {
                char[][] arrayCopy = CopyArray(visual);
                previousStates.Add(new State(move, 0, arrayCopy));
            }

            int numRemoved = 0;
            int position = 0;
            int row = move[0];
            int removeAmount = move[1];
            while (numRemoved != removeAmount)
            {
                if (visual[row][position] == 'o')
                {
                    visual[row][position] = 'x';
                    numRemoved++;
                    position++;
                }
                else
                {
                    position++;
                }
            }

            Console.WriteLine(playerName + " removed " + removeAmount + " from row " + RowIntToChar(row) + ".");
        }
        /// <summary>
        /// Calls cpus to make their respective moves
        /// </summary>
        public void MakeMoves(bool isP1Turn)
        {
            if (isP1Turn)
            {
                PlayerTakeTurn("Player 1", player1);
            }
            else
            {
                PlayerTakeTurn("Player 2", player2);
            }

        }

        /// <summary>
        /// creates a string of the board that is later printed out
        /// </summary>
        /// <returns>string to be printed out</returns>
        public string PrintBoard()
        {
            int rowLabel = 0;
            string output = "";
            for (int i = 0; i < 3; i++)
            {
                output += RowIntToChar(rowLabel) + " ";
                rowLabel++;
                for (int j = 0; j < visual[i].Count(); j++)
                {
                    output += visual[i][j];
                }
                output += "\n";
            }
            return output;
        }

        private char RowIntToChar(int r)//Changes between A,B,C to 0,1,2
        {
            char row = 'z';
            switch (r)
            {
                case 0:
                    row = 'A';
                    break;
                case 1:
                    row = 'B';
                    break;
                case 2:
                    row = 'C';
                    break;
            }
            return row;
        }
        public void resetStats()
        {
            p1Count = 0;
            p2Count = 0;
        }

    }
}
