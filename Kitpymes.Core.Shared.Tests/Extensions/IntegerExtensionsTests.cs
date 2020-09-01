using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Kitpymes.Core.Shared.Extensions.Tests
{
    [TestClass]
    public class IntegerExtensionsTests
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(-1)]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        public void ToStringFormat_Passing_ValidValue_Returns_ValueWithoutRemovedChars(int input)
        {
            var valueActual = input.ToStringFormat();

            Assert.AreEqual(input.ToString(CultureInfo.CurrentCulture), valueActual);
        }
    }
}
