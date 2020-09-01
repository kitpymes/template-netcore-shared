// -----------------------------------------------------------------------
// <copyright file="GenericExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    /*
      Clase de extensión GenericExtensions
      Contiene las extensiones de los Generic
    */

    /// <summary>
    /// Clase de extensión <c>GenericExtensions</c>.
    /// Contiene las extensiones de los Generic.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para los valores genericos.</para>
    /// </remarks>
    public static class GenericExtensions
    {
        /// <summary>
        /// Obtiene el valor por defecto.
        /// </summary>
        /// <typeparam name="T">Tipo del valor.</typeparam>
        /// <param name="value">El valor a obtener su valor por defecto.</param>
        /// <returns>El valor por defecto.</returns>
        public static object? ToDefaultValue<T>(this T value)
        => value?.GetType().ToDefaultValue();

        /// <summary>
        /// Convierte un valor en bytes.
        /// </summary>
        /// <typeparam name="T">Tipo del valor.</typeparam>
        /// <param name="value">Valor a convertir.</param>
        /// <returns>byte[].</returns>
        public static byte[] ToBytes<T>(this T value)
            where T : class
        {
            using var memoryStream = new MemoryStream();

            var binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(memoryStream, value);

            return memoryStream.ToArray();
        }

        /// <summary>
        /// Obtiene una lista nombre y valor.
        /// </summary>
        /// <param name="input">Objeto a leer sus propieades.</param>
        /// <param name="includeNullOrEmptyProperty">Si es false, no incluye las propiedades cuyo valor sea nulo ni vacio.</param>
        /// <returns>IDictionary.</returns>
        public static IDictionary ToDictionaryPropertyInfo<T>(this T input, bool includeNullOrEmptyProperty = false)
            where T : class
        {
            return input.ToIsNullOrEmptyThrow(nameof(input)).GetType().GetProperties()
                .Where(property =>
                {
                    var name = property.Name;
                    var value = property.GetValue(input, null);

                    if (!includeNullOrEmptyProperty)
                    {
                        return value is bool
                           || (value is string && !string.IsNullOrWhiteSpace(value?.ToString()))
                           || (value != null && !value.Equals(value.ToDefaultValue()));
                    }

                    return true;
                })
                .ToDictionary(
                    property => property.Name.ToFirstLetterUpper(),
                    property => property.GetValue(input, null)?.ToString());
        }
    }
}
