// -----------------------------------------------------------------------
// <copyright file="Exceptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System;

    /*
      Clase de utilidades
      Contiene las utilidades de las Exception
    */

    /// <summary>
    /// Clase de utilidades <c>Exceptions</c>.
    /// Contiene las utilidades de las Exception.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las utilidades necesarias para las Exception.</para>
    /// </remarks>
    public static class Exceptions
    {
        /// <summary>
        /// Lanza una excepción de tipo ApplicationException.
        /// </summary>
        /// <param name="message">Mensaje de la excepción.</param>
        /// <returns>ApplicationException.</returns>
        public static ApplicationException ToThrow(string message) => new ApplicationException(message);

        /// <summary>
        /// Lanza una excepción de tipo ApplicationException.
        /// </summary>
        /// <param name="exception">Excepción.</param>
        /// <returns>ApplicationException.</returns>
        public static ApplicationException ToThrow(Exception exception) => ToThrow(exception.ToFullMessage());
    }
}
