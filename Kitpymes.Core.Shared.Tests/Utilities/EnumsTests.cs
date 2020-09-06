using Kitpymes.Core.Shared.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class EnumsTests
    {
        enum FakeEnum { A, B, C }

        #region ToCount

        [TestMethod]
        public void ToCount_Passing_ValidEnumType_Returns_Count()
        {
            var valueExpected = 3;

            var valueActual = Util.Enums.ToCount<FakeEnum>();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToCount

        #region ToValues

        [TestMethod]
        public void ToValues_Passing_ValidEnumType_Returns_ValuesListString()
        {
            var result = Util.Enums.ToValues<FakeEnum>();

            Assert.IsTrue(result.Contains(FakeEnum.A.ToString()));
            Assert.IsTrue(result.Contains(FakeEnum.B.ToString()));
            Assert.IsTrue(result.Contains(FakeEnum.C.ToString()));
        }

        #endregion ToValues

        #region ToList

        [TestMethod]
        public void ToList_Passing_ValidEnumType_Returns_ValuesList()
        {
            var result = Util.Enums.ToList<FakeEnum>();

            var (value, text, selected) = result.First(x => x.value == (int)FakeEnum.A);

            Assert.AreEqual(text, FakeEnum.A.ToString());
            Assert.IsFalse(selected);
        }

        [TestMethod]
        public void ToList_Passing_ValidEnumTypeWithSelectedValue_Returns_ValuesList()
        {
            var result = Util.Enums.ToList<FakeEnum>(null, FakeEnum.B);

            var (value, text, selected) = result.First(x => x.value == (int)FakeEnum.B);

            Assert.AreEqual(text, FakeEnum.B.ToString());
            Assert.IsTrue(selected);
        }

        [TestMethod]
        public void ToList_Passing_ValidEnumTypeWithResourceType_Returns_ValuesList()
        {
            var result = Util.Enums.ToList<FakeEnum>(typeof(FakeResource));

            var (value, text, selected) = result.First(x => x.value == (int)FakeEnum.C);

            Assert.AreEqual(text, FakeResource.C);
            Assert.IsFalse(selected);
        }

        [TestMethod]
        public void ToList_Passing_ValidEnumTypeWithSelectedValueWithResourceType_Returns_ValuesList()
        {
            var result = Util.Enums.ToList<FakeEnum>(typeof(FakeResource), FakeEnum.B);

            var (value, text, selected) = result.First(x => x.value == (int)FakeEnum.B);

            Assert.AreEqual(text, FakeResource.B);
            Assert.IsTrue(selected);
        }

        #endregion ToList
    }
}
