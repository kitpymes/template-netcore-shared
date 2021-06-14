using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        private IDictionary<string, IEnumerable<string>>? List { get; set; }

        [TestMethod]
        public void AddOrUpdate_Passing_Valid_Collection_Returns_Collection()
        {
            var aKeyValueExpected = Guid.NewGuid().ToString();
            var aKeyRepeatValueExpected = Guid.NewGuid().ToString();

            List = new Dictionary<string, IEnumerable<string>>();
            List
                .AddOrUpdate("a", aKeyValueExpected)
                .AddOrUpdate("e", Guid.NewGuid().ToString())
                .AddOrUpdate("i", Guid.NewGuid().ToString())
                .AddOrUpdate("o", Guid.NewGuid().ToString())
                .AddOrUpdate("u", Guid.NewGuid().ToString())
                .AddOrUpdate("a", aKeyRepeatValueExpected);

            Assert.IsTrue(List.Contains("a", aKeyValueExpected));
            Assert.IsTrue(List.Contains("a", aKeyRepeatValueExpected));
        }

        [TestMethod]
        public void GetValues_Passing_Valid_Key_Returns_Values()
        {
            var aKeyValueExpected = Guid.NewGuid().ToString();
            var aKeyRepeatValueExpected = Guid.NewGuid().ToString();

            List = new Dictionary<string, IEnumerable<string>>();
            List
                .AddOrUpdate("a", aKeyValueExpected)
                .AddOrUpdate("e", Guid.NewGuid().ToString())
                .AddOrUpdate("i", Guid.NewGuid().ToString())
                .AddOrUpdate("o", Guid.NewGuid().ToString())
                .AddOrUpdate("u", Guid.NewGuid().ToString())
                .AddOrUpdate("a", aKeyRepeatValueExpected);

            Assert.IsTrue(List.GetValues("a")?.Count() > 0);
        }
    }
}
