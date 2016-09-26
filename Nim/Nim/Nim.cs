using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim
{
    public class Nim
    {
        public char[,] visual = new char[3, 7];
        public void Start()
        {
            Console.WriteLine("This is start");
        }

        public string PrintBoard()
        {
            string output = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    output += visual[i, j];
                }
                output += "\n";
            }
            return output;
        }
    }
}
