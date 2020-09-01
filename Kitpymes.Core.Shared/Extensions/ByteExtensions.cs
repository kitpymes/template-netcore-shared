// -----------------------------------------------------------------------
// <copyright file="ByteExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.Serialization.Formatters.Binary;

    /*
        Clase de extensión ByteExtensions
        Contiene las extensiones del tipo byte
    */

    /// <summary>
    /// Clase de extensión <c>ByteExtensions</c>.
    /// Contiene las extensiones del tipo byte.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para el tipo byte.</para>
    /// </remarks>
    public static class ByteExtensions
    {
        /// <summary>
        /// Comprime bytes.
        /// </summary>
        /// <param name="bytes">Bytes a comprimir.</param>
        /// <returns>byte[] | ApplicationException si los bytes son nulo o la cantidad es menor o igual que cero.</returns>
        public static byte[] ToCompress(this byte[] bytes)
        {
            var validBytes = bytes.ToIsNullOrEmptyThrow(nameof(bytes));

            using var outputStream = new MemoryStream();

            using (var zip = new GZipStream(outputStream, CompressionMode.Compress))
            {
                zip.Write(bytes, 0, validBytes.Length);
            }

            return outputStream.ToArray();
        }

        /// <summary>
        /// Descomprime bytes.
        /// </summary>
        /// <param name="bytes">Bytes a descomprimir.</param>
        /// <returns>byte[] | ApplicationException si los bytes son nulo o la cantidad es menor o igual que cero.</returns>
        public static byte[] ToDecompress(this byte[] bytes)
        {
            var validBytes = bytes.ToIsNullOrEmptyThrow(nameof(bytes));

            using var outputStream = new MemoryStream();

            using (var inputStream = new MemoryStream(validBytes))

            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                gZipStream.CopyTo(outputStream);
            }

            return outputStream.ToArray();
        }

        /// <summary>
        /// Descomprime los bytes y lo convierte en un objeto.
        /// </summary>
        /// <typeparam name="TResult">Tipo de objeto a devolver.</typeparam>
        /// <param name="bytes">Bytes a convertir en un objeto.</param>
        /// <returns>TResult | ApplicationException si los bytes son nulo o la cantidad es menor o igual que cero.</returns>
        public static TResult ToDecompress<TResult>(this byte[] bytes)
        {
            var validBytes = bytes.ToIsNullOrEmptyThrow(nameof(bytes));

            using var memoryStream = new MemoryStream();

            var binaryFormatter = new BinaryFormatter();

            var decompressed = validBytes.ToDecompress();

            memoryStream.Write(decompressed, 0, decompressed.Length);

            memoryStream.Seek(0, SeekOrigin.Begin);

            return (TResult)binaryFormatter.Deserialize(memoryStream);
        }

        /// <summary>
        /// Convierte bytes en un objeto.
        /// </summary>
        /// <typeparam name="TResult">Tipo de objeto.</typeparam>
        /// <param name="bytes">Bytes a convertir en objeto.</param>
        /// <returns>El objeto.</returns>
        public static TResult ToObject<TResult>(this byte[] bytes)
        {
            var validBytes = bytes.ToIsNullOrEmptyThrow(nameof(bytes));

            using MemoryStream memoryStream = new MemoryStream(validBytes);

            var binaryFormatter = new BinaryFormatter();

            return (TResult)binaryFormatter.Deserialize(memoryStream);
        }
    }
}
