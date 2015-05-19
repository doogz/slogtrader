using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownNumbersGameSolver
{
    /// <summary>
    /// Solver implements
    /// </summary>
    public class SimpleBruteForceSolver : ICountdownSolver
    {
    }

    public interface ICountdownSolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputNumbers"></param>
        /// <param name="target"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        bool GetFirstSolution(int[] inputNumbers, int target, out ISolution);
        bool GetShortestSolution(int[] inputNumbers, int target, out ISolution);
    }
}
