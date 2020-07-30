using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        #region ToDefaultValue

        [TestMethod]
        public void ToDefaultValue_Passing_ValidStruct_Returns_DefaultValue()
        {
            var value = new DateTime();
            DateTime valueExpected = default;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_ValidInt_Returns_DefaultValue()
        {
            var value = 456;
            int valueExpected = default;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_ValidString_Returns_DefaultValue()
        {
            var value = Guid.NewGuid().ToString();
            string? valueExpected = default;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_ValidGuid_Returns_DefaultValue()
        {
            var value = Guid.NewGuid();
            Guid valueExpected = Guid.Empty;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_ValidBoolean_Returns_DefaultValue()
        {
            var value = true;
            bool valueExpected = false;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToDefaultValue
    }
}
