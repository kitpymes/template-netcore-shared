// -----------------------------------------------------------------------
// <copyright file="Result.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System;
    using System.Collections.Generic;
    using Kitpymes.Core.Shared;

    /*
       Clase de resultados Result
       Contiene los métodos que devuelven los resultados
    */

    /// <summary>
    /// Clase de resultados <c>Result</c>.
    /// Contiene los métodos que devuelven los resultados.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos que devuelven resultados.</para>
    /// </remarks>
    public class Result : IResult
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result"/>.
        /// </summary>
        /// <param name="success">Si el proceso se resolvio de forma correcta o incorrecta.</param>
        public Result(bool success) => Success = success;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result"/>.
        /// </summary>
        protected Result() { }

        /// <inheritdoc/>
        public bool Success { get; }

        #region Ok

        /// <summary>
        /// Indica que el proceso se ejecutó con éxito.
        /// </summary>
        /// <returns>IResult.</returns>
        public static IResult Ok() => new Result(true);

        /// <summary>
        /// Indica que el proceso se ejecutó con éxito.
        /// </summary>
        /// <param name="message">Mensaje.</param>
        /// <returns>IResultMessage.</returns>
        public static IResultMessage Ok(string message) => new ResultMessage(true, message);

        /// <summary>
        /// Indica que el proceso se ejecutó con éxito.
        /// </summary>
        /// <param name="data">Objeto con datos.</param>
        /// <returns>IResultData{T}.</returns>
        public static IResultData<T> Ok<T>(T data)
            where T : class
        => new ResultData<T>(true, data);

        #endregion Ok

        #region Error

        /// <summary>
        /// Indica que el proceso no se ejecutó con éxito.
        /// </summary>
        /// <returns>IResult.</returns>
        public static IResult Error() => new Result(false);

        /// <summary>
        /// Indica que el proceso no se ejecutó con éxito.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="details">Detalles del error.</param>
        /// <returns>IResultMessage.</returns>
        public static IResultMessage Error(string message, object? details = null)
        => new ResultMessage(false, message, details);

        /// <summary>
        /// Indica que el proceso no se ejecutó con éxito.
        /// </summary>
        /// <param name="exception">Excepción de error.</param>
        /// <param name="details">Detalles del error.</param>
        /// <returns>IResultMessage.</returns>
        public static IResultMessage Error(Exception exception, object? details = null)
        => new ResultMessage(false, exception.ToFullMessage(), details);

        /// <summary>
        /// Indica que el proceso no se ejecutó con éxito.
        /// </summary>
        /// <param name="messages">Lista de errores.</param>
        /// <param name="details">Detalles del error.</param>
        /// <returns>IResultError.</returns>
        public static IResultError Error(IDictionary<string, string> messages, object? details = null)
        => new ResultError(messages, details);

        #endregion Error

        /// <inheritdoc/>
        public virtual string ToJson() => this.ToSerialize();
    }
}
