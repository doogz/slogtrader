using System;
using Microsoft.Practices.Unity;
using NumbersGameSdk;

namespace DeepRecursiveSolver
{
    /// <summary>
    /// In contrast to the progressive recursive solver, the deep recursive solver uses recursion exhaustively from the off.
    /// Whereas the former attempts to find solutions using 1 operation, 2 operations, 3 operations etc - progressively - this
    /// solver resurses to the lowest depths before bubbling back out (and in).
    /// Due to the unprogressive nature of the seatch, it's entirely possible that we come up with a "solution" that contains
    /// unnecessary operations.
    /// </summary>
    public class DeepRecursiveUndoingSolver : IGameSolver
    {
        static DeepRecursiveUndoingSolver()
        {
            // Where to get our IoC container??
            var container = new UnityContainer();

            // Registering...
            container.RegisterType<IGameSolver, DeepRecursiveUndoingSolver>();

            // Resolving...
            var solution = container.Resolve<Solution>();
        }

        public bool GetFirstSolution(int[] inputNumbers, int target, out ISolution solution)
        {
            var game = new NumbersGame(inputNumbers) {Target = target};
            solution = null;
            var t0 = DateTime.Now;
            bool solved = Solve(game);
            var t1 = DateTime.Now;
            var duration = t1 - t0;
            solution = new Solution(game.History);
            return solved;
        }

        public bool GetShortestSolution(int[] inputNumbers, int target, out ISolution solution)
        {
            throw new NotImplementedException();
        }

        private bool Solve(NumbersGame game)
        {
            int numbers = game.NumberCount;
            int maxIdx0 = numbers - 1;
            for (int idx0 = 0; idx0 < maxIdx0; ++idx0)
                for (int idx1 = idx0 + 1; idx1 < numbers; ++idx1)
                    foreach (Operator op in Enum.GetValues(typeof (Operator)))
                    {
                        int lhs, rhs;
                        if (game.TryOperation(idx0, idx1, op, out lhs, out rhs))
                        {
                            if (game.IsSolved) return true;
                            if (game.NumberCount >= 2 && Solve(game)) return true;
                            game.UndoOperation(idx0, idx1, lhs, rhs);
                        }
                    }
            return false;
        }
        
    }
}
