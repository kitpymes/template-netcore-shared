using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class ClaimsPrincipalExtensionsTests
    {
        private readonly string claimTypeId = ClaimTypes.NameIdentifier;
        private readonly string claimTypeName = ClaimTypes.Name;
        private readonly string claimTypeUserData = ClaimTypes.UserData;
        private readonly int claimValueId = new Random().Next();
        private readonly string claimValueName = Guid.NewGuid().ToString();
        private readonly string authenticationType = Guid.NewGuid().ToString();

        #region ToIsAuthenticated

        [TestMethod]
        public void ToIsAuthenticated_Passing_Null_ClaimsPrincipal_Returns_ApplicationException()
        {
            ClaimsPrincipal? claimsPrincipal = null;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;

            Assert.ThrowsException<ApplicationException>(() => claimsPrincipalActual.ToIsAuthenticated());
        }

        [TestMethod]
        public void ToIsAuthenticated_Passing_Claims_Returns_False()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;
            var threadCurrentPrincipalActual = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Assert.IsFalse(claimsPrincipalActual.ToIsAuthenticated());
            Assert.IsFalse(threadCurrentPrincipalActual.ToIsAuthenticated());
        }

        [TestMethod]
        public void ToIsAuthenticated_Passing_Claims_Returns_True()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeName, claimValueName), (claimTypeId, claimValueId.ToString()));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;
            var threadCurrentPrincipalActual = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Assert.IsTrue(claimsPrincipalActual.ToIsAuthenticated());
            Assert.IsTrue(threadCurrentPrincipalActual.ToIsAuthenticated());
        }

        #endregion ToIsAuthenticated

        #region ToAuthenticationType

        [TestMethod]
        public void ToAuthenticationType_Passing_Null_ClaimsPrincipal_Returns_ApplicationException()
        {
            ClaimsPrincipal? claimsPrincipal = null;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;

            Assert.ThrowsException<ApplicationException>(() => claimsPrincipalActual.ToAuthenticationType());
        }

        [TestMethod]
        public void ToAuthenticationType_Passing_Claims_Returns_Null()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;
            var threadCurrentPrincipalActual = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Assert.IsNull(claimsPrincipalActual.ToAuthenticationType());
            Assert.IsNull(threadCurrentPrincipalActual.ToAuthenticationType());
        }

        [TestMethod]
        public void ToAuthenticationType_Passing_Claims_Returns_AuthenticationType_Name()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeName, claimValueName), (claimTypeId, claimValueId.ToString()));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;
            var threadCurrentPrincipalActual = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Assert.AreEqual(authenticationType, claimsPrincipalActual.ToAuthenticationType());
            Assert.AreEqual(authenticationType, threadCurrentPrincipalActual.ToAuthenticationType());
        }

        #endregion ToAuthenticationType

        #region ToUserName

        [TestMethod]
        public void ToUserName_Passing_Null_ClaimsPrincipal_Returns_ApplicationException()
        {
            ClaimsPrincipal? claimsPrincipal = null;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;

            Assert.ThrowsException<ApplicationException>(() => claimsPrincipalActual.ToUserName());
        }

        [TestMethod]
        public void ToUserName_Passing_Invalid_ClaimsPrincipal_Returns_Null()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;
            var threadCurrentPrincipalActual = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Assert.IsNull(claimsPrincipalActual.ToUserName());
            Assert.IsNull(threadCurrentPrincipalActual.ToUserName());
        }

        [TestMethod]
        public void ToUserName_Passing_Valid_UserName_ClaimsPrincipal_Returns_String_UserName_Value()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeName, claimValueName));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;
            var threadCurrentPrincipalActual = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Assert.AreEqual(claimValueName, claimsPrincipalActual.ToUserName());
            Assert.AreEqual(claimValueName, threadCurrentPrincipalActual.ToUserName());
        }

        #endregion ToUserName

        #region ToExists

        [TestMethod]
        public void ToExists_Passing_Claims_Returns_True()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeName, claimValueName), (claimTypeId, claimValueId.ToString()));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User;
            var threadCurrentPrincipalActual = (ClaimsPrincipal)Thread.CurrentPrincipal;

            Assert.IsTrue(claimsPrincipalActual.ToExists(claimTypeName));
            Assert.IsTrue(threadCurrentPrincipalActual.ToExists(claimTypeName));

            Assert.IsTrue(claimsPrincipalActual.ToExists(claimTypeId));
            Assert.IsTrue(threadCurrentPrincipalActual.ToExists(claimTypeId));
        }

        #endregion ToExists

        #region ToGet

        [TestMethod]
        public void ToGet_Passing_Claims_Returns_Claim_Value_String()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeName, claimValueName));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User.ToGet<string>(claimTypeName);
            var threadCurrentPrincipalActual = ((ClaimsPrincipal)Thread.CurrentPrincipal).ToGet<string>(claimTypeName);

            Assert.AreEqual(claimValueName, claimsPrincipalActual);
            Assert.AreEqual(claimValueName, threadCurrentPrincipalActual);
        }

        [TestMethod]
        public void ToGet_Passing_Claims_Returns_Claim_Value_Int()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeId, claimValueId));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User.ToGetValue<int>(claimTypeId);
            var threadCurrentPrincipalActual = ((ClaimsPrincipal)Thread.CurrentPrincipal).ToGetValue<int>(claimTypeId);

            Assert.AreEqual(claimValueId, claimsPrincipalActual);
            Assert.AreEqual(claimValueId, threadCurrentPrincipalActual);
        }

        [TestMethod]
        public void ToGet_Passing_Claims_Returns_Claim_Value_Object()
        {
            var fakeObject = new FakeUser
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Age = new Random().Next(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeUserData, fakeObject));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User.ToGet<FakeUser>(claimTypeUserData);
            var threadCurrentPrincipalActual = ((ClaimsPrincipal)Thread.CurrentPrincipal).ToGet<FakeUser>(claimTypeUserData);

            Assert.AreEqual(fakeObject.Id, claimsPrincipalActual?.Id);
            Assert.AreEqual(fakeObject.Name, claimsPrincipalActual?.Name);
            Assert.AreEqual(fakeObject.Age, claimsPrincipalActual?.Age);
            CollectionAssert.AreEqual(fakeObject.Permissions, claimsPrincipalActual?.Permissions);

            Assert.AreEqual(fakeObject.Id, threadCurrentPrincipalActual?.Id);
            Assert.AreEqual(fakeObject.Name, threadCurrentPrincipalActual?.Name);
            Assert.AreEqual(fakeObject.Age, threadCurrentPrincipalActual?.Age);
            CollectionAssert.AreEqual(fakeObject.Permissions, threadCurrentPrincipalActual?.Permissions);
        }

        #endregion ToGet

        #region ToGetAll

        [TestMethod]
        public void ToGetAll_Passing_Claims_Returns_Claim_Value_String_List()
        {
            var claimTypeRole = ClaimTypes.Role;
            var permission_add = Guid.NewGuid().ToString();
            var permission_edit = Guid.NewGuid().ToString();

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeRole, permission_add), (claimTypeRole, permission_edit));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User.ToGetAll<string>(claimTypeRole);
            var threadCurrentPrincipalActual = ((ClaimsPrincipal)Thread.CurrentPrincipal).ToGetAll<string>(claimTypeRole);

            Assert.IsNotNull(claimsPrincipalActual);
            Assert.IsTrue(claimsPrincipalActual.Count() == 2);
            Assert.IsTrue(claimsPrincipalActual.Contains(permission_add));
            Assert.IsTrue(claimsPrincipalActual.Contains(permission_edit));

            Assert.IsNotNull(threadCurrentPrincipalActual);
            Assert.IsTrue(threadCurrentPrincipalActual.Count() == 2);
            Assert.IsTrue(threadCurrentPrincipalActual.Contains(permission_add));
            Assert.IsTrue(threadCurrentPrincipalActual.Contains(permission_edit));
        }

        [TestMethod]
        public void ToGetAll_Passing_Claims_Returns_Claim_Value_Int_List()
        {
            var claimTypeRole = ClaimTypes.Role;
            var permissionId_add = new Random().Next();
            var permissionId_edit = new Random().Next();

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeRole, permissionId_add), (claimTypeRole, permissionId_edit));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User.ToGetAll<int>(claimTypeRole);
            var threadCurrentPrincipalActual = ((ClaimsPrincipal)Thread.CurrentPrincipal).ToGetAll<int>(claimTypeRole);

            Assert.IsNotNull(claimsPrincipalActual);
            Assert.IsTrue(claimsPrincipalActual.Count() == 2);
            Assert.IsTrue(claimsPrincipalActual.Contains(permissionId_add));
            Assert.IsTrue(claimsPrincipalActual.Contains(permissionId_edit));

            Assert.IsNotNull(threadCurrentPrincipalActual);
            Assert.IsTrue(threadCurrentPrincipalActual.Count() == 2);
            Assert.IsTrue(threadCurrentPrincipalActual.Contains(permissionId_add));
            Assert.IsTrue(threadCurrentPrincipalActual.Contains(permissionId_edit));
        }

        [TestMethod]
        public void ToGetAll_Passing_Claims_Returns_Claim_Value_Object_List()
        {
            var fakeObject = new FakeUser
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Age = new Random().Next(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var fakeObject1 = new FakeUser
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Age = new Random().Next(),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.ToAdd(authenticationType, (claimTypeUserData, fakeObject), (claimTypeUserData, fakeObject1));
            Thread.CurrentPrincipal = claimsPrincipal;

            var fakeHttpContext = FakeHttpContext.Configure(x =>
            {
                x.User = claimsPrincipal;
            });

            var claimsPrincipalActual = fakeHttpContext.User.ToGetAll<FakeUser>(claimTypeUserData);
            var threadCurrentPrincipalActual = ((ClaimsPrincipal)Thread.CurrentPrincipal).ToGetAll<FakeUser>(claimTypeUserData);

            var resultItemClaimsPrincipalActual = claimsPrincipalActual.First(x => x.Id == fakeObject.Id);
            var resultItemThreadCurrentPrincipalActual = threadCurrentPrincipalActual.First(x => x.Id == fakeObject1.Id);

            Assert.IsNotNull(claimsPrincipalActual);
            Assert.IsTrue(claimsPrincipalActual.Count() == 2);
            Assert.AreEqual(fakeObject.Id, resultItemClaimsPrincipalActual.Id);
            Assert.AreEqual(fakeObject.Name, resultItemClaimsPrincipalActual.Name);
            Assert.AreEqual(fakeObject.Age, resultItemClaimsPrincipalActual.Age);
            CollectionAssert.AreEqual(fakeObject.Permissions, resultItemClaimsPrincipalActual.Permissions);

            Assert.IsNotNull(threadCurrentPrincipalActual);
            Assert.IsTrue(threadCurrentPrincipalActual.Count() == 2);
            Assert.AreEqual(fakeObject1.Id, resultItemThreadCurrentPrincipalActual.Id);
            Assert.AreEqual(fakeObject1.Name, resultItemThreadCurrentPrincipalActual.Name);
            Assert.AreEqual(fakeObject1.Age, resultItemThreadCurrentPrincipalActual.Age);
            CollectionAssert.AreEqual(fakeObject1.Permissions, resultItemThreadCurrentPrincipalActual.Permissions);
        }

        #endregion ToGetAll
    }
}
