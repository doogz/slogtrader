using NumbersGameSdk;

namespace SimpleGameEngine
{
    /// <summary>
    /// The IGame interface defines the protocol for playing a numbers game, within the context of the NumbersGameSdk.
    /// Each game is initialised with a fixed-length array of starting numbers, and a target, using the Initialise() method.
    /// The game proceeds by applying discreet operations, using DoOperation(). 
    /// As not all operations are valid at all times, the interface provides a checking method, IsOperationValid()
    /// the interface specifies a TryOperation() method, which returns true if it succeeds and false if it fails.
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
        /// Determines whether a given operation is valid in the context of the current game.
        /// </summary>
        /// <param name="op">The operation being checked</param>
        /// <returns>
        /// Method returns true if the operation is valid, false otherwise
        /// </returns>
        bool IsOperationValid(IOperation op);

        /// <summary>
        /// Applies a given operation to the current game.
        /// If the operation is not valid, a TODO: exception_type is thrown.
        /// </summary>
        void DoOperation(IOperation op);
        
        /// <summary>
        /// Undoes the last operation, restoring the game state to what it was before the last DoOperation().
        /// If there are no prior operations (e.g. directly after Initialise() is called), a TODO: exception_type is thown.
        /// </summary>
        void UndoOperation();
        
        /// <summary>
        /// Creates a new game instance from the current game state and the given operation.
        /// If the operation is not valid, an InvalidOperationException is thrown.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        INumbersGame CreateDescendent(IOperation op);

        /// <summary>
        /// Read-only property reporting whether the game is completed
        /// </summary>
        bool IsSolved { get; }
        
        /// <summary>
        /// Read-only property reporting whether the game is complete.
        /// A game is complete either if it is solved, or if it is exhausted
        /// </summary>
        bool IsComplete { get; }

        bool IsExhausted { get; }

        int NumberCount { get; }

        /// <summary>
        /// Reports the initial collection of numbers that we were given
        /// </summary>
        int[] InitialNumbers { get; }
        
        /// <summary>
        /// Reports the current collection of numbers available, including unused initial values, plus
        /// the unused generated intermediates.
        /// </summary>
        int[] CurrentNumbers { get; }
        
        /// <summary>
        /// The target number to be generated.
        /// </summary>
        int Target { get; }


    }
}
