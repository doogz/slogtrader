using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NumbersGameSdk
{
    /*
    public enum Platform
    {
        Native=0,
        Windows,
        Mac,
        Linux
    }
     * */

    /// <summary>
    /// ISolution models the output of a countdown numbers game solver.
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
        
        IOperation this[int idx] { get; } // My first indexer. Seriously! This used to be a GetOperation(int). Is it reasonable/best practice to provide both?
        
        double ExecutionTime { get; }
        
        bool Multithreaded { get; }
    }
}
