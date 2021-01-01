// -----------------------------------------------------------------------
// <copyright file="ResultData.cs" company="Kitpymes">
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
      Clase de resultados ResultData{T}
      Contiene los métodos que devuelven los resultados
   */

    /// <summary>
    /// Clase de resultados <c>ResultData{T}</c>.
    /// Contiene los métodos que devuelven los resultados.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos que devuelven resultados.</para>
    /// </remarks>
    public class ResultData<T> : Result, IResultData<T>
        where T : class
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ResultData{T}"/>.
        /// </summary>
        /// <param name="success">Indica si el proceso fue correcto.</param>
        /// <param name="data">Objeto con datos.</param>
        /// <param name="errors">Lista de errors.</param>
        public ResultData(
            bool success,
            T? data,
            IEnumerable<string>? errors = null)
            : base(success)
        {
            Data = data;

            if (errors != null && errors.Any())
            {
                Errors ??= new List<string>();

                Errors = errors;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string>? Errors { get; }

        /// <inheritdoc/>
        public T? Data { get; }

        /// <summary>
        /// Convierte el resultado en asincrono.
        /// </summary>
        /// <returns>Task{ResultData{T}}.</returns>
        public Task<ResultData<T>> ToAsync() => Task.FromResult(this);

        /// <inheritdoc/>
        public override string ToJson() => this.ToSerialize();
    }
}
