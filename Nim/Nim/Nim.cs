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

        public void InitializeBoard()
        {
            for(int r = 0; r < visual.Length; r++)
            {
                int columns = (r * 2) + 3;
                for(int c = 0; c < columns; c++)
                {
                    visual[r, c] = 'o';
                }
            }
        }
    }
}
