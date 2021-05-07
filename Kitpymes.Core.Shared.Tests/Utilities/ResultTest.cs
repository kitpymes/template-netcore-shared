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
            Assert.AreEqual(actual.Status, HttpStatusCode.OK.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.OK.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgProcessRanSuccessfully);
        }

        [TestMethod]
        public void ResultOk_WithMessage_WithDetails()
        {
            var messageExpected = Guid.NewGuid().ToString();
            var detailsExpected = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var actual = Result.Ok();
            actual.Message = messageExpected;
            actual.Details = detailsExpected;

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.OK.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.OK.ToString());
            Assert.AreEqual(actual.Message, messageExpected);
            Assert.AreEqual(actual.Details, detailsExpected);
        }

        #endregion ResultOK

        #region ResultOKData

        [TestMethod]
        public void ResultOkData()
        {
            var actual = Result<FakeUser>.Ok();

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.OK.ToValue());
            Assert.AreEqual(actual.Title, Resources.MsgProcessRanSuccessfully);
            Assert.AreEqual(actual.Message, Resources.MsgProcessRanSuccessfully);
        }

        [TestMethod]
        public void ResultOkData_WithData()
        {
            var dataExpected = new FakeUser { Age = 13, Permissions = new[] { "read", "create" }, Email = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString() };

            var actual = Result<FakeUser>.Ok(dataExpected);
            var actualToJson = actual.ToJson();

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.OK.ToValue());
            Assert.AreEqual(actual.Title, Resources.MsgProcessRanSuccessfully);
            Assert.AreEqual(actual.Message, Resources.MsgProcessRanSuccessfully);
            Assert.AreEqual(actual.Data, dataExpected);

            Assert.IsTrue(actualToJson.Contains(dataExpected.ToSerialize(), StringComparison.CurrentCulture));
        }

        [TestMethod]
        public void ResultOkData_WithData_WithMessage_WithDetails()
        {
            var dataExpected = new FakeUser { Age = 13, Permissions = new[] { "read", "create" }, Email = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString() };
            var messageExpected = Guid.NewGuid().ToString();
            var detailsExpected = new
            {
                Code = new Random().Next(),
                Link = Guid.NewGuid().ToString(),
                Otro = Guid.NewGuid().ToString(),
            };

            var actual = Result<FakeUser>.Ok(dataExpected);
            actual.Message = messageExpected;
            actual.Details = detailsExpected;

            var actualToJson = actual.ToJson();

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.OK.ToValue());
            Assert.AreEqual(actual.Title, Resources.MsgProcessRanSuccessfully);
            Assert.AreEqual(actual.Message, messageExpected);
            Assert.AreEqual(actual.Details, detailsExpected);
            Assert.AreEqual(actual.Data, dataExpected);

            Assert.IsTrue(actualToJson.Contains(messageExpected, StringComparison.CurrentCulture));
            Assert.IsTrue(actualToJson.Contains(detailsExpected.ToSerialize(), StringComparison.CurrentCulture));
            Assert.IsTrue(actualToJson.Contains(dataExpected.ToSerialize(), StringComparison.CurrentCulture));
        }

        #endregion ResultOKData

        #region ResultHttpStatus

        [TestMethod]
        public void ResultUnauthorized()
        {
            var actual = Result.Unauthorized();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.Unauthorized.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgUnauthorizedAccess);
        }

        [TestMethod]
        public void ResultInternalServerError()
        {
            var actual = Result.InternalServerError();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.InternalServerError.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.InternalServerError.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgFriendlyUnexpectedError);
        }

        [TestMethod]
        public void ResultBadRequest_WithErrors_KeyValue()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;
            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));

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

            var actual = Result.BadRequest(errors);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.BadRequest.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgValidationsError);
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));
        }

        [TestMethod]
        public void ResultBadRequest_WithErrors_Dictionary()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;
            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));

            var errors = new Dictionary<string, IList<string>>();

            if (stringField.ToIsNullOrEmpty())
            {
                errors.Add(nameof(stringField), new List<string> { Messages.NullOrEmpty(nameof(stringField)) });
            }

            if (!stringField.ToIsEmail())
            {
                errors[nameof(stringField)].Add(Messages.InvalidFormat(nameof(stringField)));
            }

            if (classField.ToIsNullOrEmpty())
            {
                errors.Add(nameof(classField), new List<string> { classFieldMessageExpected });
            }

            var actual = Result.BadRequest(errors);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.BadRequest.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgValidationsError);
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));
        }

        [TestMethod]
        public void ResultBadRequest_WithErrors_Value()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;
            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));

            var messages = new List<string>();

            if (stringField.ToIsNullOrEmpty())
            {
                messages.Add(Messages.NullOrEmpty(nameof(stringField)));
            }

            if (!stringField.ToIsEmail())
            {
                messages.Add(Messages.InvalidFormat(nameof(stringField)));
            }

            if (classField.ToIsNullOrEmpty())
            {
                messages.Add(classFieldMessageExpected);
            }

            var actual = Result.BadRequest(messages);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.BadRequest.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.BadRequest.ToString());
            Assert.IsTrue(actual.Message!.Contains(Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(classFieldMessageExpected));
        }

        #endregion ResultHttpStatus

        #region ResultHttpStatusData

        [TestMethod]
        public void ResultDataUnauthorized()
        {
            var actual = Result<FakeUser>.Unauthorized();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.Unauthorized.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.Unauthorized.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgUnauthorizedAccess);
        }

        [TestMethod]
        public void ResultDataInternalServerError()
        {
            var actual = Result<FakeUser>.InternalServerError();

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.InternalServerError.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.InternalServerError.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgFriendlyUnexpectedError);
        }

        [TestMethod]
        public void ResultDataBadRequest_WithErrors_KeyValue()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;
            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));

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

            var actual = Result<FakeUser>.BadRequest(errors);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.BadRequest.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgValidationsError);
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));
        }

        [TestMethod]
        public void ResultDataBadRequest_WithErrors_Dictionary()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;
            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));

            var errors = new Dictionary<string, IList<string>>();

            if (stringField.ToIsNullOrEmpty())
            {
                errors.Add(nameof(stringField), new List<string> { Messages.NullOrEmpty(nameof(stringField)) });
            }

            if (!stringField.ToIsEmail())
            {
                errors[nameof(stringField)].Add(Messages.InvalidFormat(nameof(stringField)));
            }

            if (classField.ToIsNullOrEmpty())
            {
                errors.Add(nameof(classField), new List<string> { classFieldMessageExpected });
            }

            var actual = Result<FakeUser>.BadRequest(errors);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.BadRequest.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(actual.Message, Resources.MsgValidationsError);
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));
        }

        [TestMethod]
        public void ResultDataBadRequest_WithErrors_Value()
        {
            var stringField = FakeTypes.ReferenceTypes.ClassTypes.String_Null;
            var classField = FakeTypes.ReferenceTypes.ClassTypes.Class_Null;
            var classFieldMessageExpected = Messages.NullOrEmpty(nameof(classField));

            var messages = new List<string>();

            if (stringField.ToIsNullOrEmpty())
            {
                messages.Add(Messages.NullOrEmpty(nameof(stringField)));
            }

            if (!stringField.ToIsEmail())
            {
                messages.Add(Messages.InvalidFormat(nameof(stringField)));
            }

            if (classField.ToIsNullOrEmpty())
            {
                messages.Add(classFieldMessageExpected);
            }

            var actual = Result<FakeUser>.BadRequest(messages);

            Assert.IsFalse(actual.Success);
            Assert.AreEqual(actual.Status, HttpStatusCode.BadRequest.ToValue());
            Assert.AreEqual(actual.Title, HttpStatusCode.BadRequest.ToString());
            Assert.IsTrue(actual.Message!.Contains(Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(classFieldMessageExpected));
        }

        #endregion ResultHttpStatusData

        #region ResultError

        [TestMethod]
        public void ResultError_WithTitle_WithStatusCode_WithDetails_WithExceptionType_WithMessages_WithErrors()
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

            var messages = new List<string>(errors.Select(x => x.message));

            Result? actual = null;

            if (errors.Any())
            {
                actual = Result.Error(options => options
                        .WithTitle(titleExpected)
                        .WithStatusCode(statusCodeExpected)
                        .WithDetails(detailsExpected)
                        .WithExceptionType(exceptionTypeExpected)
                        .WithMessages(messages)
                        .WithErrors(errors));
            }

            var actualJson = actual!.ToJson();

            Assert.IsFalse(actual.Success);

            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));

            Assert.IsTrue(actual.Message!.Contains(Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(classFieldMessageExpected));

            Assert.IsTrue(actualJson.Contains(Messages.NullOrEmpty(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(Messages.InvalidFormat(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(classFieldMessageExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Title, titleExpected);
            Assert.IsTrue(actualJson.Contains(titleExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Status, statusCodeExpected.ToValue());
            Assert.IsTrue(actualJson.Contains(statusCodeExpected.ToValue().ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Details, detailsExpected);
            Assert.IsTrue(actualJson.Contains(detailsExpected.ToSerialize(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Exception, exceptionTypeExpected);
            Assert.IsTrue(actualJson.Contains(exceptionTypeExpected, StringComparison.CurrentCulture));
        }

        #endregion ResultError

        #region ResultErrorData

        [TestMethod]
        public void ResultErrorData_WithTitle_WithStatusCode_WithDetails_WithExceptionType_WithMessages_WithErrors()
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

            var messages = new List<string>(errors.Select(x => x.message));

            Result? actual = null;

            if (errors.Any())
            {
                actual = Result<FakeUser>.Error(options => options
                    .WithTitle(titleExpected)
                    .WithStatusCode(statusCodeExpected)
                    .WithDetails(detailsExpected)
                    .WithExceptionType(exceptionTypeExpected)
                    .WithMessages(messages)
                    .WithErrors(errors));
            }

            var actualJson = actual!.ToJson();

            Assert.IsFalse(actual.Success);

            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(stringField), Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Errors.Contains(nameof(classField), classFieldMessageExpected));

            Assert.IsTrue(actual.Message!.Contains(Messages.NullOrEmpty(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(Messages.InvalidFormat(nameof(stringField))));
            Assert.IsTrue(actual.Message.Contains(classFieldMessageExpected));

            Assert.IsTrue(actualJson.Contains(Messages.NullOrEmpty(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(Messages.InvalidFormat(nameof(stringField)), StringComparison.CurrentCulture));
            Assert.IsTrue(actualJson.Contains(classFieldMessageExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Title, titleExpected);
            Assert.IsTrue(actualJson.Contains(titleExpected, StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Status, statusCodeExpected.ToValue());
            Assert.IsTrue(actualJson.Contains(statusCodeExpected.ToValue().ToString(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Details, detailsExpected);
            Assert.IsTrue(actualJson.Contains(detailsExpected.ToSerialize(), StringComparison.CurrentCulture));

            Assert.AreEqual(actual.Exception, exceptionTypeExpected);
            Assert.IsTrue(actualJson.Contains(exceptionTypeExpected, StringComparison.CurrentCulture));
        }

        #endregion ResultErrorData
    }
}