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
      Clase de extensi�n CheckExtensions
      Contiene las extensiones de las Excepciones
    */

    /// <summary>
    /// Clase de extensi�n <c>CheckExtensions</c>.
    /// Contiene las extensiones para validar argumentos.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las validaciones.</para>
    /// </remarks>
    public static class CheckExtensions
    {
        #region IsNullOrEmpty

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNullOrEmpty([NotNullWhen(false)] this object? source)
        => Util.Check.IsNullOrEmpty(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsNullOrEmptyWithMessageThrow<TSource>(this TSource source, string message)
        => source.ToIsThrow(() => source.ToIsNullOrEmpty(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsNullOrEmptyThrow<TSource>(this TSource source, string paramName)
        => source.ToIsNullOrEmptyWithMessageThrow(Util.Messages.NullOrEmpty(paramName));

        #endregion IsNullOrEmpty

        #region IsNullOrAny

        /// <summary>
        /// Verifica si una colecci�n es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsNullOrAny<TSource>([NotNullWhen(true)] this IEnumerable<TSource>? source)
        => Util.Check.IsNullOrAny(source).HasErrors;

        /// <summary>
        /// Verifica si una colecci�n es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor de la colecci�n a verificar.</typeparam>
        /// <param name="source">La colecci�n a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>IEnumerable{TSource} | ApplicationException.</returns>
        [return: NotNull]
        public static IEnumerable<TSource> ToIsNullOrAnyWithMessageThrow<TSource>(this IEnumerable<TSource>? source, string message)
        => source.ToIsThrow(() => source.ToIsNullOrAny(), message);

        /// <summary>
        /// Verifica si una colecci�n es nula o no contiene valores.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor de la colecci�n a verificar.</typeparam>
        /// <param name="source">La colecci�n a verificar.</param>
        /// <param name="paramName">Nombre de la variable o par�metro.</param>
        /// <returns>IEnumerable{TSource} | ApplicationException.</returns>
        [return: NotNull]
        public static IEnumerable<TSource> ToIsNullOrAnyThrow<TSource>(this IEnumerable<TSource>? source, string paramName)
        => source.ToIsNullOrAnyWithMessageThrow(Util.Messages.NullOrAny(paramName));

        #endregion IsNullOrAny

        #region IsGreater

        /// <summary>
        /// Verifica si un valor es mayor que el valor m�ximo.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="max">El valor m�ximo.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsGreater([NotNullWhen(true)] this object? source, long max)
        => Util.Check.IsGreater(max, source).HasErrors;

        /// <summary>
        /// Verifica si un valor es mayor que el valor m�ximo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="max">El valor m�ximo.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsGreaterWithMessageThrow<TSource>(this TSource source, long max, string message)
        => source.ToIsThrow(() => source.ToIsGreater(max), message);

        /// <summary>
        /// Verifica si un valor es mayor que el valor m�ximo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="max">El valor m�ximo.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsGreaterThrow<TSource>(this TSource source, long max, string paramName)
        => source.ToIsGreaterWithMessageThrow(max, Util.Messages.Greater(paramName, max));

        #endregion IsGreater

        #region IsLess

        /// <summary>
        /// Verifica si un valor es menor que el valor m�nimo.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor m�nimo.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsLess([NotNullWhen(true)] this object? source, long min)
        => Util.Check.IsLess(min, source).HasErrors;

        /// <summary>
        /// Verifica si un valor es menor que el valor m�nimo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor m�nimo.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsLessWithMessageThrow<TSource>(this TSource source, long min, string message)
        => source.ToIsThrow(() => source.ToIsLess(min), message);

        /// <summary>
        /// Verifica si un valor es menor que el valor m�nimo.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor m�nimo.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsLessThrow<TSource>(this TSource source, long min, string paramName)
        => source.ToIsLessWithMessageThrow(min, Util.Messages.Less(paramName, min));

        #endregion IsLess

        #region IsEqual

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="compare">El valor a comparar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsEqual([NotNullWhen(true)] this object? source, object? compare)
        => !Util.Check.IsEqual(source, compare).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="compare">El valor a comparar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static TSource ToIsEqualWithMessageThrow<TSource>(this TSource source, TSource compare, string message)
        => source.ToIsThrow(() => !source.ToIsEqual(compare), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="compare">El valor a comparar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <param name="paramNameCompare">Nombre del par�metro a comparar.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static TSource ToIsEqualThrow<TSource>(this TSource source, TSource compare, string paramName, string paramNameCompare)
        => source.ToIsEqualWithMessageThrow(compare, Util.Messages.NotEquals(paramName, paramNameCompare));

        #endregion IsEqual

        #region IsRange

        /// <summary>
        /// Verifica si un valor se encuentra entre un rango.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor m�nimo.</param>
        /// <param name="max">El valor m�ximo.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsRange([NotNullWhen(true)] this object? source, long min, long max)
        => Util.Check.IsRange(min, max, source).HasErrors;

        /// <summary>
        /// Verifica si un valor se encuentra entre un rango.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor m�nimo.</param>
        /// <param name="max">El valor m�ximo.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsRangeWithMessageThrow<TSource>(this TSource source, long min, long max, string message)
        => source.ToIsThrow(() => source.ToIsRange(min, max), message);

        /// <summary>
        /// Verifica si un valor se encuentra entre un rango.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="min">El valor m�nimo.</param>
        /// <param name="max">El valor m�ximo.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static TSource ToIsRangeThrow<TSource>(this TSource source, long min, long max, string paramName)
        => source.ToIsRangeWithMessageThrow(min, max, Util.Messages.Range(paramName, min, max));

        #endregion IsRange

        #region IsRegexMatch

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="regex">Expresi�n regular.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsRegexMatch([NotNullWhen(true)] this string? source, string regex)
        => !Util.Check.IsRegexMatch(regex, source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="regex">Expresi�n regular.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsRegexMatchWithMessageThrow(this string? source, string regex, string message)
        => source.ToIsThrow(() => !source.ToIsRegexMatch(regex), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="regex">Expresi�n regular.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsRegexMatchThrow(this string? source, string regex, string paramName)
        => source.ToIsRegexMatchWithMessageThrow(regex, Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsRegexMatch

        #region IsName

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsName([NotNullWhen(true)] this string? source)
        => !Util.Check.IsName(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsNameWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsName(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsNameThrow(this string? source, string paramName)
        => source.ToIsNameWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsName

        #region IsEmail

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsEmail([NotNullWhen(true)] this string? source)
        => !Util.Check.IsEmail(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsEmailWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsEmail(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsEmailThrow(this string? source, string paramName)
        => source.ToIsEmailWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsEmail

        #region IsDirectory

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="input">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsDirectory([NotNullWhen(true)] this string? input)
        => !Util.Check.IsDirectory(input).HasErrors;

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string? ToIsDirectoryWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsDirectory(), message);

        /// <summary>
        /// Verifica si existe el path de un directorio.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre de la variable o par�metro.</param>
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
        [return: NotNull]
        public static bool ToIsFile([NotNullWhen(true)] this string? input)
        => !Util.Check.IsFile(input).HasErrors;

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsFileWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsFile(), message);

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
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
        [return: NotNull]
        public static bool ToIsFileExtension([NotNullWhen(true)] this string? input)
        => !Util.Check.IsFileExtension(input).HasErrors;

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsFileExtensionWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsFileExtension(), message);

        /// <summary>
        /// Verifica si existe una fila.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>string | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsFileExtensionThrow(this string? source, string paramName)
        => source.ToIsFileExtensionWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsFileExtension

        #region IsSubdomain

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsSubdomain([NotNullWhen(true)] this string? source)
        => !Util.Check.IsSubdomain(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsSubdomainWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsSubdomain(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsSubdomainThrow(this string? source, string paramName)
        => source.ToIsSubdomainWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsSubdomain

        #region IsDomain

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsDomain([NotNullWhen(true)] this string? source)
        => !Util.Check.IsDomain(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsDomainWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsDomain(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsDomainThrow(this string? source, string paramName)
        => source.ToIsDomainWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsDomain

        #region IsHostname

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsHostname([NotNullWhen(true)] this string? source)
        => !Util.Check.IsHostname(source).HasErrors;

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsHostnameWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsHostname(), message);

        /// <summary>
        /// Verifica si un valor es nulo o vac�o.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsHostnameThrow(this string? source, string paramName)
        => source.ToIsHostnameWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsHostname

        #region IsDigit

        /// <summary>
        /// Verifica si un valor contiene alg�n digito decimal.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsDigit([NotNullWhen(true)] this string? source)
        => !Util.Check.IsDigit(source).HasErrors;

        /// <summary>
        /// Verifica si un valor contiene alg�n digito decimal.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsDigitWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsDigit(), message);

        /// <summary>
        /// Verifica si un valor contiene alg�n digito decimal.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsDigitThrow(this string? source, string paramName)
        => source.ToIsDigitWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsDigit

        #region IsUniqueChars

        /// <summary>
        /// Verifica si un valor contiene caracteres repetidos.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsUniqueChars([NotNullWhen(true)] this string? source)
        => !Util.Check.IsUniqueChars(source).HasErrors;

        /// <summary>
        /// Verifica si un valor contiene caracteres repetidos.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsUniqueCharsWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsUniqueChars(), message);

        /// <summary>
        /// Verifica si un valor contiene caracteres repetidos.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsUniqueCharsThrow(this string? source, string paramName)
        => source.ToIsUniqueCharsWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsUniqueChars

        #region IsEspecialChars

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter especial.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsEspecialChars([NotNullWhen(true)] this string? source)
        => !Util.Check.IsEspecialChars(source).HasErrors;

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter especial.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsEspecialCharsWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsEspecialChars(), message);

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter especial.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsEspecialCharsThrow(this string? source, string paramName)
        => source.ToIsEspecialCharsWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsEspecialChars

        #region IsLowercase

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter en min�scula.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsLowercase([NotNullWhen(true)] this string? source)
        => !Util.Check.IsLowercase(source).HasErrors;

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter en min�scula.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsLowercaseWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsLowercase(), message);

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter en min�scula.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsLowercaseThrow(this string? source, string paramName)
        => source.ToIsLowercaseWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsLowercase

        #region IsUppercase

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter en may�scula.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsUppercase([NotNullWhen(true)] this string? source)
        => !Util.Check.IsUppercase(source).HasErrors;

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter en may�scula.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="message">Mensaje de la validaci�n.</param>
        /// <returns>TSource | ApplicationException.</returns>
        [return: NotNull]
        public static string ToIsUppercaseWithMessageThrow(this string? source, string message)
        => source.ToIsThrow(() => !source.ToIsUppercase(), message);

        /// <summary>
        /// Verifica si un valor contiene alg�n caracter en may�scula.
        /// </summary>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="paramName">Nombre del par�metro.</param>
        /// <returns>TSource | ApplicationException: "{paramName} has invalid format".</returns>
        [return: NotNull]
        public static string ToIsUppercaseThrow(this string? source, string paramName)
        => source.ToIsUppercaseWithMessageThrow(Util.Messages.InvalidFormat(string.IsNullOrWhiteSpace(source) ? paramName : source));

        #endregion IsUppercase

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Funci�n a evaluar.</param>
        /// <param name="message">Mensaje a mostrar en la ApplicationException.</param>
        /// <returns>TSource | ApplicationException: si source es nulo o si la evaluaci�n del predicado es verdadero.</returns>
        [return: NotNull]
        public static TSource ToIsThrow<TSource>(this TSource source, Func<bool> predicate, string message)
        => source.ToIsErrors(predicate) ? throw new ApplicationException(message) : source;

        /// <summary>
        /// Valida si una o varias condiciones son verdaderas.
        /// </summary>
        /// <typeparam name="TSource">Tipo del valor a verificar.</typeparam>
        /// <param name="source">El valor a verificar.</param>
        /// <param name="predicate">Funci�n a evaluar.</param>
        /// <returns>true | false.</returns>
        public static bool ToIsErrors<TSource>([NotNullWhen(false)] this TSource source, Func<bool> predicate)
        => source is null || predicate is null || predicate();
    }
}