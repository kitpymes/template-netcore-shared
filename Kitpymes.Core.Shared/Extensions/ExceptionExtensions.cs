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
      Clase de extensi�n ExceptionExtensions
      Contiene las extensiones de las Excepciones
    */

    /// <summary>
    /// Clase de extensi�n <c>EnumExtensions</c>.
    /// Contiene las extensiones de los Enum.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las excepciones.</para>
    /// </remarks>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Obtiene el mensaje de la excepci�n.
        /// </summary>
        /// <param name="exception">Excepci�n que se obtiene el mensaje.</param>
        /// <returns>Mensaje de la excepci�n | ApplicationException: si el par�metro exception es nulo.</returns>
        public static string ToMessage(this Exception exception)
        {
            var validException = exception.ToIsNullOrEmptyThrow(nameof(exception));

            return validException.InnerException is null
                ? validException.Message
                : validException.Message + "\n\n --> " + validException.InnerException;
        }

        /// <summary>
        /// Obtiene un mensaje completo de la excepci�n.
        /// </summary>
        /// <param name="exception">Excepci�n que se obtiene el mensaje.</param>
        /// <returns>Mensaje completo de la excepci�n.</returns>
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
