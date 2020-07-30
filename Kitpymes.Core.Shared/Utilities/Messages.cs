// -----------------------------------------------------------------------
// <copyright file="Messages.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    /*
      Clase de utilidades
      Contiene los mensajes
    */

    /// <summary>
    /// Clase de utilidades <c>Messages</c>.
    /// Contiene los mensajes.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las utilidades necesarias para los mensajes.</para>
    /// </remarks>
    public static class Messages
    {
        /// <summary>
        /// Devuelve un mensaje.
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>"{paramName} is null or empty".</returns>
        public static string NullOrEmpty(string paramName) => $"{paramName} is null or empty";

        /// <summary>
        /// Devuelve un mensaje.
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>"{paramName} is null or not values".</returns>
        public static string NullOrAny(string paramName) => $"{paramName} is null or not values";

        /// <summary>
        /// Devuelve un mensaje.
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>"{paramName} is not found".</returns>
        public static string NotFound(string paramName) => $"{paramName} is not found";
    }
}
