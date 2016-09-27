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
        private LearnCPU learnP2 = new LearnCPU();
        private List<State> previousStates = new List<State>();
        private bool isP1Turn = true;

        /// <summary>
        /// Method that starts the CPU game
        /// </summary>
        /// 

        private void SetGameMode(int s)
        {
            switch (s)
            {
                case 1:
                    player1 = new UserPlayer();
                    player2 = new UserPlayer();
                    break;
                case 2:
                    player1 = new UserPlayer();
                    player2 = new RandCpu();
                    break;
                case 3:
                    player1 = new RandCpu();
                    player2 = new RandCpu();
                    break;
                    //needs a case four no AI yet
            }
        }
        public void Start(int selection)
        {
            SetGameMode(selection);
            Console.WriteLine("This is start");
            Console.WriteLine(PrintBoard());
            bool gameOver = false;
            do
            {
                Console.Clear();
                MakeMoves();
                gameOver = CheckGameOver();
                Console.WriteLine(PrintBoard());
                System.Threading.Thread.Sleep(500);
            }
            while (!gameOver);

            bool p1IsWinner = isP1Turn;

            RateMoves();

            if (p1IsWinner)
            {
                Console.WriteLine("Player 1 is the winner");
            }
            else
            {
                Console.WriteLine("Player 2 is the winner");
            }
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
                learnP2.AddMove(temp);
            }
        }

        public char[][] CopyArrayLinq(char[][] source)
        {
            return source.Select(s => s.ToArray()).ToArray();
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

            if (counter >= removeAmount)
            {
                return true;
            }
            else
            {
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

                char[][] copyArray = CopyArrayLinq(visual);
                previousStates.Add(new State(move, 0, copyArray));

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

                char[][] copyArray = CopyArrayLinq(visual);
                previousStates.Add(new State(move, 0, copyArray));

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
    }
}
