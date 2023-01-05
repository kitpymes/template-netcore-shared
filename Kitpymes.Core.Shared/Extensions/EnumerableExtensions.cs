// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión EnumerableExtensions
    Contiene las extensiones de la interface IEnumerable
*/

/// <summary>
/// Clase de extensión <c>EnumerableExtensions</c>.
/// Contiene las extensiones de la interface IEnumerable.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la colección IEnumerable.</para>
/// </remarks>
public static class EnumerableExtensions
{
    /// <summary>
    /// Convierte una colección en un string con un separador.
    /// </summary>
    /// <typeparam name="T">Tipo de colección.</typeparam>
    /// <param name="enumerable">Colección.</param>
    /// <param name="separator">Separador.</param>
    /// <returns>string: concatenado con el separador.</returns>
    public static string ToString<T>(this IEnumerable<T> enumerable, string separator)
    => string.Join(separator, enumerable.ToArray());

    /// <summary>
    /// Convierte una colección nula en una instancia de la colección.
    /// </summary>
    /// <typeparam name="T">Tipo de colección.</typeparam>
    /// <param name="enumerable">Colección.</param>
    /// <returns>IEnumerable{T} | Empty.</returns>
    public static IEnumerable<T> ToEmptyIfNull<T>(this IEnumerable<T>? enumerable)
    => enumerable ?? Enumerable.Empty<T>();

    /// <summary>
    /// Convierte una colección que sea de solo lectura.
    /// </summary>
    /// <typeparam name="T">Tipo de colección.</typeparam>
    /// <param name="enumerable">Colección.</param>
    /// <returns>ReadOnlyCollection{T}.</returns>
    public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> enumerable)
    => enumerable is not ReadOnlyCollection<T> collection
            ? new List<T>(enumerable).AsReadOnly()
            : new List<T>(collection).AsReadOnly();

    /// <summary>
    /// Carga los assemblies por el nombre.
    /// </summary>
    /// <param name="assemblies">Nombre de los assemblies a cargar.</param>
    /// <returns>IEnumerable{Assembly}.</returns>
    public static IEnumerable<Assembly> ToAssembly(this IEnumerable<string> assemblies)
    => assemblies.Select(assembly => assembly.ToAssembly());
}
