using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Extensions.Tests
{
    [TestClass]
    public class ExceptionExtensionsTests
    {
        private Exception? FakeException { get; set; }

        [TestMethod]
        public void ToMessage_Passing_ExceptionWithMessage_Returns_Message()
        {
            var stringExpected = Guid.NewGuid().ToString();
            FakeException = new Exception(stringExpected);

            var stringActual = FakeException.ToMessage();

            Assert.AreEqual(stringExpected, stringActual);
        }

        [TestMethod]
        public void ToMessage_Passing_ExceptionWithMessageWithInnerException_Returns_MessageAndInnerException()
        {
            var messageExpected = Guid.NewGuid().ToString();
            var messageInnerExceptionExpected = Guid.NewGuid().ToString();
            FakeException = new Exception(messageExpected, new ApplicationException(messageInnerExceptionExpected));

            var stringActual = FakeException.ToMessage();

            Assert.IsTrue(stringActual.Contains(messageExpected));
            Assert.IsTrue(stringActual.Contains(messageInnerExceptionExpected));
            if (FakeException.InnerException != null)
            {
                Assert.IsTrue(stringActual.Contains(FakeException.InnerException.Message));
            }
        }

        [TestMethod]
        public void ToFullMessage_Passing_ExceptionWithMessageWithInnerException_Returns_Full_Message()
        {
            var messageExpected = Guid.NewGuid().ToString();
            var messageInnerExceptionExpected = Guid.NewGuid().ToString();
            FakeException = new Exception(messageExpected, new ApplicationException(messageInnerExceptionExpected));

            var stringActual = FakeException.ToFullMessage();

            Assert.IsTrue(stringActual.Contains(messageExpected));
            Assert.IsTrue(stringActual.Contains(messageInnerExceptionExpected));
            if (FakeException.InnerException != null)
            {
                Assert.IsTrue(stringActual.Contains(FakeException.InnerException.Message));
            }
        }
    }
}
