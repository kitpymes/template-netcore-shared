// -----------------------------------------------------------------------
// <copyright file="Enums.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Resources;

    /*
      Clase de utilidades
      Contiene las utilidades de los Enum
    */

    /// <summary>
    /// Clase de utilidades <c>Enums</c>.
    /// Contiene las utilidades de los Enum.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las utilidades necesarias para los Enum.</para>
    /// </remarks>
    public static class Enums
    {
        /// <summary>
        /// Obtiene la cantidad de valores que tiene ese enum.
        /// </summary>
        /// <typeparam name="TEnum">Tipo de enum.</typeparam>
        /// <returns>La cantidad de valores.</returns>
        public static int ToCount<TEnum>()
            where TEnum : Enum
        => Enum.GetValues(typeof(TEnum)).Length;

        /// <summary>
        /// Obtiene una lista de cadena con los enum.
        /// </summary>
        /// <typeparam name="TEnum">Tipo de enum.</typeparam>
        /// <returns>La lista de enum en formato string.</returns>
        public static IEnumerable<string?> ToValues<TEnum>()
             where TEnum : struct, Enum
        => Enum.GetValues(typeof(TEnum)).OfType<Enum>().Select(x => x.ToString());

        /// <summary>
        /// Obtiene una lista de los enum.
        /// </summary>
        /// <typeparam name="TEnum">Tipo de enum.</typeparam>
        /// <param name="resourceType">Si deseamos usar recursos (resx).</param>
        /// <param name="selected">Si un valor tiene que ser seleccionado.</param>
        /// <returns>IEnumerable{(int value, string text, bool selected)}?.</returns>
        public static IEnumerable<(int value, string text, bool selected)> ToList<TEnum>(Type? resourceType = null, TEnum? selected = null)
                where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);

            ResourceManager? resource = null;

            if (resourceType != null)
            {
                resource = new ResourceManager(resourceType);
            }

            return Enum.GetValues(enumType).OfType<Enum>().Select(value =>
            (
                Convert.ToInt32(value, CultureInfo.CurrentCulture),
                resource?.GetString(value.ToString(), CultureInfo.CurrentCulture) ?? value.ToString(),
                selected != null && Convert.ToInt32(value, CultureInfo.CurrentCulture) == Convert.ToInt32(selected, CultureInfo.CurrentCulture)));
        }
    }
}
