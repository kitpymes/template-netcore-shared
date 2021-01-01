// -----------------------------------------------------------------------
// <copyright file="ResultError.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;

    /*
        Clase de resultados ResultError
        Contiene los métodos que devuelven los resultados con error
    */

    /// <summary>
    /// Clase de resultados <c>ResultError</c>.
    /// Contiene los métodos que devuelven los resultados con error.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos que devuelven resultados con error.</para>
    /// </remarks>
    public class ResultError : Result, IResultError
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ResultError"/>.
        /// </summary>
        /// <param name="errors">Lista de errores.</param>
        public ResultError(IDictionary<string, string> errors)
            : base(false)
        {
            if (errors?.Count > 0)
            {
                Errors ??= new Dictionary<string, string>();

                Errors = errors;
                Count = errors.Count;
            }
        }

        /// <inheritdoc/>
        public int? Count { get; }

        /// <inheritdoc/>
        public IDictionary<string, string>? Errors { get; }

        /// <summary>
        /// Convierte el resultado en asincrono.
        /// </summary>
        /// <returns>Task{ResultError}.</returns>
        public Task<ResultError> ToAsync() => Task.FromResult(this);

        /// <inheritdoc/>
        public override string ToJson() => this.ToSerialize();
    }
}
