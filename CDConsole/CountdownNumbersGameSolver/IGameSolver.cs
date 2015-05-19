using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountdownNumbersGameSolver
{
    /// <summary>
    /// IGameSolver defines the protocol for interacting with a countdown numbers game-solving algorithm.
    /// 
    /// When the speed of finding a working solution is our primary goal, the interface provides GetFirstSoltion. This method should return
    /// as soon as any valid solution is found.
    /// 
    /// When wishing to find the most efficient solution is more important, the interface provides GetShortestSolution.
    /// </summary>
    public interface IGameSolver
    {
        /// <summary>
        /// Writes first solution encountered into solution out-parameter. Returns false if no solution to be found (true otherwise).
        /// </summary>
        /// <param name="inputNumbers">An unsorted array of numbers representing the starting numbers</param>
        /// <param name="target">The target of this numbers game</param>
        /// <param name="solution">Receives solution</param>
        /// <returns></returns>
        bool GetFirstSolution(int[] inputNumbers, int target, out ISolution solution);
        
        /// <summary>
        /// Writes the shortest (in terms of moves) solution encountered into 'solution' out-parameter. Returns false if no solution to be found.
        /// </summary>
        /// <param name="inputNumbers"></param>
        /// <param name="target"></param>
        /// <param name="solution"></param>
        /// <returns></returns>
        bool GetShortestSolution(int[] inputNumbers, int target, out ISolution solution);
    }
}
