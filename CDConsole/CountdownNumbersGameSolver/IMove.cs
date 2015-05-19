namespace CountdownNumbersGameSolver
{
    /// <summary>
    /// IMove represents a single 'move' in the countdown numbers game.
    /// It involves two of the existing numbers being combined with a given operation.
    /// </summary>
    public interface IMove
    {
        string DisplayString { get; }
        string FirstOperand { get; }
        Operation Operation { get; }
        string SecondOperand { get; }
    }
}
