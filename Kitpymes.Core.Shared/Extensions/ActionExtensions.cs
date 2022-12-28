// -----------------------------------------------------------------------
// <copyright file="ActionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión ActionExtensions
    Contiene las extensiones del delegado Action
*/

/// <summary>
/// Clase de extensión <c>ActionExtensions</c>.
/// Contiene las extensiones del delegado Action.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para el delegado Action.</para>
/// </remarks>
public static class ActionExtensions
{
    /// <summary>
    /// Combina las opciones personalizadas <paramref name="action"/> con las opciones por defecto <paramref name="defaultOptions"/>.
    /// </summary>
    /// <typeparam name="TOptions">Tipo de opcion que es de tipo class.</typeparam>
    /// <param name="action">Configuración personalizada que sobreescribe las opciones por defecto.</param>
    /// <param name="defaultOptions">Opcionces por defecto.</param>
    /// <returns>TOptions.</returns>
    [return: NotNull]
    public static TOptions ToConfigureOrDefault<TOptions>(this Action<TOptions>? action, TOptions? defaultOptions = null)
        where TOptions : class, new()
    {
        defaultOptions ??= new TOptions();

        action?.Invoke(defaultOptions);

        return defaultOptions;
    }
}