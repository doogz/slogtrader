using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownNumbersGameSolver
{
    /// <summary>
    /// ISolution models the output of a countdown numbers game solver.
    /// A solution is just an ordered list of moves, modeled by the IMove interface.
    /// </summary>
    public interface ISolution
    {
        string DisplayString { get; }
        int NumberOfMoves{ get; }
        IMove GetMove(int idx);
    }
}
