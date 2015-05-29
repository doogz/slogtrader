using System.ComponentModel;

namespace ScottLogic.NumbersGame
{
    /// <summary>
    /// 'Operator' enumerates the (4) different kinds of mathematical operation that can be used in our game.
    /// As far as the official Countdown rules go, these are never going to change.
    /// </summary>
    public enum Operator
    {
        [Description("+")]
        Addition=0,
        [Description("-")]
        Subtraction,
        [Description("x")]
        Multiplication,
        [Description("\x00F7")] //
        Division
    }


}
