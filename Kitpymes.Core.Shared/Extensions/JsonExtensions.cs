// -----------------------------------------------------------------------
// <copyright file="JsonExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Text.Json;

    /*
      Clase de extensión JsonExtensions
      Contiene las extensiones de JsonSerializer
    */

    /// <summary>
    /// Clase de extensión <c>JsonExtensions</c>.
    /// Contiene las extensiones de JsonSerializer.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para JsonSerializer.</para>
    /// </remarks>
    public static class JsonExtensions
    {
        /// <summary>
        /// Serializa un objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto.</typeparam>
        /// <param name="value">Valor del objeto.</param>
        /// <param name="action">Opciones de configuración.</param>
        /// <returns>string | null.</returns>
        public static string ToSerialize<T>(this T value, Action<JsonSerializerOptions>? action = null)
        => JsonSerializer.Serialize(value.ToIsNullOrEmptyThrow(nameof(value)), typeof(T), action.ToConfigureOrDefault());

        /// <summary>
        /// Deserializa un objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto.</typeparam>
        /// <param name="value">Valor del objeto.</param>
        /// <param name="action">Opciones de configuración.</param>
        /// <returns>T | null.</returns>
        public static T? ToDeserialize<T>(this string value, Action<JsonSerializerOptions>? action = null)
        => JsonSerializer.Deserialize<T>(value.ToIsNullOrEmptyThrow(nameof(value)), action.ToConfigureOrDefault());
    }
}
