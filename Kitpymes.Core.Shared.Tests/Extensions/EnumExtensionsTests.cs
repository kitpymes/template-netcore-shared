using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class EnumExtensionsTests
    {
        enum FakeEnumeration 
        {
            [System.ComponentModel.DataAnnotations.Display(Name = "Pepe", Description = "Mi nombre es Pepe")]
            a,
            [System.ComponentModel.Category()]
            b, 
            [System.ComponentModel.Description("Pepe")]
            c 
        }

        [TestMethod]
        public void ToInt_Passing_Enumeration_Name_Returns_Value()
        {
            var stringExpected = 1;

            var stringActual = FakeEnumeration.b.ToValue();

            Assert.AreEqual(stringExpected, stringActual);
        }

        [TestMethod]
        public void ToInt_Passing_Enumeration_Name_Returns_Description_Atributte()
        {
            var stringExpected = "Pepe";

            var stringActual = FakeEnumeration.c.ToDescription();

            Assert.AreEqual(stringExpected, stringActual);
        }

        [TestMethod]
        public void ToDisplay_Passing_Enumeration_Name_Returns_Display_Atributte()
        {
            (string? name, string? description) stringsExpected = ("Pepe", "Mi nombre es Pepe");

            var stringsActual = FakeEnumeration.a.ToDisplay();

            Assert.AreEqual(stringsExpected, stringsActual);
        }

        [TestMethod]
        public void ToAttribute_Passing_Enumeration_Name_Returns_Atributte()
        {
            var attributeExpected = new System.ComponentModel.CategoryAttribute();

            var attributeActual = FakeEnumeration.b.ToAttribute<System.ComponentModel.CategoryAttribute>();

            Assert.AreEqual(attributeExpected, attributeActual);
        }
    }
}
