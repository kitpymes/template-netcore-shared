// -----------------------------------------------------------------------
// <copyright file="Result{T}.cs" company="Kitpymes">
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
       Clase de resultados Result{T}
       Contiene los métodos que devuelven los resultados
   */

    /// <summary>
    /// Clase de resultados <c>Result{T}</c>.
    /// Contiene los métodos que devuelven los resultados.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todos los métodos que devuelven resultados.</para>
    /// </remarks>
    public class Result<T> : Result, IResult<T>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecuto correctamente.</param>
        /// <param name="status">Código del estado de la solicitud HTTP.</param>
        /// <param name="title">Título del resultado.</param>
        /// <param name="message">Mensaje del resultado.</param>
#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
        protected Result(bool success, HttpStatusCode status, string title, string message)
#pragma warning restore CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
            : base(success, status, title, message) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecuto correctamente.</param>
#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
        private Result(bool success)
            : base(success) { }
#pragma warning restore CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

        /// <inheritdoc/>
        public T Data { get; private set; }

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <returns>Result{T}.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static new Result<T> Ok()
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        => new (true, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), Resources.MsgProcessRanSuccessfully);

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="data">Datos del resultado.</param>
        /// <returns>Result{T}.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static Result<T> Ok(T data)
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        {
            return new Result<T>(true, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), Resources.MsgProcessRanSuccessfully)
            {
                Data = data,
            };
        }

        /// <summary>
        /// Devuelve un resultado de error cuando ocurre un error de autorización.
        /// <para><strong>Unauthorized:</strong> 401.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static new Result<T> Unauthorized()
        => new (false, HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), Resources.MsgUnauthorizedAccess);

        /// <summary>
        /// Devuelve un resultado de error cuando ocurre un error no controlado.
        /// <para><strong>InternalServerError:</strong> 500.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static new Result<T> InternalServerError()
        => new (false, HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), Resources.MsgFriendlyUnexpectedError);

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="errors">Agrega los errores de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static new Result<T> BadRequest(IDictionary<string, IEnumerable<string>> errors)
        {
            errors.ThrowIfNullOrAny(nameof(errors));

            return new Result<T>(false, HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), Resources.MsgValidationsError)
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
        public static new Result<T> BadRequest(IEnumerable<(string fieldName, string message)> errors)
        {
            errors.ThrowIfNullOrAny(nameof(errors));

            var result = new Result<T>(false, HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), Resources.MsgValidationsError);

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
        public static new Result<T> BadRequest(IEnumerable<string> messages)
        => new (false, HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), messages.ThrowIfNullOrAny().ToString(", "));

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="options">Configuración de la lista de errores.</param>
        /// <returns>Result{T}.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static new Result<T> Error(Action<ErrorOptions> options)
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        {
            var config = options.ToConfigureOrDefault().ErrorSettings;

            VerifyExtensions.ThrowIf(config.Messages.IsNullOrAny() && config.Errors.IsNullOrAny(), $"{nameof(config.Messages)} and {nameof(config.Errors)} are null or not contains elements.");

            return new Result<T>(false)
            {
                Status = config.Status,
                Title = config.Title,
                TraceId = Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.CurrentCulture),
                Details = config.Details,
                Exception = config.Exception,
                Message = config.Messages?.Count > 0 ? config.Messages.ToString(", ") : null,
                Errors = config.Errors,
            };
        }

        /// <inheritdoc/>
        public override string ToJson() => this.ToSerialize(x => x.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);
    }
}
