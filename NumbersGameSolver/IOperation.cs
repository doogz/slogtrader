namespace NumbersGameSdk
{
    /// <summary>
    /// IOperation represents a single combination of two numbers with an appropriate operator in the countdown numbers game.
    /// </summary>
    public interface IOperation
    {
        int FirstOperand { get; }
        Operator Operator { get; }
        int SecondOperand { get; }
        string DisplayString { get; }
    }
}
