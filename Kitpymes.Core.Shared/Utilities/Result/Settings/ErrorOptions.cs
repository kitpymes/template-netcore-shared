// -----------------------------------------------------------------------
// <copyright file="ErrorOptions.cs" company="Kitpymes">
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

    /*
      Clase de configuración del error a devolver ErrorOptions
      Contiene loa propiedades que se devuelven cuando hay un error
    */

    /// <summary>
    /// Clase de configuración del error a devolver <c>ErrorOptions</c>.
    /// Contiene loa propiedades que se devuelven cuando hay un error.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las propiedades que se devuelven cuando hay un error.</para>
    /// </remarks>
    public class ErrorOptions
    {
        /// <summary>
        /// Nombre de la excepción por defecto.
        /// </summary>
        public const string DefaultException = nameof(ApplicationException);

        /// <summary>
        /// Obtiene la configuración de los  errores.
        /// </summary>
        public ErrorSettings ErrorSettings { get; private set; } = new ErrorSettings();

        /// <summary>
        /// Agrega un título al resultado.
        /// </summary>
        /// <param name="title">Título de error.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithTitle(string title)
        {
            ErrorSettings.Title = title;

            return this;
        }

        /// <summary>
        /// Agrega el código de estado HTTP al resultado.
        /// </summary>
        /// <param name="status">Código de estado HTTP del error.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithStatusCode(HttpStatusCode status)
        {
            ErrorSettings.Status = status.ToValue();

            return this;
        }

        /// <summary>
        /// Agrega un detalle al resultado.
        /// </summary>
        /// <param name="details">Detalle del resultado.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithDetails(object details)
        {
            ErrorSettings.Details = details;

            return this;
        }

        /// <summary>
        /// Agrega el nombre de la excepción al resultado.
        /// </summary>
        /// <param name="exception">Nombre de la excepción.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithExceptionType(string exception = DefaultException)
        {
            ErrorSettings.Exception = exception;

            return this;
        }

        #region Messages

        /// <summary>
        /// Agrega un mensaje al resultado.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithMessages(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                ErrorSettings.Messages ??= new List<string>();

                if (!ErrorSettings.Messages.Contains(message))
                {
                    ErrorSettings.Messages.Add(message);
                }
            }

            return this;
        }

        /// <summary>
        /// Agrega uno o varios mensajes al resultado.
        /// </summary>
        /// <param name="messages">Lista de mensajes de error.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithMessages(IList<string> messages)
        {
            if (messages?.Count > 0)
            {
                foreach (var message in messages)
                {
                    WithMessages(message);
                }
            }

            return this;
        }

        #endregion Messages

        #region Errors

        /// <summary>
        /// Agrega uno o varios errores del modelo al resultado.
        /// </summary>
        /// <param name="errors">Lista de errores.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithErrors(IEnumerable<(string fieldName, string message)> errors)
        {
            if (errors?.Count() > 0)
            {
                ErrorSettings.Errors ??= new Dictionary<string, IEnumerable<string>>();

                foreach (var (fieldName, message) in errors)
                {
                    if (!string.IsNullOrWhiteSpace(fieldName) && !string.IsNullOrWhiteSpace(message))
                    {
                        if (!ErrorSettings.Errors.ContainsKey(fieldName))
                        {
                            ErrorSettings.Errors.Add(fieldName, new List<string> { message });
                        }
                        else
                        {
                            ErrorSettings.Errors[fieldName] = ErrorSettings.Errors[fieldName].Concat(new string[] { message });
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Agrega uno o varios errores del modelo al resultado.
        /// </summary>
        /// <param name="errors">Lista de errores.</param>
        /// <returns>ErrorOptions.</returns>
        public ErrorOptions WithErrors(IDictionary<string, IEnumerable<string>>? errors)
        {
            if (errors?.Count > 0)
            {
                ErrorSettings.Errors = errors;
            }

            return this;
        }

        #endregion Errors
    }
}
