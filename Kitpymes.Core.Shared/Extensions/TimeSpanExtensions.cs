// -----------------------------------------------------------------------
// <copyright file="TimeSpanExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;

    /*
      Clase de extensión TimeSpanExtensions
      Contiene las extensiones de la clase TimeSpan
    */

    /// <summary>
    /// Clase de extensión <c>TimeSpanExtensions</c>.
    /// Contiene las extensiones de la clase TimeSpan.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones de la clase TimeSpan.</para>
    /// </remarks>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Convierte la hora de tipo TimeSpan en una cadena.
        /// </summary>
        /// <param name="time">Hola a convertir.</param>
        /// <returns>"{days} - {hours}:{minutes}:{seconds}".</returns>
        public static string ToFormat(this TimeSpan time)
        {
            var days = (time.Days > 0) ? $"{time.Days}d" : string.Empty;
            var hours = (time.Hours > 0) ? $"{time.Hours}h" : string.Empty;
            var minutes = (time.Minutes > 0) ? $"{time.Minutes}m" : string.Empty;
            var seconds = (time.Seconds > 0) ? $"{time.Seconds}s" : string.Empty;

            return $"{days} - {hours}:{minutes}:{seconds}".Trim();
        }
    }
}
