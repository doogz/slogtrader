using System;
using NUnit.Framework;
using ScottLogic.NumbersGame;

namespace ScottLogic.NumbersGameTests
{
    [TestFixture]
    public class OperationTestFixture
    {
        [Test]
        public void Addition()
        {
            var add = new Operation(10, 20, Operator.Addition);
            // First check operands are ordered correctly - they're provided in the "wrong" order in the ctor
            Assert.AreEqual(20, add.Lhs);
            // Now check the operation
            Assert.AreEqual(30, add.Result);
        }

        [Test]
        public void Subtraction()
        {
            var sub = new Operation(9, 99, Operator.Subtraction);
            Assert.AreEqual(9, sub.Rhs);
            Assert.AreEqual(90, sub.Result);
        }

        [Test]
        public void Multiplication()
        {
            var mul = new Operation(25, 4, Operator.Multiplication);
            Assert.AreEqual(25, mul.Lhs);
            Assert.AreEqual(4, mul.Rhs);
            Assert.AreEqual(100, mul.Result);
            Assert.AreEqual("25 x 4 = 100", mul.DisplayString);
        }

        [Test]
        public void Division()
        {
            var div = new Operation(25,5, Operator.Division);
            Assert.AreEqual(5, div.Result);
            div = new Operation(6, 36, Operator.Division);
            Assert.AreEqual(6, div.Result);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void InvalidDivision()
        {
            var div = new Operation(20, 7, Operator.Division); // 20/7 is not a whole number, you're not allowed to do this
            var res = div.Result; // Exception expected
            Assert.AreNotEqual(0, res);
        }
    }
}
