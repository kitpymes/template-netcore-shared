// -----------------------------------------------------------------------
// <copyright file="ClaimsPrincipalExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Claims;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión ClaimsPrincipalExtensions
    Contiene las extensiones de la clase ClaimsPrincipal
*/

/// <summary>
/// Clase de extensión <c>ClaimsPrincipalExtensions</c>.
/// Contiene las extensiones de la clase ClaimsPrincipal.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la clase ClaimsPrincipal.</para>
/// </remarks>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Verifica si el usuario esta autenticado en el sistema.
    /// </summary>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsAuthenticated(this ClaimsPrincipal claimsPrincipal)
    => claimsPrincipal.Identity?.IsAuthenticated == true;

    /// <summary>
    /// Obtiene el tipo de autenticación.
    /// </summary>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <returns>string | null.</returns>
    public static string? ToAuthenticationType(this ClaimsPrincipal claimsPrincipal)
    => claimsPrincipal.Identity?.AuthenticationType;

    /// <summary>
    /// Obtiene el nombre del usuario autenticado.
    /// </summary>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <returns>string | null.</returns>
    public static string? ToUserName(this ClaimsPrincipal claimsPrincipal)
    => claimsPrincipal.ToGet<string>(ClaimTypes.Name);

    /// <summary>
    /// Busca a ver si existe el nombre del tipo de claim.
    /// </summary>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
    /// <returns>true | false.</returns>
    public static bool ToExists(this ClaimsPrincipal claimsPrincipal, string claimType)
    => claimsPrincipal.HasClaim(x => x.Type == claimType);

    /// <summary>
    /// Agrega una lista de claims.
    /// </summary>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <param name="authenticationType">Tipo de autenticación.</param>
    /// <param name="claims">La lista de claims.</param>
    public static void ToAddIdentity(this ClaimsPrincipal claimsPrincipal, string? authenticationType, IEnumerable<Claim>? claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims, authenticationType);

        claimsPrincipal.AddIdentity(claimsIdentity);
    }

    /// <summary>
    /// Agrega una lista de claims.
    /// </summary>
    /// <typeparam name="T">El tipo de valor de la claim.</typeparam>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <param name="authenticationType">Tipo de autenticación.</param>
    /// <param name="claims">La lista de claims.</param>
    public static void ToAddIdentity<T>(this ClaimsPrincipal claimsPrincipal, string? authenticationType, params (string type, T value)[] claims)
    {
        var claimsList = new List<Claim>();

        foreach (var (type, value) in claims)
        {
            var claim = new Claim(type, value.ToSerialize());

            claimsList.Add(claim);
        }

        claimsPrincipal.ToAddIdentity(authenticationType, claimsList);
    }

    /// <summary>
    /// Obtiene una claim.
    /// </summary>
    /// <typeparam name="TResult">El tipo de valor de la claim.</typeparam>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
    /// <returns>TResult | null.</returns>
    public static TResult? ToGet<TResult>(this ClaimsPrincipal claimsPrincipal, string claimType)
        where TResult : class
    {
        var value = claimsPrincipal.FindFirstValue(claimType.ThrowIfNullOrEmpty());

        if (!string.IsNullOrEmpty(value))
        {
            var result = value.ToDeserialize<TResult>();

            return result;
        }

        return null;
    }

    /// <summary>
    /// Obtiene una claim.
    /// </summary>
    /// <typeparam name="TResult">El tipo de valor de la claim.</typeparam>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
    /// <returns>TResult | null.</returns>
    public static TResult? ToGetValue<TResult>(this ClaimsPrincipal claimsPrincipal, string claimType)
        where TResult : struct
    {
        claimType.ThrowIfNullOrEmpty();

        var value = claimsPrincipal.FindFirstValue(claimType);

        if (!string.IsNullOrEmpty(value))
        {
            var result = value.ToDeserialize<TResult>();

            return result;
        }

        return null;
    }

    /// <summary>
    /// Obtiene una lista de claims.
    /// </summary>
    /// <typeparam name="TResult">El tipo de valor de la claim.</typeparam>
    /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
    /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
    /// <returns>IEnumerable{TResult} | null.</returns>
    public static IEnumerable<TResult?>? ToGetAll<TResult>(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        claimType.ThrowIfNullOrEmpty();

        var values = claimsPrincipal.FindAll(claimType);

        if (!values.IsNullOrAny())
        {
            var result = values.Select(x => x.Value.ToDeserialize<TResult>());

            return result;
        }

        return null;
    }
}
