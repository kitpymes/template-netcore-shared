using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class VerifyExtensionsTests
    {
        #region NullOrEmpty

        [TestMethod]
        public void IsNullOrEmpty_Passing_InvalidValue_Returns_True()
        {
            string? parameter = null;
            var valueExpected = true;

            var valueActual = parameter.IsNullOrEmpty();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NullOrEmpty(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ThrowIfNullOrEmpty(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NullOrEmpty

        #region NullOrAny

        [TestMethod]
        public void IsNullOrAny_Passing_InvalidValue_Returns_True()
        {
            string[] parameter = Array.Empty<string>();
            var valueExpected = true;

            var valueActual = parameter.IsNullOrAny();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ThrowIfNullOrAny_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string[] parameter = Array.Empty<string>();
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NullOrAny(paramName);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ThrowIfNullOrAny(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion NullOrAny

        #region Directory

        [TestMethod]
        public void ThrowIfDirectoryNotExists_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = Guid.NewGuid().ToString();
            var valueExpected = Util.Messages.NotFound(parameter);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ThrowIfDirectoryNotExists(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Directory

        #region File

        [TestMethod]
        public void ThrowIfFileNotExists_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.NotFound(parameter);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ThrowIfFileNotExists(paramName));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion File

        #region FileExtension

        [TestMethod]
        public void ThrowIfNotFileExtension_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = Guid.NewGuid().ToString();
            var paramName = nameof(parameter);
            var valueExpected = Util.Messages.InvalidFormat(parameter);

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ThrowIfNotFileExtension(paramName));

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
        public void ThrowIfNotEmail_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage(string? value)
        {
            Assert.ThrowsException<ApplicationException>(() => value.ThrowIfNotEmail(nameof(value)));
        }

        #endregion Email

        #region Subdomain

        [DataTestMethod]
        [DataRow("dasdasd@dsasd.._ff")]
        [DataRow("34fsds@@sdd.f")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("34?fsds@sdd.yy")]
        public void ThrowIfNotSubdomain_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage(string? value)
        {
            string? parameter = Guid.NewGuid().ToString();
            var valueExpected = Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(value) ? nameof(parameter) : value);

            var result = Assert.ThrowsException<ApplicationException>(() => value.ThrowIfNotSubdomain(nameof(parameter)));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Subdomain

        #region Name

        [TestMethod]
        public void ToIsNameThrow_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? value = Guid.NewGuid().ToString();
            var valueExpected = Util.Messages.InvalidFormat(value);

            var result = Assert.ThrowsException<ApplicationException>(() => value.ThrowIfNotName(nameof(value)));

            Assert.AreEqual(valueExpected, result.Message);
        }

        #endregion Name

        #region Throw

        [TestMethod]
        public void ThrowIf_Passing_InvalidValue_Returns_ApplicationExceptionWithMessage()
        {
            string? parameter = null;
            var valueExpected = Guid.NewGuid().ToString();

            var result = Assert.ThrowsException<ApplicationException>(() => parameter.ThrowIf(() => parameter is null, valueExpected));

            Assert.AreEqual(valueExpected, result.Message);
        }

        [TestMethod]
        public void ThrowIf_Passing_ValidValue_Returns_Value()
        {
            string value = Guid.NewGuid().ToString();
            var valueExpected = value;

            var valueActual = value.ThrowIf(() => value is null, valueExpected);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion Throw

        #region ContainsUniqueChars

        [TestMethod]
        public void IsContainsUniqueChars_Passing_ValidValue_Returns_True()
        {
            var value = "1aeiou2";
            var valueExpected = true;

            var result = value.IsContainsUniqueChars();

            Assert.AreEqual(valueExpected, result);
        }

        [TestMethod]
        public void IsContainsUniqueChars_Passing_InvalidValue_Returns_False()
        {
            var value = "1aeiou1";
            var valueExpected = false;

            var result = value.IsContainsUniqueChars();

            Assert.AreEqual(valueExpected, result);
        }

        #endregion ContainsUniqueChars
    }
}
