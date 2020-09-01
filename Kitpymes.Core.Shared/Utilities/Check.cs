// -----------------------------------------------------------------------
// <copyright file="Check.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System;
    using System.Collections;
    using System.Linq;

    /*
        Configuración de validaciones
        Contiene las validaciones mas comunes
    */

    /// <summary>
    /// Configuración de validaciones <c>Check</c>.
    /// Contiene las validaciones mas comunes.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las validaciones mas utilizadas.</para>
    /// </remarks>
    public static class Check
    {
        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsNullOrEmpty(params object?[] values)
        {
            var errorsIsNullOrEmpty = values.Where(value => value is null || (value is string && string.IsNullOrWhiteSpace(value.ToString())));

            return (errorsIsNullOrEmpty.Any(), errorsIsNullOrEmpty.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsNullOrAny(params IEnumerable?[] values)
        {
            var errorsIsAny = values.Where(value =>
            {
                if (IsNullOrEmpty(value).HasErrors)
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
                        return !value.Cast<object>().Any();
                }
            });

            return (errorsIsAny.Any(), errorsIsAny.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="max">Valor máximo permitido.</param>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsGreater(long max, params object?[] values)
        {
            var errorsIsGreater = values.Where(value =>
            {
                if (IsNullOrEmpty(value).HasErrors)
                {
                    return true;
                }

                switch (value)
                {
                    case Enum en when (int)value > max:
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
            });

            return (errorsIsGreater.Any(), errorsIsGreater.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="min">Valor mínimo permitido.</param>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsLess(long min, params object?[] values)
        {
            var errorsIsLess = values.Where(value =>
            {
                if (IsNullOrEmpty(value).HasErrors)
                {
                    return true;
                }

                switch (value)
                {
                    case Enum en when (int)value < min:
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
            });

            return (errorsIsLess.Any(), errorsIsLess.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="value">Valor a validar.</param>
        /// <param name="valuesCompare">Valores a comparar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsEqual(object? value, params object?[] valuesCompare)
        {
            var errorsIsEqual = valuesCompare.Where(valueCompare => IsNullOrEmpty(valueCompare).HasErrors || !Equals(value, valueCompare));

            return (errorsIsEqual.Any(), errorsIsEqual.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="min">Valor mínimo permitido.</param>
        /// <param name="max">Valor máximo permitido.</param>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsRange(long min, long max, params object?[] values)
        {
            var errorsIsRange = values.Where(value =>
                IsNullOrEmpty(value).HasErrors ||
                IsLess(min, value).HasErrors ||
                IsGreater(max, value).HasErrors);

            return (errorsIsRange.Any(), errorsIsRange.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="regex">Expresión regular a validar.</param>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsRegexMatch(string regex, params string?[] values)
        {
            var errorsIsRegex = values.Where(value => IsNullOrEmpty(value).HasErrors || !System.Text.RegularExpressions.Regex.IsMatch(value, regex));

            return (errorsIsRegex.Any(), errorsIsRegex.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsCustom(params Func<bool>[] values)
        {
            var errorsIsCustom = values
                .Select(s => s.Invoke())
                .Where(w => w);

            return (errorsIsCustom.Any(), errorsIsCustom.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsName(params string?[] values)
        {
            var errorsIsName = values.Where(value => IsNullOrEmpty(value).HasErrors || IsRegexMatch(Regexp.ForName, value).HasErrors);

            return (errorsIsName.Any(), errorsIsName.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsEmail(params string?[] values)
        {
            var errorsIsEmail = values.Where(value =>
            {
                if (IsNullOrEmpty(value).HasErrors || IsRegexMatch(Regexp.ForEmail, value).HasErrors)
                {
                    return true;
                }

                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(value);

                    return mailAddress?.Address != value;
                }
                catch (FormatException)
                {
                    return true;
                }
            });

            return (errorsIsEmail.Any(), errorsIsEmail.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsDirectory(params string?[] values)
        {
            var errorsIsDirectory = values.Where(value => IsNullOrEmpty(value).HasErrors || !System.IO.Directory.Exists(value));

            return (errorsIsDirectory.Any(), errorsIsDirectory.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsFile(params string?[] values)
        {
            var errorsIsFile = values.Where(value => IsNullOrEmpty(value).HasErrors || !System.IO.File.Exists(value));

            return (errorsIsFile.Any(), errorsIsFile.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsFileExtension(params string?[] values)
        {
            var errorsIsExtension = values.Where(value => IsNullOrEmpty(System.IO.Path.GetExtension(value)).HasErrors);

            return (errorsIsExtension.Any(), errorsIsExtension.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsSubdomain(params string?[] values)
        {
            var errorsIsSubdomain = values.Where(value => IsNullOrEmpty(value).HasErrors || IsRegexMatch(Regexp.ForSubdomain, value).HasErrors);

            return (errorsIsSubdomain.Any(), errorsIsSubdomain.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsDomain(params string?[] values)
        {
            var errorsIsSubdomain = values.Where(value => IsNullOrEmpty(value).HasErrors || IsRegexMatch(Regexp.ForDomain, value).HasErrors);

            return (errorsIsSubdomain.Any(), errorsIsSubdomain.Count());
        }

        /// <summary>
        /// Comprueba si los valores ingresados son validos.
        /// </summary>
        /// <param name="values">Valores a validar.</param>
        /// <returns>(bool HasErrors, int Count).</returns>
        public static (bool HasErrors, int Count) IsHostname(params string?[] values)
        {
            var errorsIsSubdomain = values.Where(value => IsNullOrEmpty(value).HasErrors || IsRegexMatch(Regexp.ForHostname, value).HasErrors);

            return (errorsIsSubdomain.Any(), errorsIsSubdomain.Count());
        }

        /// <summary>
        /// Lanza una ApplicationException.
        /// </summary>
        /// <param name="message">Mensaje.</param>
        public static void Throw(string message) => throw new ApplicationException(message);
    }
}