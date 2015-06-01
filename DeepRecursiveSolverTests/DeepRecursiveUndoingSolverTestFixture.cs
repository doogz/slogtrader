using NUnit.Framework;

namespace ScottLogic.NumbersGame.ReferenceAlgorithms.Tests
{
    [TestFixture]
    public class DeepRecursiveUndoingSolverTestFixture
    {
        private ISolution _solution;
        private IGameSolver _solver;

        [SetUp]
        public void Setup()
        {
            _solver = new DeepRecursiveUndoingSolver();
        }

        [TearDown]
        public void Teardown()
        {
            _solver = null;
        }


        [Test]
        public void SimpleAddition()
        {
            bool solved = _solver.GetSolution(new []{3,7}, 10, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }

        [Test]
        public void SimpleSubtraction()
        {
            bool solved = _solver.GetSolution(new [] {7, 3}, 4, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }
        [Test]
        public void SimpleSubtraction_NumbersInverted()
        {
            bool solved = _solver.GetSolution(new [] { 3, 7}, 4, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }

        
        [Test]
        public void SimpleMultiplication()
        {
            bool solved = _solver.GetSolution( new[] {3, 7}, 21, out _solution);

            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }
        
        [Test]
        public void SimpleDivision()
        {
            bool solved = _solver.GetSolution(new[] {75, 25}, 3, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }

        [Test]
        public void MultiplicationAndAddition()
        {
            bool solved = _solver.GetSolution(new[] { 2, 8, 10,}, 82, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, _solution.NumberOfOperations);
            
        }
        [Test]
        public void DivisionAndSubtraction()
        {
            bool solved = _solver.GetSolution(new[] { 10, 50, 1}, 4, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, _solution.NumberOfOperations);
        }

        [Test]
        public void AlreadySolved()
        {
            bool solved = _solver.GetSolution(new[]{10}, 10, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(0, _solution.NumberOfOperations);
        }

        public void Unsolvable()
        {
            bool solved = _solver.GetSolution(new[]{1,2}, 5, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(0, _solution.NumberOfOperations);
        }
    }
}
