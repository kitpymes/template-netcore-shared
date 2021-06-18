// -----------------------------------------------------------------------
// <copyright file="Hash.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

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
        #region CreateRandom

        /// <summary>
        /// Crea un texto con los caracteres aleatorios y el tamaño seteado.
        /// </summary>
        /// <param name="length">Cantidad de caracteres.</param>
        /// <param name="validChars">Caracteres que se usaran para crear el texto.</param>
        /// <returns>string | ApplicationException: si length es menor que 1 o validChars es nulo o vacio.</returns>
        public static string CreateRandom(int length = 6, string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!¡@#$%€^&*?¿_-+")
        {
            length.ToIsLessThrow(1, nameof(length));
            validChars.ToIsNullOrEmptyThrow(nameof(validChars));

            var random = new Random();

            char[] chars = new char[length];

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
        /// <returns>string | ApplicationException: si texto es nulo o vacio.</returns>
        [return: NotNull]
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
        /// <returns>string | ApplicationException: si texto es nulo o vacio.</returns>
        [return: NotNull]
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

        #region Private

        [return: NotNull]
        private static string Create(HashAlgorithm algorithm, string text)
        {
            text.ToIsNullOrEmptyThrow(nameof(text));

            algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));

            var hash = algorithm.Hash.ToIsNullOrAnyThrow(nameof(algorithm.Hash));

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
