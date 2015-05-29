namespace ScottLogic.NumbersGame
{
    /// <summary>
    /// IGameSolver defines a protocol for interacting with a Countdown Numbers Game-solving algorithm.
    /// Game solvers are required to support several different goals as follows:
    /// 1. Finding a working solution and report it as quickly as possible.
    /// 2. Find the shortest solution possible.
    /// 3. Find a solution that uses up every number.
    /// To get us started, let's just focus on the GetFirstSolution() method, and then get into some WPF.
    /// 
    /// Kudos (but no extra points) for using up every one of the numbers - GetExhaustiveSolution if you're a nerd!
    /// </summary>
    public interface IGameSolver
    {
        /// <summary>
        /// Provides a solution to the caller via the out-parameter. Returns false if no solution to be found, true otherwise.
        /// </summary>
        /// <param name="inputNumbers">An unsorted array of numbers representing the starting numbers</param>
        /// <param name="target">The target of this numbers game</param>
        /// <param name="solution">Receives solution</param>
        /// <returns>Returns true if a solution was found, false otherwise</returns>
        bool GetSolution(int[] inputNumbers, int target, out ISolution solution);

        //bool GetFirstSolution(NumbersGame game, out ISolution solution);
        
        /// <summary>
        /// Writes the shortest (in terms of moves) solution discovered into 'solution' out-parameter. Returns false if no solution to be found.
        /// </summary>
        /// <param name="inputNumbers"></param>
        /// <param name="target"></param>
        /// <param name="solution"></param>
        /// <returns>Returns true if a solution was found, false otherwise</returns>
        bool GetShortestSolution(int[] inputNumbers, int target, out ISolution solution);
    }
}
