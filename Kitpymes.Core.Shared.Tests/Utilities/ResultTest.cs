using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class ResultTest
    {
        #region ResultOk

        [TestMethod]
        public void ResultOk()
        {
            var actual = Util.Result.Ok();

            Assert.AreEqual(true, actual.Success);
        }

        [TestMethod]
        public void ResultOkWithMessage()
        {
            string message = Guid.NewGuid().ToString();

            var actual = Util.Result.Ok(message);

            Assert.AreEqual(true, actual.Success);
            Assert.AreEqual(actual.Message, message);
        }

        [TestMethod]
        public void ResultDataOkWihValue()
        {
            object data = Guid.NewGuid().ToString();

            var actual = Util.Result.Ok(data);

            Assert.AreEqual(true, actual.Success);
            Assert.AreEqual(data, actual.Data);
        }

        #endregion ResultOk

        #region ResultError

        [TestMethod]
        public void ResultError()
        { 
            var actual = Util.Result.Error();

            Assert.AreEqual(false, actual.Success);
        }

        [TestMethod]
        public void ResultErrorWithMessage()
        {
            var message = Guid.NewGuid().ToString();

            var actual = Util.Result.Error(message);
            var actualJson = actual.ToJson();

            Assert.AreEqual(false, actual.Success);
            Assert.AreEqual(actual.Message, message);
            Assert.IsTrue(actualJson.Contains(message, StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultErrorWithException()
        {
            var message = Guid.NewGuid().ToString();
            var exception = new Exception(message);

            var actual = Util.Result.Error(exception);
            var actualJson = actual.ToJson();

            Assert.AreEqual(false, actual.Success);
            Assert.AreEqual(actual.Message, exception.ToFullMessage());
            Assert.IsTrue(actualJson.Contains(message, StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultErrorWithMessageWithDetails()
        {
            string message = Guid.NewGuid().ToString();

            object details = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var actual = Util.Result.Error(message, details);

            var actualJson = actual.ToJson();

            Assert.AreEqual(false, actual.Success);
            Assert.AreEqual(actual.Message, message);
            Assert.AreEqual(actual.Details, details);
            Assert.IsTrue(actualJson.Contains(message, StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(details.ToSerialize(), StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultErrorWithValidationError()
        {
            var messages = new Dictionary<string, string> {
                { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var actual = Util.Result.Error(messages);
            var actualJson = actual.ToJson();

            Assert.AreEqual(false, actual.Success);
            Assert.IsTrue(actual.Count > 0);
            Assert.AreEqual(messages.First(), actual.Messages.First());
            Assert.IsTrue(actualJson.Contains(messages.ToSerialize(), StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultErrorWithValidationErrorWithDetails()
        {
            var messages = new Dictionary<string, string> {
                { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            object details = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var actual = Util.Result.Error(messages, details);

            Assert.AreEqual(false, actual.Success);
            Assert.IsTrue(actual.Count > 0);
            Assert.AreEqual(messages.First(), actual.Messages.First());
            Assert.IsTrue(actual.Details == details);
        }

        #endregion ResultError
    }
}
