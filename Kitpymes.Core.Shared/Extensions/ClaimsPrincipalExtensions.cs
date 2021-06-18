// -----------------------------------------------------------------------
// <copyright file="ClaimsPrincipalExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

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
        /// <returns>true | false | ApplicationExceptions: si el parámetro de tipo ClaimsPrincipal es nulo.</returns>
        public static bool ToIsAuthenticated(this ClaimsPrincipal claimsPrincipal)
        {
            var identity = claimsPrincipal.ToIsNullOrEmptyThrow(nameof(claimsPrincipal)).Identity;

            return !(identity is null) && identity.IsAuthenticated;
        }

        /// <summary>
        /// Obtiene el tipo de autenticación.
        /// </summary>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <returns>string | null | ApplicationExceptions: si el parámetro de tipo ClaimsPrincipal es nulo).</returns>
        public static string? ToAuthenticationType(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.ToIsNullOrEmptyThrow(nameof(claimsPrincipal)).Identity?.AuthenticationType;

        /// <summary>
        /// Obtiene el nombre del usuario autenticado.
        /// </summary>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <returns>string | null | ApplicationExceptions: si el parámetro de tipo ClaimsPrincipal es nulo).</returns>
        public static string? ToUserName(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.ToGet<string>(ClaimTypes.Name);

        /// <summary>
        /// Busca a ver si existe el nombre del tipo de claim.
        /// </summary>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
        /// <returns>true | false | ApplicationExceptions: si el parámetro de tipo ClaimsPrincipal es nulo.</returns>
        public static bool ToExists(this ClaimsPrincipal claimsPrincipal, string claimType)
        => claimsPrincipal.ToIsNullOrEmptyThrow(nameof(claimsPrincipal)).HasClaim(x => x.Type == claimType);

        /// <summary>
        /// Agrega una lista de claims.
        /// </summary>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <param name="authenticationType">Tipo de autenticación.</param>
        /// <param name="claims">La lista de claims.</param>
        public static void ToAdd(this ClaimsPrincipal claimsPrincipal, string authenticationType, IEnumerable<Claim> claims)
        => claimsPrincipal.ToIsNullOrEmptyThrow(nameof(claimsPrincipal)).AddIdentity(new ClaimsIdentity(claims, authenticationType));

        /// <summary>
        /// Agrega una lista de claims.
        /// </summary>
        /// <typeparam name="T">El tipo de valor de la claim.</typeparam>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <param name="authenticationType">Tipo de autenticación.</param>
        /// <param name="claims">La lista de claims.</param>
        public static void ToAdd<T>(this ClaimsPrincipal claimsPrincipal, string authenticationType, params (string claimType, T value)[] claims)
        {
            var errors = new List<string>();

            if (claimsPrincipal.ToIsNullOrEmpty())
            {
                errors.Add(Util.Messages.NullOrEmpty(nameof(claimsPrincipal)));
            }

            if (claims.ToIsNullOrAny())
            {
                errors.Add(Util.Messages.NullOrEmpty(nameof(claims)));
            }

            if (errors.Any())
            {
                Util.Check.Throw(errors);
            }

            var claimsList = new List<Claim>();

            foreach (var (claimType, value) in claims)
            {
                claimsList.Add(new Claim(claimType, value.ToSerialize()));
            }

            claimsPrincipal.ToAdd(authenticationType, claimsList);
        }

        /// <summary>
        /// Obtiene una claim.
        /// </summary>
        /// <typeparam name="TResult">El tipo de valor de la claim.</typeparam>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
        /// <returns>TResult | null | ApplicationExceptions: si el parámetro de tipo ClaimsPrincipal es nulo.</returns>
        public static TResult? ToGet<TResult>(this ClaimsPrincipal claimsPrincipal, string claimType)
            where TResult : class
        => claimsPrincipal.ToIsNullOrEmptyThrow(nameof(claimsPrincipal)).FindFirstValue(claimType)?.ToDeserialize<TResult>();

        /// <summary>
        /// Obtiene una claim.
        /// </summary>
        /// <typeparam name="TResult">El tipo de valor de la claim.</typeparam>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
        /// <returns>TResult | null | ApplicationExceptions: si el parámetro de tipo ClaimsPrincipal es nulo.</returns>
        public static TResult? ToGetValue<TResult>(this ClaimsPrincipal claimsPrincipal, string claimType)
            where TResult : struct
        => claimsPrincipal.ToIsNullOrEmptyThrow(nameof(claimsPrincipal)).FindFirstValue(claimType)?.ToDeserialize<TResult>();

        /// <summary>
        /// Obtiene una lista de claims.
        /// </summary>
        /// <typeparam name="TResult">El tipo de valor de la claim.</typeparam>
        /// <param name="claimsPrincipal">Una implementación System.Security.Principal.IPrincipal que admite múltiples identidades basadas en notificaciones.</param>
        /// <param name="claimType">El nombre del tipo de claim a buscar.</param>
        /// <returns>IEnumerable{TResult} | null | ApplicationExceptions: si el parámetro de tipo ClaimsPrincipal es nulo.</returns>
        public static IEnumerable<TResult?>? ToGetAll<TResult>(this ClaimsPrincipal claimsPrincipal, string claimType)
        => claimsPrincipal.ToIsNullOrEmptyThrow(nameof(claimsPrincipal)).FindAll(claimType)?.Select(x => x.Value.ToDeserialize<TResult>());
    }
}
