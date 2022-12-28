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
        /// Inicializa una nueva instancia de la clase <see cref="Result"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecuto correctamente.</param>
        /// <param name="status">Código del estado de la solicitud HTTP.</param>
        /// <param name="title">Título del resultado.</param>
        /// <param name="message">Mensaje del resultado.</param>
        protected Result(bool success, HttpStatusCode status, string title, string message)
            : this(success)
        {
             Status = status.ToValue();
             Title = title;
             Message = message;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecuto correctamente.</param>
        protected Result(bool success) => Success = success;

        /// <inheritdoc/>
        public string? TraceId { get; protected set; }

        /// <inheritdoc/>
        public bool Success { get; protected set; }

        /// <inheritdoc/>
        public int? Status { get; protected set; }

        /// <inheritdoc/>
        public string? Title { get; set; }

        /// <inheritdoc/>
        public string? Exception { get; set; }

        /// <inheritdoc/>
        public string? Message { get; set; }

        /// <inheritdoc/>
        public object? Details { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, IEnumerable<string>>? Errors { get; protected set; }

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// <para><strong>OK:</strong> 200.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static Result Ok()
        => new (true, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), Resources.MsgProcessRanSuccessfully);

        /// <summary>
        /// Devuelve un resultado de error cuando ocurre un error de autorización.
        /// <para><strong>Unauthorized:</strong> 401.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static Result Unauthorized()
        => new (false, HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), Resources.MsgUnauthorizedAccess);

        /// <summary>
        /// Devuelve un resultado de error cuando ocurre un error no controlado.
        /// <para><strong>InternalServerError:</strong> 500.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static Result InternalServerError()
        => new (false, HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), Resources.MsgFriendlyUnexpectedError);

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="errors">Agrega los errores de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(IDictionary<string, IEnumerable<string>> errors)
        {
            errors.ThrowIfNullOrAny();

            return new (false, HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), Resources.MsgValidationsError)
            {
                Errors = errors,
            };
        }

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="errors">Agrega los errores de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(IEnumerable<(string fieldName, string message)> errors)
        {
            errors.ThrowIfNullOrAny();

            var result = new Result(false, HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), Resources.MsgValidationsError);

            result.Errors ??= new Dictionary<string, IEnumerable<string>>();

            foreach (var (fieldName, message) in errors)
            {
                result.Errors.AddOrUpdate(fieldName, message);
            }

            return result;
        }

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="messages">Agrega mensajes de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(IEnumerable<string> messages)
        => new (false, HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), messages.ThrowIfNullOrAny().ToString(", "));

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="message">Agrega un mensaje de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(string message)
        => new (false, HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message.ThrowIfNullOrEmpty());

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="options">Configuración de la lista de errores.</param>
        /// <returns>Result.</returns>
        public static Result Error(Action<ErrorOptions> options)
        {
            var config = options.ToConfigureOrDefault().ErrorSettings;

            return new Result(false)
            {
                Status = config.Status,
                Title = config.Title,
                TraceId = Guid.NewGuid().ToSecuencial().ToString(),
                Details = config.Details,
                Exception = config.Exception,
                Message = config.Messages?.Count > 0 ? config.Messages.ToString(", ") : null,
                Errors = config.Errors,
            };
        }

        /// <inheritdoc/>
        public virtual string ToJson()
        => this.ToSerialize(x => x.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);
    }
}
