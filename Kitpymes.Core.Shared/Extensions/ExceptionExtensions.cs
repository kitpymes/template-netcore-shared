// -----------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Diagnostics;
    using System.Text;

    /*
      Clase de extensión ExceptionExtensions
      Contiene las extensiones de las Excepciones
    */

    /// <summary>
    /// Clase de extensión <c>EnumExtensions</c>.
    /// Contiene las extensiones de los Enum.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las excepciones.</para>
    /// </remarks>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Obtiene el mensaje de la excepción.
        /// </summary>
        /// <param name="exception">Excepción que se obtiene el mensaje.</param>
        /// <returns>Mensaje de la excepción | ApplicationException: si el parámetro exception es nulo.</returns>
        public static string ToMessage(this Exception exception)
        {
            var validException = exception.ToIsNullOrEmptyThrow(nameof(exception));

            return validException.InnerException is null
                ? validException.Message
                : validException.Message + "\n\n --> " + validException.InnerException;
        }

        /// <summary>
        /// Obtiene un mensaje completo de la excepción.
        /// </summary>
        /// <param name="exception">Excepción que se obtiene el mensaje.</param>
        /// <returns>Mensaje completo de la excepción.</returns>
        public static string ToFullMessage(this Exception exception)
        {
            var validException = exception.ToIsNullOrEmptyThrow(nameof(exception));

            var sb = new StringBuilder();

            sb.Append($"| Error: {validException.ToMessage()} ");

            var stackFrame = new StackTrace(validException, true)?.GetFrame(0);

            var declaringType = stackFrame?.GetMethod()?.DeclaringType;

            if (!string.IsNullOrWhiteSpace(declaringType?.FullName))
            {
                sb.Append($"| File: {declaringType.FullName} ");
            }

            var fileLineNumber = stackFrame?.GetFileLineNumber();

            if (fileLineNumber.HasValue)
            {
                sb.Append($"| Line: {fileLineNumber.Value} ");
            }

            return sb.ToString();
        }
    }
}
