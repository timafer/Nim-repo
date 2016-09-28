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
        int repeat = 0;

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
                Console.WriteLine("Please Select Game mode \n 1:P v P \n 2:P v Cpu \n 3:Cpu v Cpu \n 4:Cpu v Smart Cpu \n 5:P v Smart Cpu");
                string s = Console.ReadLine();
                bool b = int.TryParse(s, out i);
                if (!b || i < 1 || i > 5)
                {
                    Console.WriteLine("ERROR:Invalid input. Please Enter A Number Between 1 and 5");
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
            if (selection == 4)
            {
                bool goodinput = false;
                while (!goodinput)
                {
                    Console.WriteLine("How many games do you want to play:");
                    string s = Console.ReadLine();
                    bool b = int.TryParse(s, out repeat);
                    if (!b || repeat < 1)
                    {
                        Console.WriteLine("ERROR:Invalid input. Please Enter A Number Above 0");
                    }
                    else
                    {
                        goodinput = true;
                    }
                }
            }
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
                    for (int i = 0; i < repeat; i++)
                    {
                        game.Start(selection);
                    }
                    break;
                case 5:
                    Console.WriteLine("Player vs Smart Computer");
                    game.Start(selection);
                    break;
                default:
                    Console.WriteLine("ERROR:Coder error bad selection");
                    break;
            }
            PlayAgain(selection);
        }
        public void PlayAgain(int selection)
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Do you want to play again y/n");
                string s = Console.ReadLine();
                if (char.ToLower(s[0]) == 'y')
                {
                    UseSelection(selection);
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
            bool svalid = false;
            while (!svalid)
            {
                Console.WriteLine("Do you want to make a diffrent selection y/n");
                string s2 = Console.ReadLine();
                if (char.ToLower(s2[0]) == 'y')
                {
                    Select();
                    svalid = true;
                }
                else if (char.ToLower(s2[0]) == 'n')
                {
                    svalid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
