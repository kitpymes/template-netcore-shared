using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class ResultTest
    {
        #region Ok

        [TestMethod]
        public void Ok_Result()
        {
            var actual = Util.Result.Ok();

            Assert.IsTrue(actual.Success);
        }

        [TestMethod]
        public void Ok_ResultMessage_WithMessage()
        {
            string message = Guid.NewGuid().ToString();

            var actual = Util.Result.Ok(message);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Message, message);
        }

        [TestMethod]
        public void Ok_ResultData_WithData()
        {
            object data = Guid.NewGuid().ToString();

            var actual = Util.Result.Ok(data);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(data, actual.Data);
        }

        #endregion Ok

        #region Error

        [TestMethod]
        public void Error_Result()
        { 
            var actual = Util.Result.Error();

            Assert.IsFalse(actual.Success);
        }

        [TestMethod]
        public void Error_ResultMessage_WithMessage_WithDetails()
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

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Message, message);
            Assert.AreEqual(actual.Details, details);
            Assert.IsTrue(actualJson.Contains(message, StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(details.ToSerialize(), StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void Error_ResultMessage_WithException_WithDetails()
        {
            var message = Guid.NewGuid().ToString();
            var exception = new Exception(message);
            object details = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var actual = Util.Result.Error(exception, details);
            var actualJson = actual.ToJson();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Details, details);
            Assert.IsTrue(actualJson.Contains(message, StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(details.ToSerialize(), StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void Error_ResultError_WithErrors()
        {
            var errors = new Dictionary<string, string> {
                { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var actual = Util.Result.Error(errors);
            var actualJson = actual.ToJson();

            Assert.IsFalse(actual.Success);
            Assert.IsTrue(actual.Count > 0);
            CollectionAssert.AreEqual(errors, actual.Errors.ToList());
            Assert.IsTrue(actualJson.Contains(errors.ToSerialize(), StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void Error_ResultData_WithErrors()
        {
            var errors = new List<string> {
                { Guid.NewGuid().ToString() }, { Guid.NewGuid().ToString() }
            };

            var actual = Util.Result.Error<object>(errors);

            Assert.IsFalse(actual.Success);
            CollectionAssert.AreEqual(errors, actual.Errors.ToList());
        }

        #endregion Error
    }
}
