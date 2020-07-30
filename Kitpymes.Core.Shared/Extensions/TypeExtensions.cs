// -----------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Collections;
    using System.Reflection;

    /*
      Clase de extensión TypeExtensions
      Contiene las extensiones de la clase Type
    */

    /// <summary>
    /// Clase de extensión <c>TypeExtensions</c>.
    /// Contiene las extensiones de la clase Type.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de la clase Type.</para>
    /// </remarks>
    public static class TypeExtensions
    {
        /// <summary>
        /// Obtiene el valor pode defecto.
        /// </summary>
        /// <param name="type">El tipo del valor.</param>
        /// <returns>El valor por defecto.</returns>
        public static object? ToDefaultValue(this Type type)
        {
            if (type is null)
            {
                return default;
            }

            return type.IsValueType ? Activator.CreateInstance(type) : default;
        }

        /// <summary>
        /// Obtiene los nombres y valores de las propiedades de un objeto.
        /// </summary>
        /// <param name="type">Tipo de objeto.</param>
        /// <returns>Lista de nombre y valores de las propiedades.</returns>
        public static IDictionary? ToDictionary(this Type type)
        => type is null ? default : type.GetProperties().ToDictionaryPropertyInfo();

        /// <summary>
        /// Obtiene el ensamblado de un tipo de objeto.
        /// </summary>
        /// <param name="type">Tipo de objeto.</param>
        /// <returns>El ensamblado.</returns>
        public static Assembly ToAssembly(this Type type)
        => type.GetTypeInfo().Assembly;
    }
}
