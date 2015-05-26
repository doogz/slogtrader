using System;
using System.Collections.Generic;
using NumbersGameSdk;
using NUnit.Framework;

namespace NumbersGameTests
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

            solution = new Solution(ops);
            expectedDisplayString = "2 + 1 = 3" + Environment.NewLine +
                                    "4 - 3 = 1" + Environment.NewLine +
                                    "6 x 5 = 30" + Environment.NewLine +
                                    "72 \x00F7 8 = 9" + Environment.NewLine;
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
