﻿using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class GenericExtensionsTests
    {
        #region ToDefaultValue

        #region IntegerTypeNumbers

        [TestMethod]
        public void ToDefaultValue_Passing_sbyte_Returns_Cero()
        {
            sbyte value = sbyte.MaxValue;

            sbyte valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_byte_Returns_Cero()
        {
            byte value = byte.MaxValue;

            byte valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_short_Returns_Cero()
        {
            short value = short.MaxValue;

            short valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_ushort_Returns_Cero()
        {
            ushort value = ushort.MaxValue;

            ushort valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow(int.MaxValue)]
        public void ToDefaultValue_Passing_int_Returns_Cero(int value)
        {
            int valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_uint_Returns_Cero()
        {
            uint value = uint.MaxValue;

            uint valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow(long.MaxValue)]
        public void ToDefaultValue_Passing_long_Returns_Cero(long value)
        {
            long valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_ulong_Returns_Cero()
        {
            ulong value = ulong.MaxValue;

            ulong valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion IntegerTypeNumbers

        #region FloatingPointTypeNumbers

        [TestMethod]
        public void ToDefaultValue_Passing_float_Returns_Cero()
        {
            float value = float.MaxValue;

            float valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow(double.MaxValue)]
        public void ToDefaultValue_Passing_double_Returns_Cero(double value)
        {
            double valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_decimal_Returns_Cero()
        {
            var value = decimal.MaxValue;
            decimal valueExpected = 0;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion FloatingPointTypeNumbers

        [DataTestMethod]
        [DataRow(true)]
        public void ToDefaultValue_Passing_bool_Returns_Cero(bool value)
        {
            bool valueExpected = false;

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToDefaultValue_Passing_char_Returns_Cero()
        {
            char value = '*';

            char valueExpected = '\0';

            var valueActual = value.ToDefaultValue();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToDefaultValue

        #region ToDictionaryPropertyInfo

        [TestMethod]
        public void ToDictionaryPropertyInfo_Passing_Object_Not_IncludeNullOrEmptyProperty_Returns_Properties_NameAndValue_Dictionary()
        {
            var fakeUserExpected = new FakeUser
            {
                Age = new Random().Next(1, 60),
                Email = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var properties = fakeUserExpected.ToDictionaryPropertyInfo();

            if (properties is not null)
            {
                Assert.IsTrue(properties.Count == 3);
                Assert.AreEqual(fakeUserExpected.Age.ToString(), properties["Age"]);
                Assert.AreEqual(fakeUserExpected.Email, properties["Email"]);
                Assert.AreEqual(fakeUserExpected.Name, properties["Name"]);
            }
        }

        [TestMethod]
        public void ToDictionaryPropertyInfo_Passing_Object_IncludeNullOrEmptyProperty_Returns_Properties_NameAndValue_Dictionary()
        {
            var fakeUserExpected = new FakeUser
            {
                Age = new Random().Next(1, 60),
                Email = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var properties = fakeUserExpected.ToDictionaryPropertyInfo(true);

            if (properties is not null)
            {
                Assert.IsTrue(properties.Count == 5);
                Assert.AreEqual(fakeUserExpected.Id.ToString(), properties["Id"]);
                Assert.AreEqual(fakeUserExpected.Age.ToString(), properties["Age"]);
                Assert.AreEqual(fakeUserExpected.Email, properties["Email"]);
                Assert.AreEqual(fakeUserExpected.Name, properties["Name"]);
                Assert.AreEqual(fakeUserExpected.Permissions?.ToString(), properties["Permissions"]);
            }
        }

        #endregion ToDictionaryPropertyInfo

        #region ToBytes

        [TestMethod]
        public void ToBytes_Passing_Object_Returns_Bytes()
        {
            var valueExpected = new FakeUser
            {
                Age = new Random().Next(1, 60),
                Email = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var bytes = valueExpected.ToBytes();

            var valueActual = bytes.ToObject<FakeUser>();

            Assert.AreEqual(valueExpected.Age, valueActual?.Age);
            Assert.AreEqual(valueExpected.Email, valueActual?.Email);
            Assert.AreEqual(valueExpected.Name, valueActual?.Name);
        }

        #endregion ToBytes
    }
}

