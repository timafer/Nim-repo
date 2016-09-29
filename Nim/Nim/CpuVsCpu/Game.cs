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
        private List<State> previousStates;
        private bool isP1Turn = true;
        private bool learningCPUOn = false;
        private LearnCPU learningCPU;
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
                    player1 = new UserPlayer();
                    player2 = new UserPlayer();
                    learningCPUOn = false;
                    break;
                case 2:
                    player1 = new UserPlayer();
                    player2 = new RandCpu();
                    learningCPUOn = false;
                    break;
                case 3:
                    player1 = new RandCpu();
                    player2 = new RandCpu();
                    learningCPUOn = false;
                    break;
                case 4:
                    player1 = new RandCpu();
                    player2 = learningCPU;
                    learningCPUOn = true;
                    break;
                case 5:
                    player1 = new UserPlayer();
                    player2 = learningCPU;
                    learningCPUOn = true;
                    break;
            }
        }
        public void Start(int selection, int sleepCounter)
        {
            isP1Turn = true;
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
                MakeMoves();
                gameOver = CheckGameOver();
                Console.WriteLine(PrintBoard());
                System.Threading.Thread.Sleep(sleepCounter);
            }
            while (!gameOver);

            bool p1IsWinner = isP1Turn;

            if (learningCPUOn)
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

            learningCPU.ResetBoard(visual);
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
                if((isP1Turn && typeof(UserPlayer) == player1.GetType()) || (!isP1Turn && typeof(UserPlayer) == player2.GetType()))
                {
                    Console.WriteLine("Error: removeAmount: [" + removeAmount + "] is greater than row [" + RowIntToChar(row) + "] remaining pieces [" + counter + "].");
                }
                return false;
            }

        }

        /// <summary>
        /// Calls cpus to make their respective moves
        /// </summary>
        public void MakeMoves()
        {
            if (isP1Turn)
            {
                Console.WriteLine("Player 1 Turn");
                bool isVaildMove = false;
                int[] move = null;
                do
                {
                    if (player1.GetType() == typeof(UserPlayer))
                    {
                        Console.WriteLine(PrintBoard());
                    }
                    move = player1.ChooseMove();

                    isVaildMove = CheckRow(move[0], move[1]);
                }
                while (!isVaildMove);

                if (learningCPUOn)
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

                Console.WriteLine("Player 1 removed " + removeAmount + " from row " + RowIntToChar(row) + ".");

            }
            else
            {
                Console.WriteLine("Player 2 Turn");
                bool isVaildMove = false;
                int[] move = null;
                do
                {
                    if (player2.GetType() == typeof(UserPlayer))
                    {
                        Console.WriteLine(PrintBoard());
                    }

                    move = player2.ChooseMove();
                    isVaildMove = CheckRow(move[0], move[1]);

                }
                while (!isVaildMove);

                if (learningCPUOn)
                {
                    char[][] arrayCopy = CopyArray(visual);
                    previousStates.Add(new State(move, 0, arrayCopy));
                }

                int numRemoved = 0;
                int position = 0;
                int row = move[0];
                int removeAmount = move[1];
                do
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
                while (numRemoved != removeAmount);

                Console.WriteLine("Player 2 removed " + removeAmount + " from row " + RowIntToChar(row) + ".");
            }

            isP1Turn = !isP1Turn;
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

        public void setRandomBounds()
        {

        }
    }
}
