using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim
{
    public class UserPlayer
    {

        private int RowCharToInt(char r)//Changes between A,B,C to 0,1,2
        {
            int row = -1;
            switch (r)
            {
                case 'A':
                    row = 0;
                    break;
                case 'a':
                    row = 0;
                    break;
                case 'B':
                    row = 1;
                    break;
                case 'b':
                    row = 1;
                    break;
                case 'C':
                    row = 2;
                    break;
                case 'c':
                    row = 2;
                    break;
            }
            return row;
        }

        public int[] ChooseMove()//will prompt user for row and number of pieces to take
        {
            int[] move = new int[2];
            char row;
            int rowInt;
            int piecesToTake;
            string pieces;
            Console.WriteLine("Please enter which row you want to take from (A,B, or C):: ");
            row = Console.ReadLine().ElementAt(0);
            Console.Write("How many pieces would you like to take from " + row + ":: ");
            pieces = Console.ReadLine();
            Int32.TryParse(pieces, out piecesToTake);
            rowInt = RowCharToInt(row);
            move[0] = rowInt;
            move[1] = piecesToTake;
            return move;
        }
    }
}
