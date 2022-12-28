// -----------------------------------------------------------------------
// <copyright file="HttpContextExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Kitpymes.Core.Shared;

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
    /// <param name="ip">Valor de la ip a devolver.</param>
    /// <returns>string | null.</returns>
    public static bool ToTryIPv6(this HttpContext httpContext, [MaybeNullWhen(false)] out string? ip)
    {
        ip = httpContext?.ToIPv6();

        return !ip.IsNullOrEmpty();
    }

    /// <summary>
    /// Obtiene la IP de la solicitud HTTP.
    /// </summary>
    /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
    /// <returns>string | null | ApplicationException: si el parámetro httpContext es nulo.</returns>
    public static string? ToIPv6(this HttpContext httpContext)
    => httpContext.ThrowIfNullOrEmpty().Connection?.RemoteIpAddress?.MapToIPv6()?.ToString();

    /// <summary>
    /// Devuelve los detalles de una solicitud HTTP.
    /// </summary>
    /// <param name="httpContext">Encapsula toda la información específica de HTTP sobre una solicitud HTTP individual.</param>
    /// <param name="optionalData">Datos opcionales.</param>
    /// <returns>Los detalles de una solicitud HTTP.</returns>
    public static string ToDetails(this HttpContext httpContext, IDictionary<string, IEnumerable<string>>? optionalData = null)
    {
        var validHttpContext = httpContext.ThrowIfNullOrEmpty();

        var sb = new StringBuilder();

        if (validHttpContext.ToTryIPv6(out var ip))
        {
            sb.Append($"IP: {ip} |");
        }

        sb.Append($" User: {validHttpContext.User.ToUserName() ?? Environment.UserName ?? "Anonymous"} |");

        if (!validHttpContext.Request.IsNullOrEmpty())
        {
            if (validHttpContext.Request.ToTryHeader("User-Agent", out var userAgents))
            {
                sb.Append($" UserAgent: [{userAgents?.ToString(", ")}] |");
            }

            if (validHttpContext.Request.ToTryContentType(out var contentType))
            {
                sb.Append($" ContentType: {contentType} |");
            }

            sb.Append($" {validHttpContext.Request.Method}: {validHttpContext.Request.ToPath()} |");
        }

        if (!optionalData.IsNullOrEmpty())
        {
            foreach (var (key, values) in optionalData)
            {
                sb.Append($" {key}: [{values?.ToString(", ")}] |");
            }
        }

        return sb.ToString();
    }
}
