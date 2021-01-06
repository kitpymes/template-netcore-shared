using Kitpymes.Core.Shared.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class ResultTest
    {
        #region ResultOK

        [TestMethod]
        public void ResultOK()
        {
            var actual = Result.Ok();

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Title, Result.DefaultTitleOk);
            Assert.AreEqual(actual.StatusCode, Result.DefaultStatusCodeOk.ToValue());
        }

        [TestMethod]
        public void ResultOk_WithMessage()
        {
            var messageExpected = Guid.NewGuid().ToString();

            var actual = Result.Ok(messageExpected);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Message, messageExpected);
        }

        #endregion ResultOK

        #region ResultOKData

        [TestMethod]
        public void ResultOkData()
        {
            var dataExpected = new FakeUser { Email = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString() };

            var actual = Result<FakeUser>.Ok(dataExpected);
            var actualToJson = actual.ToJson();

            Assert.IsTrue(actual.Success);

            Assert.AreEqual(actual.Data, dataExpected);
            Assert.IsTrue(actualToJson.Contains(dataExpected.ToSerialize(), StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultOkData_WithMessage()
        {
            var dataExpected = new FakeUser { Email = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString() };
            var messageExpected = Guid.NewGuid().ToString();

            var actual = Result<FakeUser>.Ok(dataExpected, messageExpected);
            var actualToJson = actual.ToJson();

            Assert.IsTrue(actual.Success);

            Assert.AreEqual(actual.Message, messageExpected);
            Assert.IsTrue(actualToJson.Contains(messageExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Data, dataExpected);
            Assert.IsTrue(actualToJson.Contains(dataExpected.ToSerialize(), StringComparison.CurrentCulture));
        }

        #endregion ResultOKData

        #region ResultError

        [TestMethod]
        public void ResultError()
        {
            var actual = Result.Error();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Title, Result.DefaultTitleError);
            Assert.AreEqual(actual.StatusCode, Result.DefaultStatusCodeError.ToValue());
        }

        [TestMethod]
        public void ResultError_WithMessage_WithDetails()
        {
            var messageExpected = Guid.NewGuid().ToString();
            object detailsExpected = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var actual = Result.Error(messageExpected, detailsExpected);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Message, messageExpected);
            Assert.AreEqual(actual.Details, detailsExpected);
        }

        [TestMethod]
        public void ResultError_WithTitle_WithStatusCode_WithMessage_WithDetails_WithExceptionType_WithModelErrors()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;

            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));
            var titleExpected = Guid.NewGuid().ToString();
            var messageExpected = Guid.NewGuid().ToString();
            var statusCodeExpected = HttpStatusCode.BadRequest;
            var exceptionTypeExpected = nameof(ArrayTypeMismatchException);
            object detailsExpected = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var errors = new List<(string fieldName, string message)>();

            if (stringField.ToIsNullOrEmpty())
            {
                errors.Add((nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            }

            if (!stringField.ToIsEmail())
            {
                errors.Add((nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            }

            if (classField.ToIsNullOrEmpty())
            {
                errors.Add((nameof(classField), classFieldMessageExpected));
            }

            Result? actual = null;

            if (errors.Any())
            {
                actual = Result.Error(options => options
                        .WithTitle(titleExpected)
                        .WithStatusCode(statusCodeExpected)
                        .WithMessages(messageExpected)
                        .WithDetails(detailsExpected)
                        .WithExceptionType(exceptionTypeExpected)
                        .WithErrors(errors));
            }

            var actualJson = actual!.ToJson();

            Assert.IsFalse(actual.Success);

            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));

            Assert.IsTrue(actualJson.Contains(Messages.NullOrEmpty(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(Messages.InvalidFormat(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(classFieldMessageExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Title, titleExpected);
            Assert.IsTrue(actualJson.Contains(titleExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.StatusCode, statusCodeExpected.ToValue());
            Assert.IsTrue(actualJson.Contains(statusCodeExpected.ToValue().ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Message, messageExpected);
            Assert.IsTrue(actualJson.Contains(messageExpected.ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Details, detailsExpected);
            Assert.IsTrue(actualJson.Contains(detailsExpected.ToSerialize(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.ExceptionType, exceptionTypeExpected);
            Assert.IsTrue(actualJson.Contains(exceptionTypeExpected, StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultError_WithTitle_WithStatusCode_WithMessage_WithDetails_WithExceptionType_WithErrors()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;

            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));
            var titleExpected = Guid.NewGuid().ToString();
            var statusCodeExpected = HttpStatusCode.BadRequest;
            var exceptionTypeExpected = nameof(ArrayTypeMismatchException);
            object detailsExpected = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var messagesExpected = new List<string>();

            if (stringField.ToIsNullOrEmpty())
            {
                messagesExpected.Add(Messages.NullOrEmpty(nameof(stringField)));
            }

            if (!stringField.ToIsEmail())
            {
                messagesExpected.Add(Messages.InvalidFormat(nameof(stringField)));
            }

            if (classField.ToIsNullOrEmpty())
            {
                messagesExpected.Add(classFieldMessageExpected);
            }

            Result? actual = null;

            if (messagesExpected.Any())
            {
                actual = Result.Error(options => options
                    .WithTitle(titleExpected)
                    .WithStatusCode(statusCodeExpected)
                    .WithDetails(detailsExpected)
                    .WithExceptionType(exceptionTypeExpected)
                    .WithMessages(messagesExpected));
            }

            var actualJson = actual!.ToJson();

            Assert.IsFalse(actual.Success);

            Assert.IsTrue(actual.Message!.Contains(Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(classFieldMessageExpected));

            Assert.IsTrue(actualJson.Contains(Messages.NullOrEmpty(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(Messages.InvalidFormat(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(classFieldMessageExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Title, titleExpected);
            Assert.IsTrue(actualJson.Contains(titleExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.StatusCode, statusCodeExpected.ToValue());
            Assert.IsTrue(actualJson.Contains(statusCodeExpected.ToValue().ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Message, messagesExpected.ToString(", "));
            Assert.IsTrue(actualJson.Contains(messagesExpected.First(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Details, detailsExpected);
            Assert.IsTrue(actualJson.Contains(detailsExpected.ToSerialize(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.ExceptionType, exceptionTypeExpected);
            Assert.IsTrue(actualJson.Contains(exceptionTypeExpected, StringComparison.CurrentCulture));
        }

        #endregion ResultError

        #region ResultErrorData

        [TestMethod]
        public void ResultErrorData_WithTitle_WithStatusCode_WithMessage_WithDetails_WithExceptionType_WithModelErrors()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;

            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));
            var titleExpected = Guid.NewGuid().ToString();
            var messageExpected = Guid.NewGuid().ToString();
            var statusCodeExpected = HttpStatusCode.BadRequest;
            var exceptionTypeExpected = nameof(ArrayTypeMismatchException);
            object detailsExpected = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var errors = new List<(string fieldName, string message)>();

            if (stringField.ToIsNullOrEmpty())
            {
                errors.Add((nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            }

            if (!stringField.ToIsEmail())
            {
                errors.Add((nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            }

            if (classField.ToIsNullOrEmpty())
            {
                errors.Add((nameof(classField), classFieldMessageExpected));
            }

            Result? actual = null;

            if (errors.Any())
            {
                actual = Result<FakeUser>.Error(options => options
                    .WithTitle(titleExpected)
                    .WithStatusCode(statusCodeExpected)
                    .WithMessages(messageExpected)
                    .WithDetails(detailsExpected)
                    .WithExceptionType(exceptionTypeExpected)
                    .WithErrors(errors));
            }

            var actualJson = actual!.ToJson();

            Assert.IsFalse(actual.Success);

            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));

            Assert.IsTrue(actualJson.Contains(Messages.NullOrEmpty(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(Messages.InvalidFormat(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(classFieldMessageExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Title, titleExpected);
            Assert.IsTrue(actualJson.Contains(titleExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.StatusCode, statusCodeExpected.ToValue());
            Assert.IsTrue(actualJson.Contains(statusCodeExpected.ToValue().ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Message, messageExpected);
            Assert.IsTrue(actualJson.Contains(messageExpected.ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Details, detailsExpected);
            Assert.IsTrue(actualJson.Contains(detailsExpected.ToSerialize(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.ExceptionType, exceptionTypeExpected);
            Assert.IsTrue(actualJson.Contains(exceptionTypeExpected, StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultErrorData_WithTitle_WithStatusCode_WithMessage_WithDetails_WithExceptionType_WithErrors()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;

            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));
            var titleExpected = Guid.NewGuid().ToString();
            var statusCodeExpected = HttpStatusCode.BadRequest;
            var exceptionTypeExpected = nameof(ArrayTypeMismatchException);
            object detailsExpected = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var messagesExpected = new List<string>();

            if (stringField.ToIsNullOrEmpty())
            {
                messagesExpected.Add(Messages.NullOrEmpty(nameof(stringField)));
            }

            if (!stringField.ToIsEmail())
            {
                messagesExpected.Add(Messages.InvalidFormat(nameof(stringField)));
            }

            if (classField.ToIsNullOrEmpty())
            {
                messagesExpected.Add(classFieldMessageExpected);
            }

            Result? actual = null;

            if (messagesExpected.Any())
            {
                actual = Result<FakeUser>.Error(options => options                 
                    .WithTitle(titleExpected)
                    .WithStatusCode(statusCodeExpected)
                    .WithDetails(detailsExpected)
                    .WithExceptionType(exceptionTypeExpected)
                    .WithMessages(messagesExpected));
            }

            var actualJson = actual!.ToJson();

            Assert.IsFalse(actual.Success);

            Assert.IsTrue(actual.Message!.Contains(Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(classFieldMessageExpected));

            Assert.IsTrue(actualJson.Contains(Messages.NullOrEmpty(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(Messages.InvalidFormat(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(classFieldMessageExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Title, titleExpected);
            Assert.IsTrue(actualJson.Contains(titleExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.StatusCode, statusCodeExpected.ToValue());
            Assert.IsTrue(actualJson.Contains(statusCodeExpected.ToValue().ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Message, messagesExpected.ToString(", "));
            Assert.IsTrue(actualJson.Contains(messagesExpected.First(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Details, detailsExpected);
            Assert.IsTrue(actualJson.Contains(detailsExpected.ToSerialize(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.ExceptionType, exceptionTypeExpected);
            Assert.IsTrue(actualJson.Contains(exceptionTypeExpected, StringComparison.CurrentCulture));
        }

        #endregion ResultErrorData
    }
}