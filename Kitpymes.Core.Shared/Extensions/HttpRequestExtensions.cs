// -----------------------------------------------------------------------
// <copyright file="HttpRequestExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión HttpRequestExtensions
    Contiene las extensiones de las entradas HttpRequest
*/

/// <summary>
/// Clase de extensión <c>HttpRequestExtensions</c>.
/// Contiene las extensiones de las entradas HttpRequest.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para las entradas HttpRequest.</para>
/// </remarks>
public static class HttpRequestExtensions
{
    /// <summary>
    /// Obtiene un valor del header de una entrada HTTP.
    /// </summary>
    /// <param name="httpRequest">Representa el lado entrante de una solicitud HTTP individual.</param>
    /// <param name="key">Clave del valor del header a obtener.</param>
    /// <param name="values">Valores de la clave del header.</param>
    /// <returns>List{string} | null: si la key no existe | ApplicationException: si el parámetro httpRequest es nulo o si key es nulo.</returns>
    public static bool ToTryHeader(this HttpRequest httpRequest, string key, [MaybeNullWhen(false)] out List<string?>? values)
    {
        key.ThrowIfNullOrEmpty();

        values = httpRequest.ToHeader(key);

        var result = !values.IsNullOrAny();

        return result;
    }

    /// <summary>
    /// Obtiene un valor del header de una entrada HTTP.
    /// </summary>
    /// <param name="httpRequest">Representa el lado entrante de una solicitud HTTP individual.</param>
    /// <param name="key">Clave del valor del header a obtener.</param>
    /// <returns>List{string} | null: si la key no existe | ApplicationException: si el parámetro httpRequest es nulo o si key es nulo o vacio.</returns>
    public static List<string?>? ToHeader(this HttpRequest httpRequest, string key)
    {
        key.ThrowIfNullOrEmpty();

        if (httpRequest.Headers.ContainsKey(key))
        {
            var result = httpRequest.Headers[key];

            if (!result.IsNullOrAny())
            {
                return result.ToList();
            }
        }

        return null;
    }

    /// <summary>
    /// Obtiene la URL de la solicitud HTTP.
    /// </summary>
    /// <param name="httpRequest">Representa el lado entrante de una solicitud HTTP individual.</param>
    /// <returns>Devuelve la URL de la solicitud HTTP.</returns>
    public static string ToPath(this HttpRequest httpRequest)
    {
        var scheme = httpRequest.Scheme;
        var host = httpRequest.Host.Value;
        var path = httpRequest.Path.Value;
        var queryString = httpRequest.QueryString.Value;

        return $"{scheme}://{host}{path}{queryString}";
    }

    /// <summary>
    /// Obtiene el hostname o subdomain de una entrada HTTP.
    /// </summary>
    /// <param name="httpRequest">Representa el lado entrante de una solicitud HTTP individual.</param>
    /// <returns>string | null.</returns>
    public static string? ToSubdomain(this HttpRequest httpRequest)
    => httpRequest.Host.Host?.Split('.')[0].Trim();

    /// <summary>
    /// Obtiene el ContentType de una entrada HTTP.
    /// </summary>
    /// <param name="httpRequest">Representa el lado entrante de una solicitud HTTP individual.</param>
    /// <param name="contentType">Valor a devolver.</param>
    /// <returns>true | false | value: string or null.</returns>
    public static bool ToTryContentType(this HttpRequest httpRequest, [MaybeNullWhen(false)] out string? contentType)
    {
        contentType = httpRequest.ContentType;

        var result = !contentType.IsNullOrEmpty();

        return result;
    }
}
