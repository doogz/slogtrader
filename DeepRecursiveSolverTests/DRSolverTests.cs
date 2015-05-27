using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepRecursiveSolver;
using NumbersGameSdk;
using NUnit.Framework;

//TODO: How about putting unit tests inside nested Tests or UnitTest(s) namespace -?
//namespace DeepRecursiveSolver.Tests
namespace DeepRecursiveSolverTests
{
    [TestFixture]
    public class DRSolverTests
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
            var game = new NumbersGame(new int[] {3, 7}, 10);
            bool solved = _solver.GetFirstSolution(game, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }

        [Test]
        public void SimpleSubtraction()
        {
            var game = new NumbersGame(new int[] {7, 3}, 4);
            bool solved = _solver.GetFirstSolution(game, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }
        [Test]
        public void SimpleSubtraction_NumbersInverted()
        {
            var game = new NumbersGame(new int[] { 3, 7}, 4);
            bool solved = _solver.GetFirstSolution(game, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }

        
        [Test]
        public void SimpleMultiplication()
        {
            bool solved = _solver.GetFirstSolution( 
                            new NumbersGame(new[] {3, 7}, 21), 
                            out _solution);

            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }
        
        [Test]
        public void SimpleDivision()
        {
            var game = new NumbersGame(new[] {75, 25}, 3);
            bool solved = _solver.GetFirstSolution(game, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, _solution.NumberOfOperations);
        }

        [Test]
        public void MultiplicationAndAddition()
        {
            bool solved = _solver.GetFirstSolution(
                new NumbersGame(new[] { 2, 8, 10,}, 82), // (8*10)+2
                out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, _solution.NumberOfOperations);
            
        }
        [Test]
        public void DivisionAndSubtraction()
        {
            var game = new NumbersGame(new[] { 10, 50, 1}, 4); // (50/10)-1
            bool solved = _solver.GetFirstSolution(game, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, _solution.NumberOfOperations);
            
        }

        [Test]
        public void AlreadySolved()
        {
            var game = new NumbersGame(new[] {10}, 10);
            bool solved = _solver.GetFirstSolution(game, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(0, _solution.NumberOfOperations);

        }

        public void Unsolvable()
        {
            var game = new NumbersGame(new[] {1, 2}, 5);
            bool solved = _solver.GetFirstSolution(game, out _solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(0, _solution.NumberOfOperations);
        }
    }
}
