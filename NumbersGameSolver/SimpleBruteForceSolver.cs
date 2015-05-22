using System;
using System.Collections.Generic;
using System.Linq;

namespace NumbersGameSolver
{
    /// <summary>
    /// Implements a game solver (IGameSolver) based on an exhaustive "brute force" approach.
    /// Every valid operation between every pair of numbers is tried in turn, and the process is applied recursively.
    
    /// For example, any given game position (set of available numbers), may contain multiple instances of the same number.
    /// This implementation will mindlessly explore all permutations, including those it has already explored using a different instance of the same number.
    /// 
    /// Also, this solution is exclusively single-threaded, leaving much room for improvement on multi-core machines.
    /// </summary>
    public class SimpleBruteForceSolver : IGameSolver
    {
        private readonly List<NumbersGame> _wip = new List<NumbersGame>(60); // work in progress

        public bool GetFirstSolution(int[] inputNumbers, int target, out ISolution solution)
        {
            solution = null;
            var t0 = DateTime.Now;
            var initialNumbers = new NumbersGame(inputNumbers);
            _wip.Add(initialNumbers);

            while (_wip.Any())
            {
                // Process exactly the lists we had from the last iteration first
                int lists = _wip.Count;
                for (int idx = 0; idx < lists; ++idx)
                {
                    NumbersGame game = _wip[idx];
                    if (game.Contains(target))
                    {
                        solution = new Solution(game.History);
                        return true;
                    }

                    if (game.HasDerivatives)
                    {
                        var derivatives = game.GenerateIntermediates();
                        foreach (var v in derivatives)
                        {
                            _wip.Add(v);
                        }
                        
                    }
                }
                // We haven't found any. We need to erase (0,lists] now. RemoveRange does exactly what we need.
                _wip.RemoveRange(0, lists );
            }
            var t1 = System.DateTime.Now;
            var dt = t1 - t0;
            Console.WriteLine("Exhausting the possibilities took {0}.{1} seconds", dt.Seconds, dt.Milliseconds);
            return false;
    
        }

        public bool GetShortestSolution(int[] inputNumbers, int target, out ISolution solution)
        {
            throw new NotImplementedException();
        }

    }
}

