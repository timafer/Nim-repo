using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim.CpuVsCpu
{
    [Serializable]
    public class StateStore
    {
        public List<State> learnedMoves { get; set; }
        public StateStore(List<State> Moves)
        {
            learnedMoves = Moves;
        }
    }
}
