using System;
using System.Diagnostics;

namespace ScottLogic.NumbersGame.ReferenceAlgorithms
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
#if UsingUnityYet
        static DeepRecursiveUndoingSolver()
        {
            // Where to get our IoC container??
            
            var container = new UnityContainer();

            // Registering...
            container.RegisterType<IGameSolver, DeepRecursiveUndoingSolver>();

            // Resolving...
            var solution = container.Resolve<Solution>();
        }
#endif
        public bool GetSolution(int[] inputNumbers, int target, out ISolution solution)
        {
            var initialNumbers = new Game.NumbersGame(inputNumbers, target);
            return GetSolution(initialNumbers, out solution);
        }

        private bool GetSolution(Game.NumbersGame game, out ISolution solution)
        {
            var timer = Stopwatch.StartNew();
            bool solved = Solve(game);
            solution = new Solution(game.History, timer.Elapsed);
            return solved;
        }


        private bool Solve(Game.NumbersGame game)
        {
            if (game.IsSolved) return true;

            int numbers = game.NumberCount;
            int maxIdx0 = numbers - 1;
            for (int idx0 = 0; idx0 < maxIdx0; ++idx0)
            {
                int n1 = game.GetNumber(idx0);
                for (int idx1 = idx0 + 1; idx1 < numbers; ++idx1)
                {
                    int n2 = game.GetNumber(idx1);
                    foreach (Operator op in Enum.GetValues(typeof (Operator)))
                    {
                        if (Operation.IsValid(n1, n2, op))
                        {
                            game.DoOperation(new Operation(n1, n2, op));
                            if (game.IsSolved) return true;
                            if (game.NumberCount >= 2 && Solve(game)) return true;
                            game.UndoOperation();
                        }
                    }

                }
            }
            return false;
        }
        public bool GetShortestSolution(int[] inputNumbers, int target, out ISolution solution)
        {
            throw new NotImplementedException();
        }
        
    }
}
