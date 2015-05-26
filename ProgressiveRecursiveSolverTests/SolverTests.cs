using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;
using NumbersGameSdk;
namespace ProgressiveRecursiveSolverTests
{
    [TestFixture]
    public class SolverTests
    {
        private static NumbersGame refGame1 = new NumbersGame(new int[] {10, 7, 2}, 24);
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void SolvesRefGame1()
        {
            //var solver = ProgressiveRecursiveSolver.ProgressiveRecursiveBruteForceSolver();
        }

    }
}
