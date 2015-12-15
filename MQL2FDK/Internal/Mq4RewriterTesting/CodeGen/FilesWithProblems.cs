using NUnit.Framework;

namespace Mq4RewriterTesting.CodeGen
{
    [TestFixture]
    public class FilesWithProblems
    {
        /// <summary>
        /// Failing with double sized arrays
        /// cases like this:
        ///   double a[] [10]; 
        /// are handled by the class FixDeclaredArray but are not handled correctly at least for this script
        /// </summary>
        [Test]
        public void TestTrader_test_with_report_15_min()
        {
            TestVariousFiles.TestFileSemantic("Trader_test_with_report_15_min.mq4");
        }

        [Test]
        public void TestTraderCrash()
        {
            TestVariousFiles.TestFileSemantic("Trader_crash.mq4");
        }
    }
}