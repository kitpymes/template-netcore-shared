using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
	public class GuidExtensionsTests
    {
        // Guid.CompareTo Method
        // Return A negative integer: This instance is less than value. 
        // Return Zero: This instance is equal to value.
        // Return A positive integer: This instance is greater than value.

        [TestMethod]
		public void ToSecuencial_Passing_Guids_Returns_GuidsSecuencials()
		{
            var guid = Guid.NewGuid();

            var guid1 = guid.ToSecuencial();
            var guid2 = guid.ToSecuencial();

            Assert.IsTrue(guid1.CompareTo(guid2) < 0);
         }
    }
}

