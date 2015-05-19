using System.ComponentModel;

namespace CountdownNumbersGameSolver
{
    /// <summary>
    /// Operation enumerates the (4) different kinds of mathematical operation that can be used in our game.
    /// </summary>
    public enum Operation
    {
        [Description(" + ")]
        Addition=0,
        [Description(" - ")]
        Subtraction,
        [Description(" X ")]
        Multiplication,
        [Description(" \x00F7 ")]
        Division
    }
}
