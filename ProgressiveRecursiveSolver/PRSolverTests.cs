using ScottLogic.NumbersGame;

namespace ProgressiveRecursiveSolver
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
            var game = new NumbersGame(new int[] {1, 2}, 3); // must be (1+2)
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(1, solution.NumberOfOperations);

        }

        [Test]
        public void SolveMultiplicationAndAddition()
        {
            var game = new NumbersGame(new int[] {10, 7, 2}, 24); // must be (7*2) + 10
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, solution.NumberOfOperations);

        }

        [Test]
        public void SolveDivisionAndSubtraction()
        {
            var game = new NumbersGame(new int[] {10, 50, 1}, 4); // must be (50/10)  -1
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(true, solved);
            Assert.AreEqual(2, solution.NumberOfOperations);

        }

        [Test]
        public void NotSolvable()
        {
            var game = new NumbersGame(new int[] {10, 5}, 3);
            ISolution solution;
            var solver = new ProgressiveRecursiveBruteForceSolver();
            bool solved = solver.GetFirstSolution(game, out solution);
            Assert.AreEqual(false, solved);
        }
    }
}




