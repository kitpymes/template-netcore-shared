// -----------------------------------------------------------------------
// <copyright file="Hash.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    /*
      Clase de utilidades
      Contiene las utilidades de cifrado
    */

    /// <summary>
    /// Clase de utilidades <c>Hash</c>.
    /// Contiene las utilidades de cifrado.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las utilidades necesarias para los cifrados.</para>
    /// </remarks>
    public static class Hash
    {
        private const byte _formatMarker = 0xC0;
        private const KeyDerivationPrf _prf = KeyDerivationPrf.HMACSHA512;
        private const int _saltLength = 512 / 8; // bits/1 byte = 64
        private const int _iterCount = 100000;
        private const int _headerByteLength = 13;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA512;

        #region CreateRandom

        /// <summary>
        /// Crea un texto con los caracteres aleatorios y el tamaño seteado.
        /// </summary>
        /// <param name="length">Cantidad de caracteres.</param>
        /// <param name="validChars">Caracteres que se usaran para crear el texto.</param>
        /// <returns>El texto.</returns>
        public static string CreateRandom(int length = 6, string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!¡@#$%€^&*?¿_-+")
        {
            var random = new Random();

            char[] chars = new char[length];

            validChars ??= "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!¡@#$%€^&*?¿_-+";

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }

        #endregion CreateRandom

        #region SHA256

        /// <summary>
        /// Cifra un texto.
        /// </summary>
        /// <param name="text">Texto a cifrar.</param>
        /// <returns>El texto cifrado.</returns>
        public static string CreateSHA256(string text)
        {
            using var algorithm = SHA256.Create();

            return Create(algorithm, text);
        }

        /// <summary>
        /// Verifica si el texto coincide con el texto cifrado.
        /// </summary>
        /// <param name="text">Texto a verificar.</param>
        /// <param name="hash">Texto cifrado.</param>
        /// <returns>ture | false.</returns>
        public static bool VerifySHA256(string text, string hash)
        {
            using var algorithm = SHA256.Create();

            return Verify(algorithm, text, hash);
        }

        #endregion SHA256

        #region SHA512

        /// <summary>
        /// Cifra un texto.
        /// </summary>
        /// <param name="text">Texto a cifrar.</param>
        /// <returns>El texto cifrado.</returns>
        public static string CreateSHA512(string text)
        {
            using var algorithm = SHA512.Create();

            return Create(algorithm, text);
        }

        /// <summary>
        /// Verifica si el texto coincide con el texto cifrado.
        /// </summary>
        /// <param name="text">Texto a verificar.</param>
        /// <param name="hash">Texto cifrado.</param>
        /// <returns>ture | false.</returns>
        public static bool VerifySHA512(string text, string hash)
        {
            using var algorithm = SHA512.Create();

            return Verify(algorithm, text, hash);
        }

        #endregion SHA512

        #region Password

        /// <summary>
        /// Crea una contraseña cifrada.
        /// </summary>
        /// <param name="plainPassword">Contraseña a cifrar.</param>
        /// <returns>string | ApplicationException: si el parámetro plainPassword es vacio o nulo.</returns>
        public static string CreatePassword(string plainPassword)
        {
            plainPassword.ToIsNullOrEmptyThrow(nameof(plainPassword));

            var salt = CreateSalt(_saltLength);

            var subkey = KeyDerivation.Pbkdf2(plainPassword, salt, _prf, _iterCount, _saltLength);

            var outputBytes = new byte[_headerByteLength + salt.Length + subkey.Length];

            outputBytes[0] = _formatMarker;

            WriteNetworkByteOrder(outputBytes, 1, (uint)_prf);
            WriteNetworkByteOrder(outputBytes, 5, _iterCount);
            WriteNetworkByteOrder(outputBytes, 9, _saltLength);

            Buffer.BlockCopy(salt, 0, outputBytes, _headerByteLength, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, _headerByteLength + _saltLength, subkey.Length);

            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        /// Verifica que una contraseña sea verdadera.
        /// </summary>
        /// <param name="hashedPassword">Contraseña cifrada.</param>
        /// <param name="plainPassword">Contraseña ingresada.</param>
        /// <returns>true | false | ApplicationException: si alguno de los parámetros hashedPassword o plainPassword son vacio o nulo.</returns>
        public static bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            plainPassword.ToIsNullOrEmptyThrow(nameof(plainPassword));
            hashedPassword.ToIsNullOrEmptyThrow(nameof(hashedPassword));

            byte[] decodedHashedPassword;

            try
            {
                decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            }
            catch (Exception)
            {
                return false;
            }

            if (decodedHashedPassword.Length == 0 || decodedHashedPassword[0] != _formatMarker)
            {
                return false;
            }

            try
            {
                var shaUInt = ReadNetworkByteOrder(decodedHashedPassword, 1);

                var verifyPrf = shaUInt switch
                {
                    0 => KeyDerivationPrf.HMACSHA1,
                    1 => KeyDerivationPrf.HMACSHA256,
                    2 => KeyDerivationPrf.HMACSHA512,
                    _ => KeyDerivationPrf.HMACSHA256,
                };

                if (verifyPrf != _prf)
                {
                    return false;
                }

                var verifyAlgorithmName = shaUInt switch
                {
                    0 => HashAlgorithmName.SHA1,
                    1 => HashAlgorithmName.SHA256,
                    2 => HashAlgorithmName.SHA512,
                    _ => HashAlgorithmName.SHA256,
                };

                if (verifyAlgorithmName != _hashAlgorithmName)
                {
                    return false;
                }

                int iterCountRead = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);

                if (iterCountRead != _iterCount)
                {
                    return false;
                }

                int saltLengthRead = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

                if (saltLengthRead != _saltLength)
                {
                    return false;
                }

                byte[] salt = new byte[_saltLength];

                Buffer.BlockCopy(decodedHashedPassword, _headerByteLength, salt, 0, salt.Length);

                int subkeyLength = decodedHashedPassword.Length - _headerByteLength - salt.Length;

                if (subkeyLength != _saltLength)
                {
                    return false;
                }

                byte[] expectedSubkey = new byte[subkeyLength];

                Buffer.BlockCopy(decodedHashedPassword, _headerByteLength + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                byte[] actualSubkey = new byte[_saltLength];

                actualSubkey = KeyDerivation.Pbkdf2(plainPassword, salt, _prf, _iterCount, subkeyLength);

                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }

        #endregion Password

        #region Private

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;

            for (var i = 0; i < a.Length; i++)
            {
                areSame &= a[i] == b[i];
            }

            return areSame;
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)buffer[offset + 0] << 24)
                | ((uint)buffer[offset + 1] << 16)
                | ((uint)buffer[offset + 2] << 8)
                | ((uint)buffer[offset + 3]);
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

        private static byte[] CreateSalt(int saltLength = 512 / 8)
        {
            byte[] randomBytes = new byte[saltLength];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomBytes);

            return randomBytes;
        }

        private static string Create(HashAlgorithm algorithm, string text)
        {
            text.ToIsNullOrEmptyThrow(nameof(text));

            algorithm?.ComputeHash(Encoding.UTF8.GetBytes(text));

            var hash = algorithm?.Hash;

            var result = hash.Select(x => x.ToString("x2", System.Globalization.CultureInfo.CurrentCulture));

            return string.Join(string.Empty, result);
        }

        private static bool Verify(HashAlgorithm algorithm, string text, string hash)
        {
            text.ToIsNullOrEmptyThrow(nameof(text));
            hash.ToIsNullOrEmptyThrow(nameof(hash));

            var result = Create(algorithm, text);

            return !string.IsNullOrWhiteSpace(result) && result == hash;
        }

        #endregion Private
    }
}
