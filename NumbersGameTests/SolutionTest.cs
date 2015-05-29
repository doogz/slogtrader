using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScottLogic.NumbersGame;

namespace ScottLogic.NumbersGameTests
{
    [TestFixture]
    public class SolutionTest
    {
        private ISolution solution;
        private string expectedDisplayString;

        [SetUp]
        public void Setup()
        {
            IEnumerable<IOperation> ops = new IOperation[]
            {
                new Operation(1, 2, Operator.Addition),
                new Operation(3, 4, Operator.Subtraction),
                new Operation(5, 6, Operator.Multiplication),
                new Operation(72, 8, Operator.Division)
            };

            solution = new Solution(ops, new TimeSpan(hours: 0, minutes: 0, seconds: 1));
            expectedDisplayString = string.Format("2 + 1 = 3{0}4 - 3 = 1{0}6 x 5 = 30{0}72 \x00F7 8 = 9{0}", Environment.NewLine);
        }

        [TearDown]
        public void Teardown()
        {
            solution = null;
        }

        [Test]
        public void NumberOfOperations()
        {
            Assert.AreEqual(4, solution.NumberOfOperations);
        }

        [Test]
        public void DisplayString()
        {
            Assert.AreEqual(expectedDisplayString, solution.GetMultilineDisplayString(), "Checked DisplayString for all four operations!");
        }

    }
}
