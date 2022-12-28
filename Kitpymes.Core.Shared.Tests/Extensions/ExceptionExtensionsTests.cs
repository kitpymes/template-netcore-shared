using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class ExceptionExtensionsTests
    {
        private Exception? FakeException { get; set; }

        [TestMethod]
        public void ToFullMessage_Passing_ExceptionWithMessage_Returns_Message()
        {
            var stringExpected = Guid.NewGuid().ToString();
            FakeException = new Exception(stringExpected);

            var stringActual = FakeException.ToFullMessage();

            Assert.AreEqual(stringExpected, stringActual);
        }

        [TestMethod]
        public void ToFullMessage_Passing_ExceptionWithMessageWithInnerException_Returns_MessageAndInnerException()
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

        [TestMethod]
        public void ToFullMessageDetail_Passing_ExceptionWithMessageWithInnerException_Returns_Full_Message_Detail()
        {
            var messageExpected = Guid.NewGuid().ToString();
            var messageInnerExceptionExpected = Guid.NewGuid().ToString();
            var messageInnerInnerExceptionExpected = Guid.NewGuid().ToString();

            FakeException = new Exception(messageExpected, 
                new ApplicationException(messageInnerExceptionExpected, 
                new ArgumentOutOfRangeException(messageInnerInnerExceptionExpected)));

            FakeException.Data.Add("Id", Guid.NewGuid());
            FakeException.Data.Add("Name", Guid.NewGuid().ToString());
            FakeException.Data.Add("Email", Guid.NewGuid().ToString());

            var stringActual = FakeException.ToFullMessageDetail();

            Assert.IsTrue(stringActual.Contains(messageExpected));
            Assert.IsTrue(stringActual.Contains(messageInnerExceptionExpected));
            Assert.IsTrue(stringActual.Contains(messageInnerInnerExceptionExpected));

            if (FakeException.InnerException != null)
            {
                Assert.IsTrue(stringActual.Contains(FakeException.InnerException.Message));

                if (FakeException.InnerException.InnerException != null)
                {
                    Assert.IsTrue(stringActual.Contains(FakeException.InnerException.InnerException.Message));
                }
            }
        }
    }
}
