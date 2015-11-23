namespace SoftFX.Extended.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using SoftFX.Extended;

    [TestClass]
    public class ProtocolVersionTests
    {
        #region Static members

        static readonly FixProtocolVersion FirstExt11 = new FixProtocolVersion("ext", 1, 1);
        static readonly FixProtocolVersion FirstExt12 = new FixProtocolVersion("ext", 1, 2);
        static readonly FixProtocolVersion FirstStd11 = new FixProtocolVersion("std", 1, 2);
        static readonly FixProtocolVersion FirstStd12 = new FixProtocolVersion("std", 1, 2);

        static readonly FixProtocolVersion SecondExt11 = new FixProtocolVersion("ext", 1, 1);
        static readonly FixProtocolVersion SecondExt12 = new FixProtocolVersion("ext", 1, 2);

        // ReSharper disable RedundantDefaultFieldInitializer
        static readonly FixProtocolVersion FirstNull = null;
        static readonly FixProtocolVersion SecondNull = null;
        // ReSharper restore RedundantDefaultFieldInitializer
        #endregion
        
        [TestMethod]
        public void StaticToString()
        {
            var st = FixProtocolVersion.ToString("xxx", 5, 12);
            Assert.IsTrue("xxx.5.12" == st);
        }

        [TestMethod]
        public void InstanceToString()
        {
            var value = new FixProtocolVersion("xxx", 4, 17);
            var st = value.ToString();
            Assert.IsTrue("xxx.4.17" == st);
        }

        [TestMethod]
        public void CreateForInvalidType()
        {
            try
            {
                new FixProtocolVersion(null, 1, 1);
                Assert.Fail("Invalid type has been accepted");
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void CreateForInvalidMajorVersion()
        {
            var random = new Random();
            try
            {
                new FixProtocolVersion(String.Empty, -random.Next(), 1);
                Assert.Fail("Invalid major version has been accepted");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void CreateForInvalidMinorVersion()
        {
            var random = new Random();
            try
            {
                new FixProtocolVersion(String.Empty, 1, -random.Next());
                Assert.Fail("Invalid minor version has been accepted");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void CorrectLess()
        {
            var status = FirstExt11 < FirstExt12;
            Assert.IsTrue(status);
            status = FirstExt11 < SecondExt12;
            Assert.IsTrue(status);
            // ReSharper disable EqualExpressionComparison
#pragma warning disable 1718
            status = FirstExt11 < FirstExt11;
#pragma warning restore 1718
            // ReSharper restore EqualExpressionComparison
            Assert.IsFalse(status);
            status = FirstExt11 < SecondExt11;
            Assert.IsFalse(status);
            status = FirstExt12 < FirstExt11;
            Assert.IsFalse(status);
            status = FirstExt12 < SecondExt11;
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void NullLess()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 < SecondNull);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstNull < SecondExt11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void CorrectMore()
        {
            var status = FirstExt12 > FirstExt11;
            Assert.IsTrue(status);
            status = FirstExt12 > SecondExt11;
            Assert.IsTrue(status);
            // ReSharper disable EqualExpressionComparison
#pragma warning disable 1718
            status = FirstExt11 > FirstExt11;
#pragma warning restore 1718
            // ReSharper restore EqualExpressionComparison
            Assert.IsFalse(status);
            status = FirstExt11 > SecondExt11;
            Assert.IsFalse(status);
            status = FirstExt11 > FirstExt12;
            Assert.IsFalse(status);
            status = FirstExt11 > SecondExt12;
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void NullMore()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 > SecondNull);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstNull > SecondExt11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void CorrectLessOrEqual()
        {
            // ReSharper disable EqualExpressionComparison
#pragma warning disable 1718
            var status = FirstExt11 <= FirstExt11;
#pragma warning restore 1718
            // ReSharper restore EqualExpressionComparison
            Assert.IsTrue(status);
            status = FirstExt11 <= SecondExt11;
            Assert.IsTrue(status);
            status = FirstExt11 <= FirstExt12;
            Assert.IsTrue(status);
            status = FirstExt11 <= SecondExt12;
            Assert.IsTrue(status);
            status = FirstExt12 <= FirstExt11;
            Assert.IsFalse(status);
            status = FirstExt12 <= SecondExt11;
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void NullLessOrEqual()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 <= SecondNull);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstNull <= SecondExt11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void CorrectMoreOrEqual()
        {
            // ReSharper disable EqualExpressionComparison
#pragma warning disable 1718
            var status = FirstExt11 >= FirstExt11;
#pragma warning restore 1718
            // ReSharper restore EqualExpressionComparison
            Assert.IsTrue(status);
            status = FirstExt11 >= SecondExt11;
            Assert.IsTrue(status);
            status = FirstExt12 >= FirstExt11;
            Assert.IsTrue(status);
            status = FirstExt12 >= SecondExt11;
            Assert.IsTrue(status);
            status = FirstExt11 >= FirstExt12;
            Assert.IsFalse(status);
            status = FirstExt11 >= SecondExt12;
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void NullMoreOrEqual()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 >= SecondNull);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }

            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstNull >= SecondExt11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid null comparison");
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void CorrectEqual()
        {
            // ReSharper disable EqualExpressionComparison
#pragma warning disable 1718
            var status = (FirstExt11 == FirstExt11);
#pragma warning restore 1718
            // ReSharper restore EqualExpressionComparison
            Assert.IsTrue(status);
            status = (FirstExt11 == SecondExt11);
            Assert.IsTrue(status);
            status = (FirstExt11 == FirstExt12);
            Assert.IsFalse(status);
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            status = (FirstNull == SecondNull);
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            Assert.IsTrue(status);
            status = (FirstExt11 == SecondNull);
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void CorrectNotEqual()
        {
            // ReSharper disable EqualExpressionComparison
#pragma warning disable 1718
            var status = (FirstExt11 != FirstExt11);
#pragma warning restore 1718
            // ReSharper restore EqualExpressionComparison
            Assert.IsFalse(status);
            status = (FirstExt11 != SecondExt11);
            Assert.IsFalse(status);
            status = (FirstExt11 != FirstExt12);
            Assert.IsTrue(status);
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            status = (FirstNull != SecondNull);
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            Assert.IsFalse(status);
            status = (FirstExt11 != SecondNull);
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void DifferentTypesLess()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 < FirstStd11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }

            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 < FirstStd12);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void DifferentTypesMore()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 > FirstStd11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }

            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 > FirstStd12);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void DifferentTypesLessOrEqual()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 <= FirstStd11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }

            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 <= FirstStd12);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void DifferentTypesMoreOrEqual()
        {
            try
            {
                // ReSharper disable UnusedVariable
                var status = (FirstExt11 < FirstStd11);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }

            try
            {
                // ReSharper disable UnusedVariable
                Boolean status = (FirstExt11 < FirstStd12);
                // ReSharper restore UnusedVariable
                Assert.Fail("Invalid comparison");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void ParseCorrectString()
        {
            var protocol = new FixProtocolVersion("ext.1.2");
            Assert.IsTrue("ext" == protocol.Type);
            Assert.IsTrue(1 == protocol.MajorVersion);
            Assert.IsTrue(2 == protocol.MinorVersion);
        }

        [TestMethod]
        public void ParseIncorrectString()
        {
            try
            {
                new FixProtocolVersion("ext.1.2.3");
                Assert.Fail("Incorrect string is accepted");
            }
            catch (ArgumentException)
            {
            }
            try
            {
                new FixProtocolVersion(null);
                Assert.Fail("Null string is accepted");
            }
            catch (ArgumentNullException)
            {
            }
        }
    }
}
