// -----------------------------------------------------------------------
// <copyright file="IResult.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System.Collections.Generic;

    /// <summary>
    /// Resultado.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Obtiene un valor que indica si el proceso fue correcto.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Obtiene un valor que indica el código del estado de la solicitud HTTP.
        /// </summary>
        int? StatusCode { get; }

        /// <summary>
        /// Obtiene un valor que indica el título del resultado.
        /// </summary>
        string? Title { get; }

        /// <summary>
        /// Obtiene un valor que indica el mensaje del resultado.
        /// </summary>
        string? Message { get; }

        /// <summary>
        /// Obtiene un detalle.
        /// </summary>
        object? Details { get; }

        /// <summary>
        /// Obtiene un valor que indica el id del error.
        /// </summary>
        string? TraceId { get; }

        /// <summary>
        /// Obtiene un valor que indica el tipo de excepción.
        /// </summary>
        string? ExceptionType { get; }

        /// <summary>
        /// Obtiene la lista de errores de un modelo.
        /// </summary>
        public IDictionary<string, IEnumerable<string>>? Errors { get; }

        /// <summary>
        /// Serializa un objeto.
        /// </summary>
        /// <returns>string.</returns>
        string ToJson();
    }
}
