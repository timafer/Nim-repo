using Nim.CpuVsCpu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim
{
    class Menu
    {
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
                    Nim pvp = new Nim();
                    pvp.Start();
                    break;
                case 2:
                    Console.WriteLine("Starting Player vs Computer");
                    break;
                case 3:
                    Console.WriteLine("Starting Computer vs Computer");
                    Game cvc = new Game();
                    cvc.Start();
                    break;
                case 4:
                    Console.WriteLine("Starting Computer vs Smart Computer");
                    break;
                default:
                    Console.WriteLine("ERROR:Coder error bad selection");
                    break;
            }
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
