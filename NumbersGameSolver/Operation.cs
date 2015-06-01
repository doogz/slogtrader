using System;
using System.ComponentModel;
using System.Reflection;

namespace ScottLogic.NumbersGame
{
    /// <summary>
    /// Encapsulates an immutable operation combining two numbers with an operator, and used to track the history of a game.
    
    /// Note: it's small (12bytes), immutable, not intended to participate in class hierarchies, and therefore a candidate struct.
    /// After some performance testing, the current choice of struct was strongly justified:
    /// Time to solve the "Identity set" {1,1,1,1,1,1} with no Operation history is about 3s
    /// Time to solve it with an Operation history using struct implementation is about 4s
    /// Time to solve it with an Operation history using class implementation is about 8s - about 5 times slower!!
    /// </summary>

    public struct Operation : IOperation
    {
        public int Lhs { get; private set; }
        public int Rhs { get; private set; }
        public Operator Operator { get; private set; }
        public int Result
        {
            get
            {
                int res = Apply(Lhs, Rhs, Operator);
                if (res == 0)
                {
                    throw new InvalidOperationException("The result of this operation is not defined.");
                }
                return res;
            } 
        }

        public Operation(int i, int j, Operator op) : this() //*** Needed IFF struct impl
        {
            // First operand must be >= second in all cases; operators - and / demand it so (within the rules of the game)
            bool firstBigger = i > j;
            Lhs = firstBigger ? i : j;
            Rhs = firstBigger ? j : i;
            Operator = op;
        }

        /// <summary>
        /// Static method for retrieving the description (attribute) of an enumeration value - where one exists.
        /// It could happily be refactored as a general extension method for Enums somewhere. It's pretty handy.
        /// </summary>
        /// <param name="operatorEnum"></param>
        /// <returns></returns>

        static private string OperatorDescription(Enum operatorEnum)
        {
            FieldInfo fi = operatorEnum.GetType().GetField(operatorEnum.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return operatorEnum.ToString();
        }
        public string DisplayString
        {
            get { return String.Format("{0} {1} {2} = {3}", Lhs, OperatorDescription(Operator), Rhs, Result); }
        }

        public static bool IsValid(int n1, int n2, Operator op)
        {
            switch (op)
            {
                case Operator.Addition: // Always valid
                    return true;

                case Operator.Subtraction: // Valid as long as n1 != n2 (that'd return a zero)
                    return n1 != n2;

                case Operator.Multiplication: // Valid as long as n1!=1 && n2!=1 (that'd return n2, or n1 - no progress)
                    if (n1 > 0x7FFF || n2 > 0x7FFF) return false; // Avoid overflow
                    return !(n1 == 1 || n2 == 1);

                case Operator.Division:
                    if (n1 == 1 || n2 == 1) return false;
                    return (n1 >= n2) ? (n1%n2 == 0) : (n2%n1 == 0);
                
                default:
                    return false;


            }
        }
        public static int Apply(int n1, int n2, Operator op)
        {
            switch (op)
            {
                case Operator.Addition:
                    return n1 + n2;

                case Operator.Subtraction:
                    return (n1 >= n2 ? n1 - n2 : n2 - n1);  // Could return a 0

                case Operator.Multiplication:
                    return n1 * n2;

                case Operator.Division:
                    int numerator = Math.Max(n1, n2);
                    int denominator = Math.Min(n1, n2);
                    if (denominator == 0 || (numerator % denominator) != 0)
                    {
                        return 0;
                    }
                    return n1 >= n2 ? n1/n2 : n2/n1;

                default:
                    return 0;
            }

        }
    }
}
