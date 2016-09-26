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
            InitializeBoard();
            Console.WriteLine(PrintBoard());
        }

        public string PrintBoard()
        {
            char rowLabel = 'A';
            string output = "";
            for (int i = 0; i < 3; i++)
            {
                output += rowLabel++ + " ";
                for (int j = 0; j < 7; j++)
                {
                    output += visual[i, j];
                }
                output += "\n";
            }
            return output;
        }
        public void InitializeBoard()
        {
            for (int r = 0; r < visual.GetLength(0); r++)
            {
                int columns = (r * 2) + 3;
                for (int c = 0; c < columns; c++)
                {
                    visual[r, c] = 'o';
                }
            }
        }
    }
}
