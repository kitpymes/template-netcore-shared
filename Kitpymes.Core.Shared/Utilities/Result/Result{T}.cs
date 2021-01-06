// -----------------------------------------------------------------------
// <copyright file="Result{T}.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System;
    using System.Linq;
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
        protected Result(bool success, int statusCode, string title, string? message = null)
#pragma warning restore CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
            : base(success, statusCode, title, message) { }

        /// <inheritdoc/>
#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.
        public T Data { get; private set; }
#pragma warning restore CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="message">Agrega un mensaje al resultado.</param>
        /// <returns>Result{T}.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static new Result<T> Ok(string? message = null)
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        {
            return new Result<T>(true, DefaultStatusCodeOk.ToValue(), DefaultTitleOk, message);
        }

        /// <summary>
        /// Agrega uno o varios errores al resultado.
        /// </summary>
        /// <param name="data">Datos del resultado.</param>
        /// <param name="message">Agrega un mensaje al resultado.</param>
        /// <returns>Result{T}.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static Result<T> Ok(T data, string? message = null)
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        {
            return new Result<T>(true, DefaultStatusCodeOk.ToValue(), DefaultTitleOk, message)
            {
                Data = data,
            };
        }

        /// <summary>
        /// Devuelve un resultado de error con los campos obligatorios.
        /// </summary>
        /// <param name="message">Mensaje del resultado.</param>
        /// <param name="details">Detalle del resultado.</param>
        /// <returns>Result.</returns>
#pragma warning disable CA1000 // No declarar miembros estáticos en tipos genéricos
        public static new Result<T> Error(string? message = null, object? details = null)
#pragma warning restore CA1000 // No declarar miembros estáticos en tipos genéricos
        {
            return new Result<T>(false, DefaultStatusCodeError.ToValue(), DefaultTitleError, message)
            {
                Details = details,
            };
        }

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

            return new Result<T>(false, config.StatusCode ?? DefaultStatusCodeError.ToValue(), config.Title ?? DefaultTitleError, config.Message)
            {
                TraceId = Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.CurrentCulture),
                ExceptionType = config.Exception,
                Details = config.Details,
                Message = config.Messages?.Count > 0 ? string.Join(", ", config.Messages) : null,
                Errors = config.ModelErrors,
            };
        }

        /// <inheritdoc/>
        public override string ToJson() => this.ToSerialize();
    }
}
