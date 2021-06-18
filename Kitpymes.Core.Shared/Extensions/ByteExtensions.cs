// -----------------------------------------------------------------------
// <copyright file="ByteExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Xml.Serialization;

    /*
        Clase de extensi�n ByteExtensions
        Contiene las extensiones del tipo byte
    */

    /// <summary>
    /// Clase de extensi�n <c>ByteExtensions</c>.
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
        /// <returns>byte[] | ApplicationException: si bytes es nulo o no contiene bytes.</returns>
        [return: NotNull]
        public static byte[] ToCompress(this byte[] bytes)
        {
            bytes.ToIsNullOrAnyThrow(nameof(bytes));

            using var outputStream = new MemoryStream();

            using (var zip = new GZipStream(outputStream, CompressionMode.Compress))
            {
                zip.Write(bytes, 0, bytes.Count());
            }

            return outputStream.ToArray();
        }

        /// <summary>
        /// Descomprime bytes.
        /// </summary>
        /// <param name="bytes">Bytes a descomprimir.</param>
        /// <returns>byte[] | ApplicationException: si bytes es nulo o no contiene bytes.</returns>
        [return: NotNull]
        public static byte[] ToDecompress(this byte[] bytes)
        {
            bytes.ToIsNullOrEmptyThrow(nameof(bytes));

            using var outputStream = new MemoryStream();

            using (var inputStream = new MemoryStream(bytes))

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
        /// <returns>TResult | null | ApplicationException: si bytes es nulo o no contiene bytes.</returns>
        public static TResult? ToDecompress<TResult>(this byte[] bytes)
        {
            bytes.ToIsNullOrAnyThrow(nameof(bytes));

            using MemoryStream memoryStream = new ();

            var decompressed = bytes.ToDecompress();

            memoryStream.Write(decompressed, 0, decompressed.Length);

            memoryStream.Seek(0, SeekOrigin.Begin);

            var serializer = new XmlSerializer(typeof(TResult));

            return (TResult?)serializer.Deserialize(memoryStream);
        }

        /// <summary>
        /// Convierte bytes en un objeto.
        /// </summary>
        /// <typeparam name="TResult">Tipo de objeto.</typeparam>
        /// <param name="bytes">Bytes a convertir en objeto.</param>
        /// <returns>TResult | null | ApplicationException: si bytes es nulo o no contiene bytes.</returns>
        public static TResult? ToObject<TResult>(this byte[] bytes)
        {
            bytes.ToIsNullOrAnyThrow(nameof(bytes));

            using MemoryStream memoryStream = new (bytes);

            var serializer = new XmlSerializer(typeof(TResult));

            return (TResult?)serializer.Deserialize(memoryStream);
        }
    }
}
