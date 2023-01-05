// -----------------------------------------------------------------------
// <copyright file="VerifyExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Kitpymes.Core.Shared.Util;

/*
    Clase de extensión VerifyExtensions
    Contiene las extensiones de las Excepciones
*/

/// <summary>
/// Clase de extensión <c>VerifyExtensions</c>.
/// Contiene las extensiones para validar argumentos.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para las validaciones.</para>
/// </remarks>
public static class VerifyExtensions
{
    #region NullOrEmpty

    /// <summary>
    /// Verifica si un valor es nulo o vacío.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this object? value)
    => value is null || (value is string && string.IsNullOrWhiteSpace(value.ToString()));

    /// <summary>
    /// Verifica si un valor es nulo o vacío.
    /// </summary>
    /// <typeparam name="TValue">Tipo del valor a verificar.</typeparam>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{name} is null or empty".</returns>
    public static TValue ThrowIfNullOrEmpty<TValue>([NotNull] this TValue? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => value.IsNullOrEmpty(), Util.Messages.NullOrEmpty(name));

    #endregion NullOrEmpty

    #region NullOrAny

    /// <summary>
    /// Verifica si una colección es nula o no contiene valores.
    /// </summary>
    /// <typeparam name="TValue">Tipo del valor a verificar.</typeparam>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsNullOrAny<TValue>([NotNullWhen(false)] this IEnumerable<TValue>? value)
    {
        if (value is null)
        {
            return true;
        }

        switch (value)
        {
            case string s when s.Length < 1:
            case Array a when a.Length < 1:
            case ICollection c when c.Count < 1:
                return true;
            default:
                return value.Cast<object>()?.Any() == false;
        }
    }

    /// <summary>
    /// Verifica si una colección es nula o no contiene valores.
    /// </summary>
    /// <typeparam name="TValue">Tipo del valor de la colección a verificar.</typeparam>
    /// <param name="value">La colección a verificar.</param>
    /// <param name="name">Nombre de la variable o parámetro.</param>
    /// <returns>IEnumerable{TValue} | ApplicationException: "{name} is null or not values".</returns>
    public static IEnumerable<TValue> ThrowIfNullOrAny<TValue>([NotNull] this IEnumerable<TValue>? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => value.IsNullOrAny(), Util.Messages.NullOrAny(name));

    #endregion NullOrAny

    #region Greater

