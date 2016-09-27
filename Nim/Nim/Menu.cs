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
                    Nim pvp = new Nim();
                    pvp.Start();
                    break;
                case 2:
                    break;
                case 3:
                    Game cvc = new Game();
                    cvc.Start();
                    break;
                case 4:
                    break;
                default:
                    Console.WriteLine("ERROR:Coder error bad selection");
                    break;
            }
        }
    }
}
