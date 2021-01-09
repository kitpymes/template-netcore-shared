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
    using System.Net;
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
        /// Título por defecto cuando se devuelve uno o varios errores.
        /// </summary>
        public const string DefaultTitleOk = "The process ran successfully.";

        /// <summary>
        /// Título por defecto cuando el proceso se realizo con exito.
        /// </summary>
        public const string DefaultTitleError = "One or more validations errors ocurred.";

        /// <summary>
        /// Código de estado HTTP por defecto cuando el proceso se ejecuto sin errores.
        /// </summary>
        public const HttpStatusCode DefaultStatusCodeOk = HttpStatusCode.OK;

        /// <summary>
        /// Código de estado HTTP por defecto cuando el proceso se ejecuto con errores.
        /// </summary>
        public const HttpStatusCode DefaultStatusCodeError = HttpStatusCode.BadRequest;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecuto correctamente.</param>
        /// <param name="statusCode">Código del estado de la solicitud HTTP.</param>
        /// <param name="title">Título del resultado.</param>
        /// <param name="message">Mensaje del resultado.</param>
        protected Result(bool success, int statusCode, string title, string? message = null)
        {
             Success = success;
             StatusCode = statusCode;
             Title = title;
             Message = message;
        }

        /// <inheritdoc/>
        public bool Success { get; protected set; }

        /// <inheritdoc/>
        public int? StatusCode { get; protected set; }

        /// <inheritdoc/>
        public string? Title { get; protected set; }

        /// <inheritdoc/>
        public string? Message { get; protected set; }

        /// <inheritdoc/>
        public string? TraceId { get; protected set; }

        /// <inheritdoc/>
        public string? ExceptionType { get; protected set; }

        /// <inheritdoc/>
        public object? Details { get; protected set; }

        /// <inheritdoc/>
#pragma warning disable CA2227 // Las propiedades de colección deben ser de solo lectura
        public IDictionary<string, IEnumerable<string>>? Errors { get; protected set; }
#pragma warning restore CA2227 // Las propiedades de colección deben ser de solo lectura

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="message">Agrega un mensaje al resultado.</param>
        /// <returns>Result.</returns>
        public static Result Ok(string? message = null)
        {
            return new Result(true, DefaultStatusCodeOk.ToValue(), DefaultTitleOk, message);
        }

        /// <summary>
        /// Devuelve un resultado de error con los campos obligatorios.
        /// </summary>
        /// <param name="message">Mensaje del resultado.</param>
        /// <param name="details">Detalle del resultado.</param>
        /// <returns>Result.</returns>
        public static Result Error(string? message = null, object? details = null)
        {
            return new Result(false, DefaultStatusCodeError.ToValue(), DefaultTitleError, message)
            {
                Details = details,
            };
        }

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="options">Configuración de la lista de errores.</param>
        /// <returns>Result.</returns>
        public static Result Error(Action<ErrorOptions> options)
        {
            var config = options.ToConfigureOrDefault().ErrorSettings;

            return new Result(false, config.StatusCode ?? DefaultStatusCodeError.ToValue(), config.Title ?? DefaultTitleError, config.Message)
            {
                TraceId = Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.CurrentCulture),
                Details = config.Details,
                ExceptionType = config.Exception,
                Message = config.Messages?.Count > 0 ? config.Messages.ToString(", ") : null,
                Errors = config.ModelErrors,
            };
        }

        /// <inheritdoc/>
        public virtual string ToJson() => this.ToSerialize(x => x.IgnoreNullValues = true);
    }
}