    /// <summary>
    /// Verifica si un valor es mayor que el valor máximo permitido.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="max">El valor máximo.</param>
    /// <returns>true | false.</returns>
    public static bool IsGreater([NotNullWhen(true)] this object? value, long max)
    {
        if (value.IsNullOrEmpty())
        {
            return true;
        }

        switch (value)
        {
            case Enum _ when (int)value > max:
            case string s when s.Length > max:
            case sbyte sb when sb > max:
            case short sh when sh > max:
            case int inte when inte > max:
            case long lo when lo > max:
            case byte by when by > max:
            case ushort us when us > max:
            case uint ui when ui > max:
            case ulong ul when Convert.ToInt64(ul) > max:
            case char ch when ch > max:
            case float fl when fl > max:
            case double d when d > max:
            case decimal de when de > max:
            case Array a when a.Length > max:
            case ICollection c when c.Count > max:
            case IEnumerable e when e.Cast<object>().Count() > max:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Verifica si un valor es mayor que el valor máximo permitido.
    /// </summary>
    /// <typeparam name="TValue">Tipo del valor a verificar.</typeparam>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="max">El valor máximo.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{name} must be less than {max}".</returns>
    public static TValue ThrowIfGreater<TValue>([NotNull] this TValue? value, long max, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => value.IsGreater(max), Util.Messages.Greater(name, max));

    #endregion Greater

    #region Less

    /// <summary>
    /// Verifica si un valor es menor que el valor mínimo permitido.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="min">El valor mínimo.</param>
    /// <returns>true | false.</returns>
    public static bool IsLess([NotNullWhen(true)] this object? value, long min)
    {
        if (value.IsNullOrEmpty())
        {
            return false;
        }

        switch (value)
        {
            case Enum _ when (int)value < min:
            case string s when s.Length < min:
            case sbyte sb when sb < min:
            case short sh when sh < min:
            case int inte when inte < min:
            case long lo when lo < min:
            case byte by when by < min:
            case ushort us when us < min:
            case uint ui when ui < min:
            case ulong ul when Convert.ToInt64(ul) < min:
            case char ch when ch < min:
            case float fl when fl < min:
            case double d when d < min:
            case decimal de when de < min:
            case Array a when a.Length < min:
            case ICollection c when c.Count < min:
            case IEnumerable e when e.Cast<object>().Count() < min:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Verifica si un valor es menor que el valor mínimo permitido.
    /// </summary>
    /// <typeparam name="TValue">Tipo del valor a verificar.</typeparam>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="min">El valor mínimo.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{name} must be greater than {min}".</returns>
    public static TValue ThrowIfLess<TValue>([NotNull] this TValue? value, long min, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => value.IsLess(min), Util.Messages.Less(name, min));

    #endregion Less

    #region Equal

    /// <summary>
    /// Verifica si dos valores son iguales.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="valueCompare">El valor a comparar.</param>
    /// <returns>true | false.</returns>
    public static bool IsEqual([NotNullWhen(true)] this object? value, object? valueCompare)
    => Equals(value, valueCompare);

    /// <summary>
    /// Verifica si dos valores son iguales.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="valueCompare">El valor a comparar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <param name="nameCompare">Nombre del parámetro a comparar.</param>
    /// <returns>TValue | ApplicationException: "{name} and {nameCompare} not equals".</returns>
    public static TValue ThrowIfNotEqual<TValue>([NotNull] this TValue? value, TValue valueCompare, [CallerArgumentExpression("value")] string name = "", [CallerArgumentExpression("valueCompare")] string nameCompare = "")
    => value.ThrowIf(() => !value.IsEqual(valueCompare), Util.Messages.NotEquals(name, nameCompare));

    #endregion Equal

    #region Range

    /// <summary>
    /// Verifica si un valor se encuentra entre un rango.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="min">El valor mínimo.</param>
    /// <param name="max">El valor máximo.</param>
    /// <returns>true | false.</returns>
    public static bool IsRange([NotNullWhen(true)] this object? value, long min, long max)
    => !value.IsNullOrEmpty() && value.IsGreater(min) && value.IsLess(max);

    /// <summary>
    /// Verifica si un valor se encuentra entre un rango.
    /// </summary>
    /// <typeparam name="TValue">Tipo del valor a verificar.</typeparam>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="min">El valor mínimo.</param>
    /// <param name="max">El valor máximo.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{name} must be in the range {min} to {max}".</returns>
    public static TValue ThrowIfNotRange<TValue>([NotNull] this TValue? value, long min, long max, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsRange(min, max), Util.Messages.Range(name, min, max));

    #endregion Range

    #region Regex

    /// <summary>
    /// Verifica si un valor coincide con la expresión regular.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="regex">Expresión regular.</param>
    /// <returns>true | false.</returns>
    public static bool IsMatch([NotNullWhen(true)] this string? value, string regex)
    => !value.IsNullOrEmpty() && Regex.IsMatch(value, regex, options: RegexOptions.CultureInvariant);

    /// <summary>
    /// Verifica si un valor coincide con la expresión regular.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="regex">Expresión regular.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotMatch([NotNull] this string? value, string regex, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsMatch(regex), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Regex

    #region Name

    /// <summary>
    /// Verifica si un nombre tiene caracteres permitidos.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsName([NotNullWhen(true)] this string? value) => value.IsMatch(Regexp.ForName);

    /// <summary>
    /// Verifica si un nombre tiene caracteres permitidos.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotName([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsName(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Name

    #region Email

    /// <summary>
    /// Verifica si un email tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsEmail([NotNullWhen(true)] this string? value) => value.IsMatch(Regexp.ForEmail);

    /// <summary>
    /// Verifica si un email tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotEmail([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsEmail(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Email

    #region Directory

    /// <summary>
    /// Verifica si el path directorio existe.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsDirectory([NotNullWhen(true)] this string? value) => Directory.Exists(value);

    /// <summary>
    /// Verifica si el path directorio existe.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre de la variable o parámetro.</param>
    /// <returns>string | ApplicationException.</returns>
    public static string? ThrowIfDirectoryNotExists([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsDirectory(), Util.Messages.NotFound(value.IsNullOrEmpty() ? name : value));

    #endregion Directory

    #region File

    /// <summary>
    /// Verifica si el path de la fila existe.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsFile([NotNullWhen(true)] this string? value) => File.Exists(value);

    /// <summary>
    /// Verifica si el path de la fila existe.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>string | ApplicationException: "{valueOrName} is not found".</returns>
    public static string ThrowIfFileNotExists([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsFile(), Util.Messages.NotFound(value.IsNullOrEmpty() ? name : value));

    #endregion File

    #region FileExtension

    /// <summary>
    /// Verifica si la extensión de la fila tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsFileExtension([NotNullWhen(true)] this string? value) => Path.GetExtension(value)?.Any() == true;

    /// <summary>
    /// Verifica si la extensión de la fila tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>string | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotFileExtension([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsFileExtension(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion FileExtension

    #region Subdomain

    /// <summary>
    /// Verifica si el subdominio tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsSubdomain([NotNullWhen(true)] this string? value) => value.IsMatch(Regexp.ForSubdomain);

    /// <summary>
    /// Verifica si el subdominio tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotSubdomain([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsSubdomain(), Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Subdomain

    #region Domain

    /// <summary>
    /// Verifica si el dominio tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsDomain([NotNullWhen(true)] this string? value) => value.IsMatch(Regexp.ForDomain);

    /// <summary>
    /// Verifica si el dominio tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotDomain([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsDomain(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Domain

    #region Hostname

    /// <summary>
    /// Verifica si el hostname tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsHostname([NotNullWhen(true)] this string? value) => value.IsMatch(Regexp.ForHostname);

    /// <summary>
    /// Verifica si el hostname tiene un formato correcto.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotHostname([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsHostname(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Hostname

    #region Digit

    /// <summary>
    /// Verifica si un valor contiene algún digito decimal.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsContainsDigit([NotNullWhen(true)] this string? value) => value?.Any(char.IsDigit) == true;

    /// <summary>
    /// Verifica si un valor contiene algún digito decimal.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotContainsDigit([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsContainsDigit(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Digit

    #region UniqueChars

    /// <summary>
    /// Verifica si un valor contiene caracteres repetidos.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsContainsUniqueChars([NotNullWhen(true)] this string? value)
    {
        if (value is null)
        {
            return false;
        }

        var list = new HashSet<char>(value.Length);

        foreach (char val in value)
        {
            if (list.Contains(val))
            {
                return false;
            }

            list.Add(val);
        }

        return true;
    }

    /// <summary>
    /// Verifica si un valor contiene caracteres repetidos.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotContainsUniqueChars([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsContainsUniqueChars(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion UniqueChars

    #region EspecialChars

    /// <summary>
    /// Verifica si un valor contiene algún caracter especial.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsContainsEspecialChars([NotNullWhen(true)] this string? value) => value?.Any(x => !char.IsLetterOrDigit(x)) == true;

    /// <summary>
    /// Verifica si un valor contiene algún caracter especial.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotContainsEspecialChars([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsContainsEspecialChars(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion EspecialChars

    #region Lowercase

    /// <summary>
    /// Verifica si un valor contiene algún caracter en minúscula.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsLowercase([NotNullWhen(true)] this string? value) => value?.Any(char.IsLower) == true;

    /// <summary>
    /// Verifica si un valor contiene algún caracter en minúscula.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotLowercase([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsLowercase(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Lowercase

    #region Uppercase

    /// <summary>
    /// Verifica si un valor contiene algún caracter en mayúscula.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool IsUppercase([NotNullWhen(true)] this string? value) => value?.Any(char.IsUpper) == true;

    /// <summary>
    /// Verifica si un valor contiene algún caracter en mayúscula.
    /// </summary>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="name">Nombre del parámetro.</param>
    /// <returns>TValue | ApplicationException: "{valueOrName} has invalid format".</returns>
    public static string ThrowIfNotUppercase([NotNull] this string? value, [CallerArgumentExpression("value")] string name = "")
    => value.ThrowIf(() => !value.IsUppercase(), Util.Messages.InvalidFormat(value.IsNullOrEmpty() ? name : value));

    #endregion Uppercase

    /// <summary>
    /// Valida si una o varias condiciones son verdaderas.
    /// </summary>
    /// <typeparam name="TValue">Tipo del valor a verificar.</typeparam>
    /// <param name="value">El valor a verificar.</param>
    /// <param name="predicate">Función a evaluar.</param>
    /// <param name="message">Mensaje a mostrar en la ApplicationException.</param>
    /// <returns>TValue | ApplicationException: si value es nulo o si la evaluación del predicado es verdadero.</returns>
    public static TValue ThrowIf<TValue>([NotNull] this TValue? value, Func<bool> predicate, string message)
    => value is null || predicate is null || predicate() ? throw new ApplicationException(message) : value;

    /// <summary>
    /// Valida si una o varias condiciones son verdaderas.
    /// </summary>
    /// <param name="condition">Condición a evaluar.</param>
    /// <param name="message">Mensaje a mostrar en la ApplicationException.</param>
    public static void ThrowIf([DoesNotReturnIf(true)] bool condition, string message)
    {
        if (condition)
        {
            throw new ApplicationException(message);
        }
    }

    /// <summary>
    /// Si existen errores muestra los mensajes en una excepción de tipo ApplicationException.
    /// </summary>
    /// <param name="errors">Lista de mensajes de errores.</param>
    public static void ThrowIf(IEnumerable<string> errors)
    {
        if (errors?.Any() == true)
        {
            var error = errors.ToString(", ");

            ThrowIf(true, error);
        }
    }
}