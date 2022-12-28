// -----------------------------------------------------------------------
// <copyright file="HttpResponseExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /*
      Clase de extensión HttpResponseExtensions
      Contiene las extensiones de las salidas HttpResponse
    */

    /// <summary>
    /// Clase de extensión <c>HttpResponseExtensions</c>.
    /// Contiene las extensiones de las salidas HttpResponse.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las salidas HttpResponse.</para>
    /// </remarks>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Respuesta de una petición HTTP a través de una HttpResponse.
        /// </summary>
        /// <param name="httpResponse">Representa el lado saliente de una solicitud HTTP individual.</param>
        /// <param name="status">Estado de la respuesta.</param>
        /// <param name="message">Mensaje de la respuesta.</param>
        /// <param name="contentType">Tipo de contenido de la respuesta.</param>
        /// <param name="headers">Cabezara de la respuesta.</param>
        /// <returns>Respuesta de la petición HTTP | ApplicationException: si el parámetro httpResponse es nulo.</returns>
        public static async Task ToResultAsync(
            this HttpResponse httpResponse,
            HttpStatusCode status,
            string message,
            string contentType = MediaTypeNames.Application.Json,
            IDictionary<string, IEnumerable<string>>? headers = null)
        {
            httpResponse.Clear();

            httpResponse.StatusCode = (int)status;

            httpResponse.ContentType = contentType;

            if (headers?.Count > 0)
            {
                foreach (var (key, values) in headers)
                {
                    httpResponse.Headers.AppendList(key, values.ToList());
                }
            }

            await httpResponse.WriteAsync(message).ConfigureAwait(false);
        }
    }
}
