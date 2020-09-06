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
        public void ToIsNullOrEmptyThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NullOrEmpty(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToIsNullOrEmptyThrow(paramName));

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
        public void ToIsNullOrAnyThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string[] parameter = new string[] { };
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NullOrAny(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToIsNullOrAnyThrow(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NullOrAny

        #region Directory

        [TestMethod]
        public void ToIsDirectoryThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = Guid.NewGuid().ToString();
            var valueExpected = Util.Messages.NotFound(parameter);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToIsDirectoryThrow(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Directory

        #region File

        [TestMethod]
        public void ToIsFileThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NotFound(parameter);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToIsFileThrow(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion File

        #region FileExtension

        [TestMethod]
        public void ToIsFileExtensionThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.InvalidFormat(parameter);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToIsFileExtensionThrow(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion FileExtension

        #region Email

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.._ff")]
        [DataRow("34fsds@@sdd.f")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("34?fsds@sdd.yy")]
        public void ToIsEmailThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage(string? value)
        {
            Assert.ThrowsException<ApplicationException>(() => value.ToIsEmailThrow(nameof(value)));
        }

        #endregion Email

        #region Subdomain

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.._ff")]
        [DataRow("34fsds@@sdd.f")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("34?fsds@sdd.yy")]
        public void ToIsSubdomainThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage(string? value)
        {
            string? parameter = Guid.NewGuid().ToString();
            var valueExpected = Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(value) ? nameof(parameter) : value);

            var result = Assert.ThrowsException<ApplicationException>(() => value.ToIsSubdomainThrow(nameof(parameter)));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Subdomain

        #region Name

        [TestMethod]
        public void ToIsNameThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.InvalidFormat(parameter);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToIsNameThrow(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Name

        #region Throw

        [TestMethod]
        public void ToThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var valueExpected = Guid.NewGuid().ToString();

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ToIsThrow(() => parameter is null, valueExpected));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Throw

        #region IsErrors

        [TestMethod]
        public void ToHasErrors_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var valueExpected = true;

            var valueActual = parameter.ToIsErrors(() => parameter is null);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion IsErrors
    }
}
