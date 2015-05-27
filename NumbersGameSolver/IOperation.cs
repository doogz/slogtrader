namespace NumbersGameSdk
{
    /// <summary>
    /// IOperation represents a single combination of two numbers with an appropriate operator in the countdown numbers game.
    /// At a bare minimum, we want to show the working out for intermediate values.
    /// As a possible extension, we could identify the whereabouts of the values we're using i.e. "Take the 1 at the beginning of the set of numbers, and the 3 at the third position..."
    /// </summary>
    public interface IOperation
    {
        int FirstOperand { get; }
        Operator Operator { get; }
        int SecondOperand { get; }
        string DisplayString { get; }
    }
}
