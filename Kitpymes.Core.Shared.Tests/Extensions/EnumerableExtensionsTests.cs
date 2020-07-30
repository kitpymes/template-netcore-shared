using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        private IEnumerable<string>? Collection { get; set; }

        [TestMethod]
        public void ToString_Passing_Valid_Collection_Returns_String_With_Default_Separator()
        {
            var stringExpected = "a, e, i, o, u";

            Collection = new List<string>() { "a", "e", "i", "o", "u", };

            var stringActual = Collection.ToString(", ");

            Assert.AreEqual(stringExpected, stringActual);
        }

        [TestMethod]
        public void ToString_Passing_Valid_Collection_With_Custom_Separator_Returns_String_With_Custom_Separator()
        {
            var stringExpected = "a-e-i-o-u";

            Collection = new List<string>() { "a", "e", "i", "o", "u", };

            var stringActual = Collection.ToString("-");

            Assert.AreEqual(stringExpected, stringActual);
        }

        [TestMethod]
        public void ToEmptyIfNull_Passing_Null_Collection_Returns_Empty_Collection()
        {
            var collectionExpected = Enumerable.Empty<string>();

            var collectionActual = Collection.ToEmptyIfNull();

            Assert.AreEqual(collectionExpected, collectionActual);
        }

        [TestMethod]
        public void ToReadOnly_Passing_Valid_Collection_Returns_Readonly_Collection()
        {
            var collectionExpected = new List<string>() { "a", "e", "i", "o", "u", };

            Collection = new List<string>() { "a", "e", "i", "o", "u", };

            var collectionActual = Collection.ToReadOnly();

            CollectionAssert.AreEqual(collectionExpected, collectionActual);
        }
    }
}
