namespace ScottLogic.NumbersGame
{
    /// <summary>
    /// IOperation represents the combination of two numbers with an appropriate operator to yield a result.
    /// Each operation therefore consumes two numbers and generates one new one.
    /// </summary>
    public interface IOperation
    {
        int FirstOperand { get; }
        Operator Operator { get; }
        int SecondOperand { get; }
        int Result { get; }
        string DisplayString { get; }
    }
}
