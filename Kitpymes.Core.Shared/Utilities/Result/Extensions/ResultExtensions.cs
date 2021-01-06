// -----------------------------------------------------------------------
// <copyright file="ResultExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System.Collections.Generic;

    /*
       Clase de extensión ResultExtensions
       Contiene las extensiones para los resultados
   */

    /// <summary>
    /// Clase de extensión <c>ResultExtensions</c>.
    /// Contiene las extensiones para los resultados.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para los resultados.</para>
    /// </remarks>
    public static class ResultExtensions
    {
        /// <summary>
        /// Obtiene un error.
        /// </summary>
        /// <param name="errors">Lista de errores.</param>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>(bool Exists, string? Message).</returns>
        public static bool Contains(this IDictionary<string, IList<string>>? errors, string fieldName, string message)
        {
            return errors != null && errors[fieldName] != null && errors[fieldName].Contains(message);
        }
    }
}
