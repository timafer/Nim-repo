using Nim.CpuVsCpu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim
{
    public class Menu
    {
        private Game game;

        public Menu(Game game)
        {
            this.game = game;
        }

        public void Select()
        {
            int i = 0;
            bool goodinput = false;
            while (!goodinput)
            {
                Console.WriteLine("Please Select Game mode \n 1:P v P \n 2:P v Cpu \n 3:Cpu v Cpu \n 4:Cpu v Smart Cpu");
                string s = Console.ReadLine();
                bool b = int.TryParse(s, out i);
                if (!b || i < 1 || i > 4)
                {
                    Console.WriteLine("ERROR:Invalid input. Please Enter A Number Between 1 and 4");
                }
                else
                {
                    goodinput = true;
                }
            }
            UseSelection(i);

        }
        public void UseSelection(int selection)
        {
            switch (selection)
            {
                case 1:
                    Console.WriteLine("Starting Player vs Player");
                    game.Start(selection);
                    break;
                case 2:
                    Console.WriteLine("Starting Player vs Computer");
                    game.Start(selection);
                    break;
                case 3:
                    Console.WriteLine("Starting Computer vs Computer");
                    game.Start(selection);
                    break;
                case 4:
                    Console.WriteLine("Starting Computer vs Smart Computer");
                    game.Start(selection);
                    showStats(game.p1Count, game.p2Count);
                    break;
                default:
                    Console.WriteLine("ERROR:Coder error bad selection");
                    break;
            }
            PlayAgain();
        }

        private void showStats(int p1, int p2)
        {
            Console.WriteLine("Player 1 (Random CPU) won " + p1 + " times.\n Player 2 (Smart CPU) won " + p2 + " times.");
            Console.WriteLine("Stats - Player 1 won " + (double)((p1/repeat) * 100) + 
                "% of the games.\nPlayer 2 won " + (double)((p2/repeat) * 100) + "% of the games.");
        }

        public void PlayAgain()
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Do you want to make a diffrent selection y/n");
                string s = Console.ReadLine();
                if (char.ToLower(s[0]) == 'y')
                {
                    Select();
                    valid = true;
                }
                else if (char.ToLower(s[0]) == 'n')
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
