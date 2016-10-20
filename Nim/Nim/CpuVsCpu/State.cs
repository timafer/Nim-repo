using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Nim.CpuVsCpu
{
    [Serializable]
    public class State
    {
        public double ValueOfWorth { get; set; }
        public int[] StateOfBoard { get; private set; }
        public int[] MoveMade { get; private set; }

        /// <summary>
        /// The ctor state of the board and move that was made
        /// </summary>
        /// <param name="move">The move that is to be saved</param>
        /// <param name="worth">The value of the move made</param>
        /// <param name="board">The placement of the pieces on the board at the time of the move</param>
        public State(int[] move, double worth, int[] board)
        {
            MoveMade = move;
            StateOfBoard = board;
            ValueOfWorth = worth;
        }
    }
}
