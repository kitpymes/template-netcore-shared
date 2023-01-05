// -----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión DictionaryExtensions
    Contiene las extensiones del delegado Action
*/

/// <summary>
/// Clase de extensión <c>DictionaryExtensions</c>.
/// Contiene las extensiones para el diccionario de valores.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para el diccionario de valores.</para>
/// </remarks>
public static class DictionaryExtensions
{
    /// <summary>
    /// Agrega una clave si no existe o agrega un valor a la clave si esta existe.
    /// </summary>
    /// <param name="dictionary">Lista de diccionario.</param>
    /// <param name="key">Clave del diccionario.</param>
    /// <param name="value">Valor del diccionario.</param>
    /// <returns>IDictionary{string, IList{string}} | ApplicationException: si dictionary es nula o no contiene elementos, o si key es nulo o vacio o si value es nulo o vacio.</returns>
    public static IDictionary<string, IEnumerable<string>> AddOrUpdate(this IDictionary<string, IEnumerable<string>>? dictionary, string key, string value)
    {
        var validDictionary = dictionary.ValidateThrow(key, value);

        if (validDictionary.ContainsKey(key))
        {
            if (!validDictionary[key].Contains(value))
            {
                validDictionary[key] = validDictionary[key].Concat(new string[] { value });
            }
        }
        else
        {
            validDictionary.Add(key, new List<string> { value });
        }

        return validDictionary;
    }

    /// <summary>
    /// Verifica si existe una clave y su valor en la lista de tipo diccionario.
    /// </summary>
    /// <param name="dictionary">Lista de diccionario.</param>
    /// <param name="key">Clave del diccionario.</param>
    /// <param name="value">Valor del diccionario.</param>
    /// <returns>bool | ApplicationException: si dictionary es nula o no contiene elementos, o si key es nulo o vacio o si value es nulo o vacio.</returns>
    public static bool Contains(this IDictionary<string, IEnumerable<string>>? dictionary, string key, string value)
    {
        var validDictionary = dictionary.ValidateThrow(key, value);

        return validDictionary.ContainsKey(key) && validDictionary[key].Contains(value);
    }

    /// <summary>
    /// Obtiene los valores de una clave.
    /// </summary>
    /// <param name="dictionary">Lista de diccionario.</param>
    /// <param name="key">Clave del diccionario.</param>
    /// <returns>IList{string} | null: si la key no existe o no contiene valores | ApplicationException: si dictionary es nula o no contiene elementos, o si key es nulo o vacio.</returns>
    public static IEnumerable<string>? GetValues(this IDictionary<string, IEnumerable<string>>? dictionary, string key)
    {
        var validDictionary = dictionary.ValidateThrow(key);

        return validDictionary.TryGetValue(key, out IEnumerable<string>? value) ? value : null;
    }

    [return: NotNull]
    private static IDictionary<string, IEnumerable<string>> ValidateThrow(this IDictionary<string, IEnumerable<string>>? dictionary, string key)
    {
        var errors = new List<string>();

        if (dictionary.IsNullOrEmpty())
        {
            errors.Add(Util.Messages.NullOrEmpty(nameof(dictionary)));
        }

        if (key.IsNullOrEmpty())
        {
            errors.Add(Util.Messages.NullOrEmpty(nameof(key)));
        }

        VerifyExtensions.ThrowIf(errors);

        return dictionary!;
    }

    [return: NotNull]
    private static IDictionary<string, IEnumerable<string>> ValidateThrow(this IDictionary<string, IEnumerable<string>>? dictionary, string key, string value)
    {
        dictionary.ValidateThrow(key);

        value.ThrowIfNullOrEmpty();

        return dictionary!;
    }
}
