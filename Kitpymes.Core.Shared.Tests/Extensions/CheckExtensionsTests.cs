using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class CheckExtensionsTests
    {
        #region NullOrEmpty

        [TestMethod]
        public void ToIsNullOrEmpty_Passing_InvalidValue_Returns_True()
        {
            string? parameter = null;
            var valueExpected = true;

            var valueActual = parameter.ToIsNullOrEmpty();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToThrowIfNullOrEmpty_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NullOrEmpty(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToThrowIfNullOrEmpty(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NullOrEmpty

        #region NullOrAny

        [TestMethod]
        public void ToIsNullOrAny_Passing_InvalidValue_Returns_True()
        {
            string[] parameter = new string[] { };
            var valueExpected = true;

            var valueActual = parameter.ToIsNullOrAny();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToThrowIfNullOrAny_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string[] parameter = new string[] { };
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NullOrAny(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToThrowIfNullOrAny(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NullOrAny

        #region NotFound

        [TestMethod]
        public void ToThrowIfNotFound_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NotFound(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToThrowIfNotFound(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NotFound

        #region NotFoundDirectory(

        [TestMethod]
        public void ToThrowIfNotFoundDirectory_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NotFound(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToThrowIfNotFoundDirectory(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NotFound

        #region Throw

        [TestMethod]
        public void ToThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var valueExpected = Guid.NewGuid().ToString();

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToThrow(() => parameter is null, valueExpected));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Throw

        #region HasErrors

        [TestMethod]
        public void ToHasErrors_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var valueExpected = true;

            var valueActual = parameter.ToHasErrors(() => parameter is null);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion HasErrors
    }
}
