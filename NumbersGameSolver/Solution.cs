using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NumbersGameSdk
{
    /// <summary>
    /// Library implementation of ISolution based on an ordered, indexed list of operations.
    /// New 'solution' implementors are to provide IGameSolver types, and can reuse this class for returning their results.
    /// Clients further down the chain seeing only ISolution don't get to modify properties, but do get read access via the interface
    /// </summary>

    public class Solution : ISolution
    {
        /// <summary>
        /// Game solvers are expected to support three different requirements
        /// </summary>
        public enum Goal
        {
            [Description("First solution found")]
            FirstFound,
            [Description("Fewest number of operations")]
            FewestOperations,
            [Description("Using all numbers")]
            UseAllNumbers
        }

        private readonly List<IOperation> _operations = new List<IOperation>();

        public Solution(IEnumerable<IOperation> operations)
        {
            _operations.AddRange(operations);
        }
        /// <summary>
        /// Implements ISolution.GetMultilineDisplayString
        /// Note: This method returns a platform-specific multi-line string.
        /// That's fine for display on the same machine, but not necessarily over the wire.
        /// </summary>
        public string GetMultilineDisplayString()
        {
            
            // There are only a few steps (five or less), so this multiple concatenation isn't too bad itself, especially with a string builder.
            // Note how both solution and operation have this as a method now, rather than a property. 
            var sb=new StringBuilder();
            foreach (var op in _operations)
                sb.AppendLine(op.DisplayString);

            return sb.ToString();
        }
        /// <summary>
        /// Public Utility method for individually adding operations. 
        /// Intended for solutions with a custom data structure - a node based one is going to be way faster, and is intended for delivery in a later phase
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public void AddOperation(IOperation operation)
        {
            _operations.Add(operation); // 
        }
        /// <summary>
        /// Implements ISolution.NumberOfOperations
        /// </summary>
        public int NumberOfOperations
        {
            get { return _operations.Count; }
        }

        /// <summary>
        /// Public indexer for the operations leading to the solution.
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public IOperation this[int idx]
        {
            get { return _operations[idx]; }
            set { _operations[idx] = value; }
        }
        /// <summary>
        /// Public execution time property
        /// The value presented is the real-world time for generating an answer, not the speed for a single core.
        /// The property is settable from clients of Solution, but only gettable from the interface
        /// </summary>
        public double ExecutionTime { get; set; }

        /// <summary>
        /// Public multithreaded flag as property. Set accessor from within Solution, but not ISolution.
        /// </summary>
        public bool Multithreaded { get; set; }

    }
}
