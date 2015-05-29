using System.Collections.Generic;

namespace ScottLogic.NumbersGame.Game
{
    /// <summary>
    /// The INumbersGame interface defines an optional protocol for playing out a numbers game, within the context of the NumbersGameSdk.
    /// Each game is initialised with a fixed-length array of starting numbers, and a target, using the Initialise() method.
    /// The game proceeds by applying discreet operations, using DoOperation(), until the game is solved or exhausted. 
    /// Game playing strategies are provided with an "undo" capability (reversing the effect of the last operation), and
    /// a generational "spawning" capability.
    /// </summary>
    public interface INumbersGame
    {
        /// <summary>
        /// Initialise game with a starting set of values and a target
        /// </summary>
        /// <param name="initialValues"></param>
        /// <param name="target"></param>
        void Initialise(int[] initialValues, int target);

        /// <summary>
        /// Applies a given operation to the current game.
        /// If the operation is not valid, an InvalidOperationException is thrown.
        /// Operations can be checked for validity in advance - see the static Operation.IsValid() method
        /// </summary>
        void DoOperation(IOperation op);
        
        /// <summary>
        /// Undoes the last operation, restoring the game state to what it was before the last DoOperation().
        /// If there are no prior operations (e.g. directly after Initialise() is called), an InvalidOperationException.
        /// The do/undo mechanism is provided to support game playing strategies that try out operations in-place,
        /// and hence require a means of unwinding those changes.
        /// </summary>
        void UndoOperation();
        
        /// <summary>
        /// Creates a new game instance from the current game state and the given operation.
        /// This provides an alternative approach for game playing strategies. Instead of attempting to modify and reverse-modify
        /// game state, we do a deep copy of the current game and then mutate that.
        /// If the operation is not valid, an InvalidOperationException is thrown.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        INumbersGame CreateDescendent(IOperation op);

        List<INumbersGame> CreateAllDescendents();

        bool IsSolved { get; }

        IEnumerable<IOperation> History { get; }
        
        bool IsComplete { get; }

        bool IsExhausted { get; }

        int NumberCount { get; }

        int[] CurrentNumbers { get; }
        /// <summary>
        /// The target number to be generated.
        /// </summary>
        int Target { get; }


    }
}
