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
        /// <param name="statusCode">Código del estado de la solicitud HTTP.</param>
        /// <param name="title">Título del resultado.</param>
        /// <param name="message">Mensaje del resultado.</param>
        protected Result(bool success, HttpStatusCode statusCode, string title, string message)
            : this(success)
        {
             StatusCode = statusCode.ToValue();
             Title = title;
             Message = message;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecuto correctamente.</param>
        protected Result(bool success) => Success = success;

        /// <inheritdoc/>
        public bool Success { get; protected set; }

        /// <inheritdoc/>
        public int? StatusCode { get; protected set; }

        /// <inheritdoc/>
        public string? Title { get; protected set; }

        /// <inheritdoc/>
        public string? TraceId { get; protected set; }

        /// <inheritdoc/>
        public string? ExceptionType { get; protected set; }

        /// <inheritdoc/>
        public string? Message { get; set; }

        /// <inheritdoc/>
        public object? Details { get; set; }

        /// <inheritdoc/>
#pragma warning disable CA2227 // Las propiedades de colección deben ser de solo lectura
        public IDictionary<string, IList<string>>? Errors { get; protected set; }
#pragma warning restore CA2227 // Las propiedades de colección deben ser de solo lectura

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <returns>Result.</returns>
        public static Result Ok()
        => new Result(true, HttpStatusCode.OK, Resources.MsgProcessRanSuccessfully, Resources.MsgProcessRanSuccessfully);

        /// <summary>
        /// Devuelve un resultado de error cuando ocurre un error de autorización.
        /// <para><strong>Unauthorized:</strong> 401.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static Result Unauthorized()
        => new Result(false, HttpStatusCode.Unauthorized, Resources.MsgErrorsTitle, Resources.MsgUnauthorizedAccess);

        /// <summary>
        /// Devuelve un resultado de error cuando ocurre un error no controlado.
        /// <para><strong>InternalServerError:</strong> 500.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static Result InternalServerError()
        => new Result(false, HttpStatusCode.InternalServerError, Resources.MsgErrorsTitle, Resources.MsgFriendlyUnexpectedError);

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="errors">Agrega los errores de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(IDictionary<string, IList<string>> errors)
        => new Result(false, HttpStatusCode.BadRequest, Resources.MsgErrorsTitle, Resources.MsgErrorsTitle)
        {
            Errors = errors,
        };

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="errors">Agrega los errores de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(IList<(string fieldName, string message)> errors)
        {
            var result = new Result(false, HttpStatusCode.BadRequest, Resources.MsgErrorsTitle, Resources.MsgErrorsTitle);

            if (errors != null)
            {
                result.Errors ??= new Dictionary<string, IList<string>>();

                foreach (var (fieldName, message) in errors)
                {
                    if (!string.IsNullOrWhiteSpace(fieldName) && !string.IsNullOrWhiteSpace(message))
                    {
                        if (!result.Errors.ContainsKey(fieldName))
                        {
                            result.Errors.Add(fieldName, new List<string> { message });
                        }
                        else
                        {
                            result.Errors[fieldName].Add(message);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="messages">Agrega mensajes de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(IList<string> messages)
        => new Result(false, HttpStatusCode.BadRequest, Resources.MsgErrorsTitle, messages.ToString(", "));

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="message">Agrega un mensaje de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static Result BadRequest(string message)
        => new Result(false, HttpStatusCode.BadRequest, Resources.MsgErrorsTitle, message);

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
                StatusCode = config.StatusCode,
                Title = config.Title,
                TraceId = Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.CurrentCulture),
                Details = config.Details,
                ExceptionType = config.Exception,
                Message = config.Messages?.Count > 0 ? config.Messages.ToString(", ") : null,
                Errors = config.Errors,
            };
        }

        /// <inheritdoc/>
        public virtual string ToJson() => this.ToSerialize(x => x.IgnoreNullValues = true);
    }
}
