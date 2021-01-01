// -----------------------------------------------------------------------
// <copyright file="ResultMessage.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;

    /*
        Clase de resultados ResultMessage
        Contiene los métodos que devuelven los resultados con un mensaje
    */

    /// <summary>
    /// Clase de resultados <c>ResultMessage</c>.
    /// Contiene los métodos que devuelven los resultados con un mensaje.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos que devuelven resultados con un mensaje.</para>
    /// </remarks>
    public class ResultMessage : Result, IResultMessage
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ResultMessage"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecutó con éxito.</param>
        /// <param name="message">Mensaje.</param>
        /// <param name="details">Detalle.</param>
        public ResultMessage(bool success, string message, object? details = null)
            : base(success)
        {
            Message = message;
            Details = details;
        }

        /// <inheritdoc/>
        public string? Message { get; }

        /// <inheritdoc/>
        public object? Details { get; }

        /// <summary>
        /// Convierte el resultado en asincrono.
        /// </summary>
        /// <returns>Task{ResultError}.</returns>
        public Task<ResultMessage> ToAsync() => Task.FromResult(this);

        /// <inheritdoc/>
        public override string ToJson() => this.ToSerialize();
    }
}
