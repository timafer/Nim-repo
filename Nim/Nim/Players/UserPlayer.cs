﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim.Players
{
    public class UserPlayer: Player
    {

        public UserPlayer(int[] visual,string name) : base(visual,name)
        {
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

        public override int[] ChooseMove()//will prompt user for row and number of pieces to take
        {
            int[] move = new int[2];
            char row;
            int rowInt;
            int piecesToTake;
            string pieces;
            int maxPieces = 0;
            bool selectRow = true;
            do
            {
                Console.Write("Please enter which row you want to take from (A,B, or C):: ");
                row = Console.ReadLine().ElementAt(0);
                if (row == 'A' || row == 'B' || row == 'C' || row == 'a' || row == 'b' || row == 'c')
                {
                    selectRow = false;
                }
                else
                {
                    Console.WriteLine("Error:: row [" + row + "] is not valid! please true A, B, or C!");
                }
            } while (selectRow);
            bool passed = false;
            rowInt = RowCharToInt(row);
            maxPieces = countRow(rowInt);
            do
            {
                Console.Write("How many pieces would you like to take from " + row + ":: ");
                pieces = Console.ReadLine();
                passed = Int32.TryParse(pieces, out piecesToTake) && (piecesToTake > 0 && piecesToTake <= maxPieces);
                if(!passed)
                {
                    Console.WriteLine("Error: [" + pieces + "] is not valid input please put in a number includeing 1 through "+ maxPieces + "!");
                }
            } while (!passed);
            move[0] = rowInt;
            move[1] = piecesToTake;
            return move;
        }
    }
}
