using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
	public class ActionExtensionsTests
    {
        private readonly Guid expectedId = Guid.NewGuid();
        private readonly string expectedName = Guid.NewGuid().ToString();
        private readonly string expectedEmail = Guid.NewGuid().ToString();
        private readonly int expectedAge = new Random().Next(1, 80);
        private readonly string expectedAddPermission = Guid.NewGuid().ToString();
        private readonly string expectedUpdatePermission = Guid.NewGuid().ToString();

        private Action<FakeUser>? CustomAction { get; set; }

        [TestMethod]
		public void ToConfigureOrDefault_Passing_ActionCustomValues_Returns_CustomValues()
		{
            CustomAction = x => {
                x.Id = expectedId;
                x.Name = expectedName;
                x.Age = expectedAge;
                x.Permissions = new string[] { expectedAddPermission, expectedUpdatePermission };
            };

            var result = CustomAction.ToConfigureOrDefault();

			Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedAge, result.Age);

            if (result.Permissions != null)
            {
                CollectionAssert.Contains(result.Permissions, expectedAddPermission);
                CollectionAssert.Contains(result.Permissions, expectedUpdatePermission);
            }            
         }

        [TestMethod]
        public void ToConfigureOrDefault_Passing_DefaultValues_And_ActionCustomValues_Returns_DefaultValues_And_CustomValues()
        {
            CustomAction = x => {               
                x.Age = expectedAge;
                x.Permissions = new string[] { expectedAddPermission, expectedUpdatePermission };
            };

            var defaultAction = new FakeUser
            {
                Id = expectedId,
                Name = expectedName,
                Email = expectedEmail,
                Age = new Random().Next(81, 90),
                Permissions = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            };

            var result = CustomAction.ToConfigureOrDefault(defaultAction);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedEmail, result.Email);
            Assert.AreEqual(expectedAge, result.Age);

            if (result.Permissions != null)
            {
                CollectionAssert.Contains(result.Permissions, expectedAddPermission);
                CollectionAssert.Contains(result.Permissions, expectedUpdatePermission);
            }            
        }
    }
}
