using System.Runtime.InteropServices;
using NUnit.Framework;
using ScottLogic.NumbersGame;
using ScottLogic.NumbersGame.Game;
using ScottLogic.NumbersGame.ReferenceAlgorithms;

namespace DeepRecursiveSolverTests
{
    [TestFixture]
    public class UnitTest
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void SolveSimpleAddition()
        {
            var input = new [] {1, 2};
            var target = 3; 
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetSolution(input, target, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, solution.NumberOfOperations);

        }

        [Test]
        public void SolveMultiplicationAndAddition()
        {
            var input = new [] {10, 7};
            int target=24;
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetSolution(input, target, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, solution.NumberOfOperations);

        }

        [Test]
        public void SolveDivisionAndSubtraction()
        {
            var input = new[] {10, 50, 1};
            int target= 4;
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetSolution(input, target, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, solution.NumberOfOperations);

        }

        [Test]
        public void NotSolvable()
        {
            var input = new[] {10, 5};
            int target = 3;
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetSolution(input, target, out solution);
            Assert.AreEqual(false, solved);
        }
    }
}




