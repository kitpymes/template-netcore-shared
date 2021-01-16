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
    using System.Linq;
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
        /// <param name="statusCode">Código del estado de la solicitud HTTP.</param>
        /// <param name="title">Título del resultado.</param>
        /// <param name="message">Mensaje del resultado.</param>
#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
        protected Result(bool success, HttpStatusCode statusCode, string title, string message)
#pragma warning restore CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
            : base(success, statusCode, title, message) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="success">Si el proceso se ejecuto correctamente.</param>
#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
        private Result(bool success)
            : base(success) { }
#pragma warning restore CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

        /// <inheritdoc/>
#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
        public T Data { get; private set; }
#pragma warning restore CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <returns>Result{T}.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static new Result<T> Ok()
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        => new Result<T>(true, HttpStatusCode.OK, Resources.MsgProcessRanSuccessfully, Resources.MsgProcessRanSuccessfully);

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="data">Datos del resultado.</param>
        /// <returns>Result{T}.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static Result<T> Ok(T data)
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        {
            return new Result<T>(true, HttpStatusCode.OK, Resources.MsgProcessRanSuccessfully, Resources.MsgProcessRanSuccessfully)
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
        => new Result<T>(false, HttpStatusCode.Unauthorized, Resources.MsgErrorsTitle, Resources.MsgUnauthorizedAccess);

        /// <summary>
        /// Devuelve un resultado de error cuando ocurre un error no controlado.
        /// <para><strong>InternalServerError:</strong> 500.</para>
        /// </summary>
        /// <returns>Result.</returns>
        public static new Result<T> InternalServerError()
        => new Result<T>(false, HttpStatusCode.InternalServerError, Resources.MsgErrorsTitle, Resources.MsgFriendlyUnexpectedError);

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="errors">Agrega los errores de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static new Result<T> BadRequest(IDictionary<string, IList<string>> errors)
        => new Result<T>(false, HttpStatusCode.BadRequest, Resources.MsgErrorsTitle, Resources.MsgErrorsTitle)
        {
            Errors = errors,
        };

        /// <summary>
        /// Devuelve un resultado de error de validación.
        /// <para><strong>BadRequest:</strong> 400.</para>
        /// </summary>
        /// <param name="errors">Agrega los errores de validación al resultado.</param>
        /// <returns>Result.</returns>
        public static new Result<T> BadRequest(IList<(string fieldName, string message)> errors)
        {
            var result = new Result<T>(false, HttpStatusCode.BadRequest, Resources.MsgErrorsTitle, Resources.MsgErrorsTitle);

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
        public static new Result<T> BadRequest(IList<string> messages)
        => new Result<T>(false, HttpStatusCode.BadRequest, Resources.MsgErrorsTitle, messages.ToString(", "));

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

            return new Result<T>(false)
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
        public override string ToJson() => this.ToSerialize(x => x.IgnoreNullValues = true);
    }
}
