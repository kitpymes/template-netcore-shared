using Kitpymes.Core.Shared.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kitpymes.Core.Shared.Extensions.Tests
{
    [TestClass]
    public class QueryableExtensionsTests
    {
        private readonly List<FakeUser> fakeListExpected = new List<FakeUser> 
        { 
            new FakeUser
            {
                Age = 55,
                Email = Guid.NewGuid().ToString(),
                Name = "a",
            },
            new FakeUser
            {
                Age = 18,
                Email = Guid.NewGuid().ToString(),
                Name = "h",
            },
            new FakeUser
            {
                Age = 40,
                Email = Guid.NewGuid().ToString(),
                Name = "b",
            },
        };

        #region ToWhere

        [TestMethod]
        public void ToWhere_Passing_Null_Predicate_Returns_Same_List()
        {
            Expression<Func<FakeUser, bool>>? predicate = null;

            var fakeListActual = fakeListExpected.AsQueryable().ToWhere(predicate).ToList();

            CollectionAssert.AreEquivalent(fakeListExpected, fakeListActual);
        }

        [TestMethod]
        public void ToWhere_Passing_Valid_Predicate_Returns_Filter_List()
        {
            Expression<Func<FakeUser, bool>>? predicate = x => x.Age > 20;

            var fakeListActual = fakeListExpected.AsQueryable().ToWhere(predicate).ToList();

            Assert.IsTrue(fakeListActual.Count == 2);
        }

        #endregion ToWhere

        #region ToPaged

        [TestMethod]
        public void ToPaged_Passing_Age_Ascending_Order_Returns_Filter_List()
        {
            var property = "Age";
            var ascending = true;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 18);
            Assert.IsTrue(fakeListActual.First().Name == "h");
        }

        [TestMethod]
        public void ToPaged_Passing_Age_Descending_Order_Returns_Filter_List()
        {
            var property = "Age";
            var ascending = false;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 55);
            Assert.IsTrue(fakeListActual.First().Name == "a");
        }

        [TestMethod]
        public void ToPaged_Passing_Name_Ascending_Order_Returns_Filter_List()
        {
            var property = "Name";
            var ascending = true;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 55);
            Assert.IsTrue(fakeListActual.First().Name == "a");
        }

        [TestMethod]
        public void ToPaged_Passing_Name_Descending_Order_Returns_Filter_List()
        {
            var property = "Name";
            var ascending = false;

            var fakeListActual = fakeListExpected.AsQueryable().ToPaged(property, ascending).ToList();

            Assert.IsTrue(fakeListActual.First().Age == 18);
            Assert.IsTrue(fakeListActual.First().Name == "h");
        }

        #endregion ToPaged
    }
}
