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
            PromptUser();
            Console.WriteLine(PrintBoard());
        }

        private void PromptUser()//will prompt user for row and number of pieces to take
        {
            char row;
            int piecesToTake;
            string pieces;    
            Console.WriteLine("Please enter which row you want to take from (A,B, or C):: ");
            row = Console.ReadLine().ElementAt(0);
            Console.Write("How many pieces would you like to take from " + row + ":: ");
            pieces = Console.ReadLine();
            Int32.TryParse(pieces, out piecesToTake);
            TakePieces(row, piecesToTake);
        }

        private int CountRow(int r) // counts pieces in row
        {
            int pieceTotal = 0;
            for(int j = 0; j < visual.GetLength(1); j++)
            {
                if(visual[r,j] == 'o')
                {
                    pieceTotal++;
                }
            }
            return pieceTotal;
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

        private void TakePieces(char row, int piecesToRemove)//Removes pieces from board and is basic turn logic
        {
            int rowNum = RowCharToInt(row);
            int rowsPieces = CountRow(rowNum);
            int piecesToTake = piecesToRemove;
            if(piecesToRemove <= rowsPieces)// checks that there are enough pieces to take
            {
                for(int c = visual.GetLength(1) - 1; (c >= 0 && piecesToTake != 0); c--)
                {
                    if (visual[rowNum, c] == 'o')
                    {
                        visual[rowNum, c] = ' ';
                        piecesToTake--;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: cannot take pieces that are not there [" + row + ", " + rowsPieces + "] [row, number of pieces in row]");
            }
        }

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

        public void InitializeBoard()
        {
            for(int r = 0; r < visual.GetLength(0); r++)
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
