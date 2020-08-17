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
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNullOrEmpty(this object? source)
        => Util.Check.IsNullOrEmpty(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} is null or empty".</returns>
        [return: NotNull]
        public static TSource ToThrowIfNullOrEmpty<TSource>(this TSource source, string paramName)
        => source.ToThrow(() => source.ToIsNullOrEmpty(), Util.Messages.NullOrEmpty(paramName));

        #endregion NullOrEmpty

        #region NullOrAny

        /// <summary>
        /// Verifica si una colección es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="input">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNullOrAny<TSource>([NotNullWhen(false)] this IEnumerable<TSource>? input)
        => Util.Check.IsNullOrAny(input).HasErrors;

        /// <summary>
        /// Verifica si una colección es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor de la colección a verificar.</typeparam>
        /// <param name="source">La colección a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>IEnumerable{TSource} | ApplicationException: "{paramName} is null or not values".</returns>
        [return: NotNull]
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
        [return: NotNull]
        public static TSource ToThrowIfNotFound<TSource>(this TSource source, string paramName)
        => source.ToThrow(() => source.ToIsNullOrEmpty(), Util.Messages.NotFound(paramName));

        #endregion NotFound

        #region NotFoundDirectory

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="input">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNotFoundDirectory([NotNullWhen(false)] this string? input)
        => Util.Check.IsDirectory(input).HasErrors;

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>string | ApplicationException: "{valueOrParamName} is not found".</returns>
        [return: NotNull]
        public static string? ToThrowIfNotFoundDirectory(this string? source, string paramName)
        => source.ToThrow(() => source.ToIsNotFoundDirectory(), Util.Messages.NotFound(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion NotFoundDirectory

        #region NotFoundFile

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="input">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNotFoundFile([NotNullWhen(false)] this string? input)
        => Util.Check.IsFile(input).HasErrors;

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>string | ApplicationException: "{valueOrParamName} is not found".</returns>
        [return: NotNull]
        public static string ToThrowIfNotFoundFile(this string? source, string paramName)
        => source.ToThrow(() => source.ToIsNotFoundFile(), Util.Messages.NotFound(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion NotFoundFile

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Función a evaluar.</param>
        /// <param name="message">Mensaje a mostrar en la ApplicationException.</param>
        /// <returns>TSource | ApplicationException: si source es nulo o si la evaluación del predicado es verdadero.</returns>
        [return: NotNull]
        public static TSource ToThrow<TSource>(this TSource source, Func<bool> predicate, string message)
        => source.ToHasErrors(predicate) ? throw new ApplicationException(message) : source;

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Función a evaluar.</param>
        /// <returns>true | false.</returns>
        public static bool ToHasErrors<TSource>([NotNullWhen(false)] this TSource source, Func<bool> predicate)
        => source is null || predicate is null || predicate();
    }
}
