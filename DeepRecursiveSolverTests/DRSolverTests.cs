using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepRecursiveSolver;
using NumbersGameSdk;
using NUnit.Framework;

namespace DeepRecursiveSolverTests
{
    [TestFixture]
    public class DRSolverTests
    {
        [Test]
        public void SolveSimpleAddition()
        {
            var game = new NumbersGame(new int[] {3, 7}, 10);
            var solver = new DeepRecursiveUndoingSolver();
            ISolution solution;
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, solution.NumberOfOperations);
        }

        [Test]
        public void SimpleSubtraction()
        {
            var game = new NumbersGame(new int[] {7, 3}, 4);
            var solver = new DeepRecursiveUndoingSolver();
            ISolution solution;
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, solution.NumberOfOperations);
        }
        public void SimpleSubtractionNumbersInverted()
        {
            var game = new NumbersGame(new int[] { 3, 7}, 4);
            var solver = new DeepRecursiveUndoingSolver();
            ISolution solution;
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, solution.NumberOfOperations);
        }

        
        [Test]
        public void SimpleMultiplication()
        {
            var game = new NumbersGame(new int[] {3, 7}, 21);
            var solver = new DeepRecursiveUndoingSolver();
            ISolution solution;
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, solution.NumberOfOperations);
        }
        
        [Test]
        public void SimpleDivision()
        {
            var game = new NumbersGame(new[] {75, 25}, 3);
            var solver = new DeepRecursiveUndoingSolver();
            ISolution solution;
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, solution.NumberOfOperations);
        }

        [Test]
        public void MultiplicationAndAddition()
        {
            var game = new NumbersGame(new[] { 2, 8, 10,}, 82); // (8*10)+2
            var solver = new DeepRecursiveUndoingSolver();
            ISolution solution;
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, solution.NumberOfOperations);
            
        }
        [Test]
        public void DivisionAndSubtraction()
        {
            var game = new NumbersGame(new[] { 10, 50, 1}, 4); // (50/10)-1
            var solver = new DeepRecursiveUndoingSolver();
            ISolution solution;
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, solution.NumberOfOperations);

        }




    }
}
