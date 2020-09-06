using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    /*
        Clase base BaseTests

        [TestClass]
        public class TimeSpanExtensionsTests : BaseTests
        {
            [DataTestMethod]
            [DataRow("aeiou", "a", "eiou")]
            public void ToRemove_Passing_ValidValue_Returns_ValueWithoutRemovedChars(string value, string remove, string expected)
            {
                this.Trace($"Remover los caracteres '{remove}' del texto '{value}', resultado esperado '{expected}'");
    
                var actual = value.ToRemove(remove);
    
                Assert.AreEqual(expected, actual);
            }
        }
    */
    [TestClass]
    public abstract class BaseTests
    {
        public TestContext? TestContext { get; set; }

        [AssemblyInitialize]
        public static void ClassInitialize(TestContext TestContext)
        => Console.WriteLine($"ClassName: {TestContext.FullyQualifiedTestClassName}");

        protected void Trace(string? message = null)
        {
            Console.WriteLine($"TestName: {TestContext?.TestName}");

            if (!string.IsNullOrWhiteSpace(message))
                Console.WriteLine($"TestMessage: {message}");
        }

        [TestCleanup]
        public void Cleanup()
        => Console.WriteLine($"TestResult: {TestContext?.CurrentTestOutcome.ToString()}");
    }
}
