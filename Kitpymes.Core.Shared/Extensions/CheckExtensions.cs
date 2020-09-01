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
        #region IsNullOrEmpty

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
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsNullOrEmptyWithMessageThrow<TSource>(this TSource source, string message)
        => source.ToIsThrow(() => source.ToIsNullOrEmpty(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsNullOrEmptyThrow<TSource>(this TSource source, string paramName)
        => source.ToIsNullOrEmptyWithMessageThrow(Util.Messages.NullOrEmpty(paramName));

        #endregion IsNullOrEmpty

        #region IsNullOrAny

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
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>IEnumerable{TSource} | ApplicationException.</returns>
        [return: NotNull]
        public static IEnumerable<TSource> ToIsNullOrAnyWithMessageThrow<TSource>([NotNull] this IEnumerable<TSource> source, string message)
        => source.ToIsThrow(() => source.ToIsNullOrAny(), message);

        /// <summary>
        /// Verifica si una colección es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor de la colección a verificar.</typeparam>
        /// <param name="source">La colección a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>IEnumerable{TSource} | ApplicationException.</returns>
        [return: NotNull]
        public static IEnumerable<TSource> ToIsNullOrAnyThrow<TSource>([NotNull] this IEnumerable<TSource> source, string paramName)
        => source.ToIsNullOrAnyWithMessageThrow(Util.Messages.NullOrAny(paramName));

        #endregion IsNullOrAny

        #region IsDirectory

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="input">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsDirectory([NotNullWhen(false)] this string? input)
        => !Util.Check.IsDirectory(input).HasErrors;

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string? ToIsDirectoryWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsDirectory(), message);

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o parámetro.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string? ToIsDirectoryThrow(this string? source, string paramName)
        => source.ToIsDirectoryWithMessageThrow(Util.Messages.NotFound(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsDirectory

        #region IsFile

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="input">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsFile([NotNullWhen(false)] this string? input)
        => !Util.Check.IsFile(input).HasErrors;

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsFileWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsFile(), message);

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsFileThrow(this string? source, string paramName)
        => source.ToIsFileWithMessageThrow(Util.Messages.NotFound(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsFile

        #region IsFileExtension

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="input">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsFileExtension([NotNullWhen(false)] this string? input)
        => !Util.Check.IsFileExtension(input).HasErrors;

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsFileExtensionWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsFileExtension(), message);

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsFileExtensionThrow(this string? source, string paramName)
        => source.ToIsFileExtensionWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsFileExtension

        #region IsEmail

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsEmail(this string? source)
        => !Util.Check.IsEmail(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsEmailWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsEmail(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsEmailThrow(this string? source, string paramName)
        => source.ToIsEmailWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsEmail

        #region IsSubdomain

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsSubdomain(this string? source)
        => !Util.Check.IsSubdomain(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsSubdomainWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsSubdomain(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsSubdomainThrow(this string? source, string paramName)
        => source.ToIsSubdomainWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsSubdomain

        #region IsDomain

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsDomain(this string? source)
        => !Util.Check.IsDomain(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsDomainWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsDomain(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsDomainThrow(this string? source, string paramName)
        => source.ToIsDomainWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsDomain

        #region IsHostname

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsHostname(this string? source)
        => !Util.Check.IsHostname(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsHostnameWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsHostname(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsHostnameThrow(this string? source, string paramName)
        => source.ToIsHostnameWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsHostname

        #region IsRegexMatch

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="regex">Expresión regular.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsRegexMatch(this string? source, string regex)
        => !Util.Check.IsRegexMatch(regex, source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="regex">Expresión regular.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsRegexMatchWithMessageThrow(this string? source, string regex, string message)
        => source.ToIsThrow(() => !source.ToIsRegexMatch(regex), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="regex">Expresión regular.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsRegexMatchThrow(this string? source, string regex, string paramName)
        => source.ToIsRegexMatchWithMessageThrow(regex, Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsRegexMatch

        #region IsLess

        /// <summary>
        /// Verifica si un valor es menor que el valor mínimo.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsLess(this object? source, long min)
        => Util.Check.IsLess(min, source).HasErrors;

        /// <summary>
        /// Verifica si un valor es menor que el valor mínimo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsLessWithMessageThrow<TSource>(this TSource source, long min, string message)
        => source.ToIsThrow(() => source.ToIsLess(min), message);

        /// <summary>
        /// Verifica si un valor es menor que el valor mínimo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsLessThrow<TSource>(this TSource source, long min, string paramName)
        => source.ToIsLessWithMessageThrow(min, Util.Messages.Less(paramName, min));

        #endregion IsLess

        #region IsGreater

        /// <summary>
        /// Verifica si un valor es mayor que el valor máximo.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="max">El valor máximo.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsGreater(this object? source, long max)
        => Util.Check.IsGreater(max, source).HasErrors;

        /// <summary>
        /// Verifica si un valor es mayor que el valor máximo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="max">El valor máximo.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsGreaterWithMessageThrow<TSource>(this TSource source, long max, string message)
        => source.ToIsThrow(() => source.ToIsGreater(max), message);

        /// <summary>
        /// Verifica si un valor es mayor que el valor máximo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="max">El valor máximo.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsGreaterThrow<TSource>(this TSource source, long max, string paramName)
        => source.ToIsGreaterWithMessageThrow(max, Util.Messages.Greater(paramName, max));

        #endregion IsGreater

        #region IsName

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsName(this string? source)
        => !Util.Check.IsName(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsNameWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsName(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsNameThrow(this string? source, string paramName)
        => source.ToIsNameWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsName

        #region IsEqual

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="compare">El valor a comparar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsEqual(this object? source, object? compare)
        => !Util.Check.IsEqual(source, compare).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="compare">El valor a comparar.</param>
        /// <param name="message">Mensaje de la validación.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static TSource ToIsEqualWithMessageThrow<TSource>(this TSource source, TSource compare, string message)
        => source.ToIsThrow(() => !source.ToIsEqual(compare), message);

        /// <summary>
        /// Verifica si un valor es nulo o vacío.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="compare">El valor a comparar.</param>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <param name="paramNameCompare">Nombre del parámetro a comparar.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static TSource ToIsEqualThrow<TSource>(this TSource source, TSource compare, string paramName, string paramNameCompare)
        => source.ToIsEqualWithMessageThrow(compare, Util.Messages.NotEquals(paramName, paramNameCompare));

        #endregion IsEqual

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Función a evaluar.</param>
        /// <param name="message">Mensaje a mostrar en la ApplicationException.</param>
        /// <returns>TSource | ApplicationException: si source es nulo o si la evaluación del predicado es verdadero.</returns>
        [return: NotNull]
        public static TSource ToIsThrow<TSource>(this TSource source, Func<bool> predicate, string message)
        => source.ToIsErrors(predicate) ? throw new ApplicationException(message) : source;

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Función a evaluar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsErrors<TSource>([NotNullWhen(false)] this TSource source, Func<bool> predicate)
        => source is null || predicate is null || predicate();
    }
}
