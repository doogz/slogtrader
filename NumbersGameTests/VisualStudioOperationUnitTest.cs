using NumbersGameSdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumbersGameTests
{
    //[TestClass]

    public class VisualStudioOperationUnitTest
    {
        //[TestMethod]
        public void Addition()
        {
            var op = new Operation(3, 7, Operator.Addition);
            Assert.AreEqual(10, op.Result);

        }
    }
}
