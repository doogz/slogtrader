using System;

namespace ScottLogic.NumbersGame
{
    /// <summary>
    /// ISolution represents the description of a correctly-solved numbers game.
    /// It supports read-only access to an indexed list of operations, modeled by IOperation,
    /// and provides some additional details of the solution via a few properties.
    /// </summary>
    public interface ISolution
    {
        /// <summary>
        /// Generates a multi-line string for display on the host platform.
        /// </summary>
        string GetMultilineDisplayString();

        int NumberOfOperations{ get; }
        
        IOperation this[int idx] { get; }
        
        TimeSpan ExecutionTime { get; }
        
        bool Multithreaded { get; }
    }
}
