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
        public int[] visual;
        public Player player1;
        public Player player2;
        /// <summary>
        /// Method that starts the CPU game
        /// </summary>
        /// 
        public Game(Player p1,Player p2,int[] board)
        {
            player1 = p1;
            player2 = p2;
            visual = board;
        }
        public void Start(int sleepCounter)
        {
            MoveControl Movecontrol = new MoveControl(player2);
            bool isP1Turn = true;
            Console.WriteLine(PrintBoard());
            bool gameOver = false;
            do
            {
                if (player1.GetType() != typeof(UserPlayer))
                {
                    Console.Clear();
                }
                MakeMoves(isP1Turn,Movecontrol);
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
                Movecontrol.RateMoves();
            }

            if (p1IsWinner)
            {
                Console.WriteLine("Player 1 is the winner");
                player1.wincount++;
            }
            else
            {
                Console.WriteLine("Player 2 is the winner");
                player2.wincount++;
            }

            visual = new int[]
            {
            3,5,7
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

            foreach (int row in visual)
            {

                counter += row;
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
        /// <summary>
        /// Calls cpus to make their respective moves
        /// </summary>
        public void MakeMoves(bool isP1Turn,MoveControl mc)
        {
            if (isP1Turn)
            {
                mc.PlayerTakeTurn(player1,visual);
            }
            else
            {
                mc.PlayerTakeTurn(player2,visual);
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
            int repeat = 3;
            for (int i = 0; i < 3; i++)
            {
                output += (char)(rowLabel+65) + " ";
                rowLabel++;
                int ocount = 0;                
                for (int j = 0; j < repeat; j++)
                {
                    ocount = visual[i];
                    if (j<ocount)
                    {
                        output += "o";
                    }
                    else
                    {
                        output += "x";
                    }
                }
                repeat += 2;
                ocount = 0;
                output += "\n";
            }
            return output;
        }
    }
}
