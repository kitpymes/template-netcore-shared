// -----------------------------------------------------------------------
// <copyright file="HttpContextExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Microsoft.AspNetCore.Http;

    /*
      Clase de extensión HttpContextExtensions
      Contiene las extensiones del HttpContext
    */

    /// <summary>
    /// Clase de extensión <c>HttpContextExtensions</c>.
    /// Contiene las extensiones del HttpContext.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para el HttpContext.</para>
    /// </remarks>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Obtiene la IP de la solicitud HTTP.
        /// </summary>
        /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
        /// <param name="value">Valor a devolver.</param>
        /// <returns>string | null | ApplicationException: si el parámetro httpContext es nulo.</returns>
        public static bool ToTryIPv6(this HttpContext httpContext, [MaybeNullWhen(false)] out string? value)
        {
            var ip = httpContext.ToThrowIfNullOrEmpty(nameof(httpContext)).ToIPv6();

            if (string.IsNullOrWhiteSpace(ip))
            {
                value = null;
                return false;
            }

            value = ip;
            return true;
        }

        /// <summary>
        /// Obtiene la IP de la solicitud HTTP.
        /// </summary>
        /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
        /// <returns>string | null | ApplicationException: si el parámetro httpContext es nulo.</returns>
        public static string? ToIPv6(this HttpContext httpContext)
        => httpContext.ToThrowIfNullOrEmpty(nameof(httpContext)).Connection?.RemoteIpAddress?.MapToIPv6()?.ToString();

        /// <summary>
        /// Devuelve los detalles de una solicitud HTTP.
        /// </summary>
        /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
        /// <param name="optionalData">Datos opcionales.</param>
        /// <returns>Los detalles de una solicitud HTTP.</returns>
        public static string ToDetails(this HttpContext httpContext, string? optionalData = null)
        {
            var validHttpContext = httpContext.ToThrowIfNullOrEmpty(nameof(httpContext));

            var sb = new StringBuilder();

            if (validHttpContext.ToTryIPv6(out var ip))
            {
                sb.Append($"| IP: {ip} ");
            }

            if (validHttpContext.Request.ToTryHeader("User-Agent", out var values))
            {
                sb.Append($"| UserAgent: {values} ");
            }

            sb.Append($"| ContentType: {validHttpContext.Request.ContentType} ");

            sb.Append($"| User: {validHttpContext.User.ToUserName()}");

            sb.Append($"| {validHttpContext.Request.Method}: {validHttpContext.Request.ToPath()} ");

            if (!optionalData.ToIsNullOrEmpty())
            {
                sb.Append($"| Data: {optionalData} ");
            }

            return sb.ToString();
        }
    }
}
