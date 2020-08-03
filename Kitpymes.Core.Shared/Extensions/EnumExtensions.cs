// -----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /*
      Clase de extensión EnumExtensions
      Contiene las extensiones de los Enum
    */

    /// <summary>
    /// Clase de extensión <c>EnumExtensions</c>.
    /// Contiene las extensiones de los Enum.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las Enumeraciones.</para>
    /// </remarks>
    public static class EnumExtensions
    {
        /// <summary>
        /// Obtiene el número entero de la enumeración.
        /// </summary>
        /// <param name="name">Nombre de la enumeración.</param>
        /// <returns>El número entero de la enumeración.</returns>
        public static int ToValue(this Enum name)
        => (int)(IConvertible)name;

        /// <summary>
        /// Obtiene la descripción del atributo DescriptionAttribute de una enumeración.
        /// </summary>
        /// <param name="name">Nombre de la enumeración.</param>
        /// <returns>Descripción de la enumeración | ApplicationException: si el parámetro name es nulo.</returns>
        public static string? ToDescription(this Enum name)
        => name.ToAttribute<DescriptionAttribute>()?.Description ?? name?.ToString();

        /// <summary>
        /// Obtiene los valores del atributo DisplayAttribute de una enumeración.
        /// </summary>
        /// <param name="name">Nombre de la enumeración.</param>
        /// <returns>El nombre y la descipción de la enumeración | ApplicationException: si el parámetro name es nulo.</returns>
        public static (string? name, string? description) ToDisplay(this Enum name)
        {
            var attribute = name.ToAttribute<DisplayAttribute>();
            return attribute == null ? (name?.ToString(), name?.ToString()) : (attribute.Name, attribute.Description);
        }

        /// <summary>
        /// Obtiene el atributo de una enumeración.
        /// </summary>
        /// <typeparam name="TAttribute">Tipo de atributo a obtener.</typeparam>
        /// <param name="name">Nombre de la enumeración.</param>
        /// <returns>El atributo de la enumeración | ApplicationException: si el parámetro name es nulo.</returns>
        public static TAttribute? ToAttribute<TAttribute>(this Enum name)
            where TAttribute : Attribute
        {
            var validEnum = name.ToThrowIfNullOrEmpty(nameof(name));

            var type = validEnum.GetType();

            var memberInfo = type.GetMember(validEnum.ToString()).FirstOrDefault(m => m.DeclaringType == type);

            var attribute = Attribute.GetCustomAttribute(memberInfo, typeof(TAttribute), false);

            return attribute is TAttribute attribute1 ? attribute1 : null;
        }
    }
}
