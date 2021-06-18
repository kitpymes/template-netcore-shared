// -----------------------------------------------------------------------
// <copyright file="ActionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;

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
        /// Combina las opciones personalizadas <paramref name="action"/> de una acción con las opciones por defecto <paramref name="overrideDefaultOptions"/>.
        /// </summary>
        /// <typeparam name="TOptions">Tipo de opcion que es de tipo class.</typeparam>
        /// <param name="action">Opciones personalizadas.</param>
        /// <param name="overrideDefaultOptions">Sobreescribimos las opcionces por defecto.</param>
        /// <returns>TOptions | ApplicationException: si action es nulo.</returns>
        [return: NotNull]
        public static TOptions ToConfigureOrDefault<TOptions>(this Action<TOptions>? action, TOptions? overrideDefaultOptions = null)
            where TOptions : class, new()
        {
            overrideDefaultOptions ??= new TOptions();

            action?.Invoke(overrideDefaultOptions);

            return overrideDefaultOptions;
        }
    }
}
