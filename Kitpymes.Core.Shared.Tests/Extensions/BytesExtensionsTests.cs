using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
	public class BytesExtensionsTests
    {
        private readonly Guid expectedId = Guid.NewGuid();
        private readonly string expectedName = Guid.NewGuid().ToString();
        private readonly int expectedAge = new Random().Next(1, 80);
        private readonly string expectedAddPermission = Guid.NewGuid().ToString();
        private readonly string expectedUpdatePermission = Guid.NewGuid().ToString();

        private FakeUser FakeObject = new FakeUser();

        [TestInitialize]
        public void Initialize()
        {
            FakeObject = new FakeUser
            {
                Id = expectedId,
                Name = expectedName,
                Age = expectedAge,
                Permissions = new string[] { expectedAddPermission, expectedUpdatePermission }
            };
        }

        [TestMethod]
		public void ToCompress_Passing_Value_Bytes_Returns_Value_Bytes()
		{
            var fakeObjectToBytes = FakeObject.ToBytes();

            var bytesToCompress = fakeObjectToBytes?.ToCompress();

            var compressToDecompress = bytesToCompress?.ToDecompress();

            Assert.AreEqual(fakeObjectToBytes?.Length, compressToDecompress?.Length);
        }

        [TestMethod]
        public void ToCompress_Passing_Object_Bytes_Returns_Object()
        {
            var fakeObjectToBytes = FakeObject.ToBytes();

            var bytesToCompress = fakeObjectToBytes?.ToCompress();

            var decompressToObject = bytesToCompress?.ToDecompress<FakeUser>();

            Assert.AreEqual(expectedId, decompressToObject?.Id);
            Assert.AreEqual(expectedName, decompressToObject?.Name);
            Assert.AreEqual(expectedAge, decompressToObject?.Age);
            CollectionAssert.Contains(decompressToObject?.Permissions, expectedAddPermission);
            CollectionAssert.Contains(decompressToObject?.Permissions, expectedUpdatePermission);
        }
    }
}
