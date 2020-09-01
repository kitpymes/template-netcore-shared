using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Utilities.Tests
{
    [TestClass]
    public class HashTests
    {
        #region CreateRandom

        [DataTestMethod]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        public void CreateRandom_WithDefaultAllowableCharacters_Passing_ValidArguments_Returns_String_Random_Value(int length)
        {
            var hash = Util.Hash.CreateRandom(length);

            Assert.IsTrue(hash.Length == length);
        }

        [DataTestMethod]
        [DataRow(8, "ae")]
        [DataRow(5, "aeiou123456")]
        [DataRow(10, "7890ABFLFLFFL")]
        [DataRow(20, "qazwscefbthmiklp09876543")]
        public void CreateRandom_WithCustomAllowableCharacters_Passing_Valid_Arguments_Returns_String_Random_Value(int length, string allowableCharacters)
        {
            var hash = Util.Hash.CreateRandom(length, allowableCharacters);

            Assert.IsTrue(hash.Length == length);
        }

        #endregion CreateRandom

        #region SHA256

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void CreateSHA256_Passing_Invalid_Arguments_Returns_ApplicationException(string text)
        {
            Assert.ThrowsException<ApplicationException>(() => Util.Hash.CreateSHA256(text));
        }

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(HashTests))]
        [DataRow("sdlfkjldsfksdfñl·%$·&$/U&/)(&()?=)¿?^P*^PÑL,.,.lñ´ñ+98098'098098")]
        public void CreateAndVerifySHA256_Passing_Valid_Arguments_Returns_True(string text)
        {
            var hash = Util.Hash.CreateSHA256(text);

            var isValid = Util.Hash.VerifySHA256(text, hash);

            Assert.IsTrue(isValid);
        }

        #endregion SHA256

        #region SHA512

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void CreateSHA512_Passing_Invalid_Arguments_Returns_ApplicationException(string text)
        {
            Assert.ThrowsException<ApplicationException>(() => Util.Hash.CreateSHA512(text));
        }

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(HashTests))]
        [DataRow("sdlfkjldsfksdfñl·%$·&$/U&/)(&()?=)¿?^P*^PÑL,.,.lñ´ñ+98098'098098")]
        public void CreateAndVerifySHA512_Passing_Valid_Arguments_Returns_True(string text)
        {
            var hash = Util.Hash.CreateSHA512(text);

            var isValid = Util.Hash.VerifySHA512(text, hash);

            Assert.IsTrue(isValid);
        }

        #endregion SHA512

        #region Password

        [DataTestMethod]
        [DataRow("Password")]
        [DataRow(nameof(HashTests))]
        [DataRow("sdlfkjldsfksdfñl·%$·&$/U&/)(&()?=)¿?^P*^PÑL,.,.lñ´ñ+98098'098098")]
        public void CreateAndVerifyPassword_Passing_Valid_Arguments_Returns_IsValid(string plainPassword)
        {
            var hashPassword = Util.Hash.CreatePassword(plainPassword);

            var isValid = Util.Hash.VerifyPassword(hashPassword, plainPassword);

            Assert.IsTrue(isValid);
        }

        #endregion Password
    }
}
