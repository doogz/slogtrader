using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;
using NumbersGameSdk;
using ProgressiveRecursiveSolver;

namespace ProgressiveRecursiveSolverTests
{
    [TestFixture]
    public class SolverTests
    {
        // Some solvable reference games
        private static NumbersGame GetSolvableGame(int n)
        {
            switch (n)
            {
                case 0:
                    return new NumbersGame(new int[] {10, 7, 2}, 24); // must be (7*2) + 10
                case 1:
                    return new NumbersGame(new int[] {1,1,1,1,1,1}, 9); // must be (1+(1+1)) * (1+(1+1))
                case 2:
                    return new NumbersGame(new int[] {3,5,7,1,9,1}, 947); // must be 3*5*7*9 +1 +1
            }
            return null;
        }


        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void SolveRefGame1()
        {
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetFirstSolution(GetSolvableGame(0), out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, solution.NumberOfOperations);

        }

        [Test]
        public void SolveRefGame2()
        {
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetFirstSolution(GetSolvableGame(1), out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(5, solution.NumberOfOperations);

        }
        [Test]
        public void SolveRefGame3()
        {
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetFirstSolution(GetSolvableGame(2), out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(5, solution.NumberOfOperations);

        }


    }
}
