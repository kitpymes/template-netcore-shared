using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
	public class AgileMapperExtensionsTests
    {
        private readonly string addPermission = Guid.NewGuid().ToString();
        private readonly string updatePermission = Guid.NewGuid().ToString();

        private FakeUser Source { get; set; } = new FakeUser();
        private FakeUserDto Destination { get; set; } = new FakeUserDto();

        [TestInitialize]
        public void Initialize()
        {
            Source = new FakeUser
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Age = new Random().Next(),
                Permissions = new string[] { addPermission, updatePermission },
            };
        }

        #region ToMapClone

        [TestMethod]
		public void ToMapClone_Passing_Source_Returns_New_Source_Object()
		{
            var result = Source.ToMapClone();

            Assert.AreEqual(Source.Id, result.Id);
            Assert.AreEqual(Source.Name, result.Name);
            Assert.AreEqual(Source.Age, result.Age);

            if (result.Permissions != null)
            {
                CollectionAssert.Contains(result.Permissions, addPermission);
                CollectionAssert.Contains(result.Permissions, updatePermission);

            }
           
            CollectionAssert.AreEqual(Source.Permissions, result.Permissions);
        }

        #endregion ToMapClone

        #region ToMapNew

        [TestMethod]
		public void ToMapNew_Passing_TSource_And_TDestination_Returns_New_TDestination_Object()
		{
			var result = Source.ToMapNew<FakeUser, FakeUserDto>();

            Assert.AreEqual(Source.Name, result.Name);
            Assert.AreEqual(Source.Age, result.Age);

            if (result.Permissions != null)
            {
                CollectionAssert.Contains(result.Permissions, addPermission);
                CollectionAssert.Contains(result.Permissions, updatePermission);
                CollectionAssert.AreEqual(Source.Permissions, result.Permissions);
            }            
        }

        [TestMethod]
        public void ToMapNew_Passing_TDestination_Returns_New_Destination_Object()
        {
            var result = Source.ToMapNew<FakeUserDto>();

            Assert.AreEqual(Source.Name, result.Name);
            Assert.AreEqual(Source.Age, result.Age);

            if (result.Permissions != null)
            {
                CollectionAssert.Contains(result.Permissions, addPermission);
                CollectionAssert.Contains(result.Permissions, updatePermission);
                CollectionAssert.AreEqual(Source.Permissions, result.Permissions);
            }
        }

        #endregion ToMapNew

        #region ToMapUpdate

        [TestMethod]
        public void ToMapUpdate_Passing_Destination_Object_Returns_Updated_Destination_Object()
        {
            Destination = new FakeUserDto
            {
                Name = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Age = 0,
                Permissions = new string[] { addPermission },
            };

            Source.Email = null;

            var result = Source.ToMapUpdate(Destination);

            Assert.AreEqual(Source.Name, result.Name);
            Assert.AreEqual(Destination.Email, result.Email);
            Assert.AreEqual(Source.Age, result.Age);
            CollectionAssert.Contains(Destination.Permissions, addPermission);

            if (result.Permissions != null)
            {
                CollectionAssert.Contains(result.Permissions, updatePermission);
                CollectionAssert.AreEqual(Source.Permissions, result.Permissions);
            }
        }

        #endregion ToMapUpdate

        #region ToMapMerge

        [TestMethod]
        public void ToMapMerge_Passing_Destination_Object_Returns_Merged_Destination_Object()
        {
            Destination = new FakeUserDto
            {
                Name = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Age = 0,
                Permissions = new string[] { addPermission },
            };

            Source.Email = null;

            var result = Source.ToMapMerge(Destination);

            Assert.AreEqual(Destination.Name, result.Name);
            Assert.AreEqual(Destination.Email, result.Email);
            Assert.AreEqual(Source.Age, result.Age);
            CollectionAssert.Contains(Destination.Permissions, addPermission);

            if (result.Permissions != null)
            {
                CollectionAssert.Contains(result.Permissions, updatePermission);
                CollectionAssert.AreEqual(Source.Permissions, result.Permissions);
            }
        }

        #endregion ToMapMerge

        #region ToMapList

        [TestMethod]
        public void ToMapList_Passing_Source_Type_And_Destination_Type_Returns_Destination_Object_List()
        {
            var item = new FakeUser
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Age = new Random().Next(),
                Permissions = new string[] { addPermission, updatePermission }
            };

            var item1 = new FakeUser
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Age = new Random().Next(),
                Permissions = new string[] { addPermission }
            };

            var source = new List<FakeUser> { item, item1 }.AsQueryable();

            var result = source.ToMapList<FakeUser, FakeUserDto>();
            var resultItem = result.First(x => x.Name == item.Name);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
            Assert.AreEqual(item.Name, resultItem.Name);
            Assert.AreEqual(item.Age, resultItem.Age);
            Assert.AreEqual(item.Permissions, resultItem.Permissions);
        }

        #endregion ToMapList
    }
}
