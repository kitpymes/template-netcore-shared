// -----------------------------------------------------------------------
// <copyright file="ErrorSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System.Collections.Generic;

    /*
      Clase de configuración del error a devolver ErrorSettings
      Contiene loa propiedades que se devuelven cuando hay un error
    */

    /// <summary>
    /// Clase de configuración del error a devolver <c>ErrorSettings</c>.
    /// Contiene loa propiedades que se devuelven cuando hay un error.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades que se devuelven cuando hay un error.</para>
    /// </remarks>
    public class ErrorSettings
    {
        /// <summary>
        /// Obtiene o establece un valor que indica el número del estado de la solicitud HTTP.
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que indica el título del resultado.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que indica el id del error.
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que indica el tipo de excepción.
        /// </summary>
        public string? Exception { get; set; }

        /// <summary>
        /// Obtiene o establece un detalle.
        /// </summary>
        public object? Details { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de errores.
        /// </summary>
        public IList<string>? Messages { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de errores de un modelo.
        /// </summary>
        public IDictionary<string, IEnumerable<string>>? Errors { get; set; }
    }
}
