using Kitpymes.Core.Shared.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Extensions.Tests
{
    [TestClass]
	public class ActionExtensionsTests
    {
        private readonly Guid expectedCustomActionId = Guid.NewGuid();
        private readonly string expectedCustomActionName = Guid.NewGuid().ToString();
        private readonly int expectedCustomActionAge = new Random().Next(1, 80);
        private readonly string expectedCustomActionAddPermission = Guid.NewGuid().ToString();
        private readonly string expectedCustomActionUpdatePermission = Guid.NewGuid().ToString();

        private Action<FakeUser>? CustomAction { get; set; }

        [TestMethod]
		public void ToConfigureOrDefault_Passing_ActionCustomValues_Returns_CustomValues()
		{
            CustomAction = x => {
                x.Id = expectedCustomActionId;
                x.Name = expectedCustomActionName;
                x.Age = expectedCustomActionAge;
                x.Permissions = new string[] { expectedCustomActionAddPermission, expectedCustomActionUpdatePermission };
            };

            var result = CustomAction.ToConfigureOrDefault();

			Assert.IsNotNull(result);
            Assert.AreEqual(expectedCustomActionId, result.Id);
            Assert.AreEqual(expectedCustomActionName, result.Name);
            Assert.AreEqual(expectedCustomActionAge, result.Age);
            CollectionAssert.Contains(result.Permissions, expectedCustomActionAddPermission);
            CollectionAssert.Contains(result.Permissions, expectedCustomActionUpdatePermission);
         }

        [TestMethod]
        public void ToConfigureOrDefault_Passing_DefaultValues_And_ActionCustomValues_Returns_DefaultValues_And_CustomValues()
        {
            var expectedDefaultActionName = Guid.NewGuid().ToString();

            CustomAction = x => {
                x.Id = expectedCustomActionId;
                x.Age = expectedCustomActionAge;
                x.Permissions = new string[] { expectedCustomActionAddPermission, expectedCustomActionUpdatePermission };
            };

            var defaultAction = new FakeUser
            {
                Id = Guid.NewGuid(),
                Name = expectedDefaultActionName,
                Age = new Random().Next(81, 90)
            };

            var result = CustomAction.ToConfigureOrDefault(defaultAction);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCustomActionId, result.Id);
            Assert.AreEqual(expectedDefaultActionName, result.Name);
            Assert.AreEqual(expectedCustomActionAge, result.Age);
            CollectionAssert.Contains(result.Permissions, expectedCustomActionAddPermission);
            CollectionAssert.Contains(result.Permissions, expectedCustomActionUpdatePermission);
        }
    }
}
