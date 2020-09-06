using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class JsonExtensionsTests
    {
        [TestMethod]
        public void ToSerialize_Passing_Objects_Returns_String_Values()
        {
            var userExpected = new FakeUser
            {
                Age = new Random().Next(1, 60),
                Email = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var userActual = userExpected.ToSerialize();

            StringAssert.Contains(userActual, userExpected.Age.ToString());
            StringAssert.Contains(userActual, userExpected.Email);
            StringAssert.Contains(userActual, userExpected.Name);
        }

        [TestMethod]
        public void ToDeserialize_Passing_String_Returns_Object()
        {
            var userExpected = new FakeUser
            {
                Age = new Random().Next(1, 60),
                Email = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var userSerialize = userExpected.ToSerialize();

            var userActual = userSerialize.ToDeserialize<FakeUser>(); 
            
            Assert.AreEqual(userExpected.Age, userActual.Age);
            Assert.AreEqual(userExpected.Email, userActual.Email);
            Assert.AreEqual(userExpected.Name, userActual.Name);
        }
    }
}
