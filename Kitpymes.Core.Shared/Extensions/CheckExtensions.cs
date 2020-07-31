// -----------------------------------------------------------------------
// <copyright file="CheckExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /*
      Clase de extensión CheckExtensions
      Contiene las extensiones de las Excepciones
    */

    /// <summary>
    /// Clase de extensión <c>CheckExtensions</c>.
    /// Contiene las extensiones para validar argumentos.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las validaciones.</para>
    /// </remarks>
    public static class CheckExtensions
    {
        #region NullOrEmpty

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNullOrEmpty<TSource>([NotNullWhen(false)] this TSource source)
        => source is null || (source is string && string.IsNullOrWhiteSpace(source as string)) || source.Equals(default(TSource));

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} is null or empty".</returns>
        public static TSource ToThrowIfNullOrEmpty<TSource>([NotNull] this TSource source, string paramName)
        => source.ToThrow(() => source.ToIsNullOrEmpty(), Util.Messages.NullOrEmpty(paramName));

        #endregion NullOrEmpty

        #region NullOrAny

        /// <summary>
        /// Verifica si una colección es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNullOrAny<TSource>([NotNullWhen(false)] this IEnumerable<TSource> source)
        => source is null || !source.Any();

        /// <summary>
        /// Verifica si una colección es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor de la colección a verificar.</typeparam>
        /// <param name="source">La colección a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>v{TSource} | ApplicationException: "{paramName} is null or not values".</returns>
        public static IEnumerable<TSource> ToThrowIfNullOrAny<TSource>([NotNull] this IEnumerable<TSource> source, string paramName)
        => source.ToThrow(() => source.ToIsNullOrAny(), Util.Messages.NullOrAny(paramName));

        #endregion NullOrAny

        #region NotFound

        /// <summary>
        /// Verifica si no se encuentra un valor.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} is not found".</returns>
        public static TSource ToThrowIfNotFound<TSource>([NotNull] this TSource source, string paramName)
        => source.ToThrow(() => source.ToIsNullOrEmpty(), Util.Messages.NotFound(paramName));

        #endregion NotFound

        #region NotFoundDirectory

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>string | ApplicationException: "{paramName} is not found".</returns>
        public static string? ToThrowIfNotFoundDirectory([NotNull] this string? source, string paramName)
        => source.ToThrow(() => !source.ToDirectoryExists(), Util.Messages.NotFound(paramName));

        #endregion NotFoundDirectory

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Función a evaluar.</param>
        /// <param name="message">Mensaje a mostrar en la ApplicationException.</param>
        /// <returns>TSource | ApplicationException: si source es nulo o si la evaluación del predicado es verdadero.</returns>
        public static TSource ToThrow<TSource>([NotNull] this TSource source, Func<bool> predicate, string message)
        => source is null || predicate is null || predicate() ? throw Util.Exceptions.ToThrow(message) : source;

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Función a evaluar.</param>
        /// <returns>true | false.</returns>
        public static bool ToHasErrors<TSource>(this TSource source, Func<bool> predicate)
        => source is null || predicate is null || predicate();
    }
}
