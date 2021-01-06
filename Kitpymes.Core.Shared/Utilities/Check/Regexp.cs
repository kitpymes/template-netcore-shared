// -----------------------------------------------------------------------
// <copyright file="Regexp.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    /*
        Clase de expresiones regulares de Regexp
        Contiene las expresiones regulares mas utilizadas
    */

    /// <summary>
    /// Clase de expresiones regulares <c>Regexp</c>.
    /// Contiene las expresiones regulares mas utilizadas.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las expresiones regulares necesarias.</para>
    /// </remarks>
    public static class Regexp
    {
        /// <summary>
        /// Expresión regular de una fecha.
        /// </summary>
        public const string ForDate = @"^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))) ?((20|21|22|23|[01]\d|\d)(([:.][0-5]\d){1,2}))?$";

        /// <summary>
        /// Expresión regular de un número decimal.
        /// </summary>
        public const string ForDecimal = @"^((-?[1-9]+)|[0-9]+)(\.?|\,?)([0-9]*)$";

        /// <summary>
        /// Expresión regular para un email.
        /// </summary>
        public const string ForEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        /// <summary>
        /// Expresión regular de un número hexadecimal.
        /// </summary>
        public const string ForHex = "^#?([a-f0-9]{6}|[a-f0-9]{3})$";

        /// <summary>
        /// Expresión regular de un número entero.
        /// </summary>
        public const string ForInteger = "^((-?[1-9]+)|[0-9]+)$";

        /// <summary>
        /// Expresión regular para un tag.
        /// </summary>
        public const string ForTag = @"^<([a-z1-6]+)([^<]+)*(?:>(.*)<\/\1>| *\/>)$";

        /// <summary>
        /// Expresión regular para la hora.
        /// </summary>
        public const string ForTime = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";

        /// <summary>
        /// Expresión regular para una url.
        /// </summary>
        public const string ForUrl = @"^((https?|ftp|file):\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

        /// <summary>
        /// Expresión regular para un nombre.
        /// </summary>
        public const string ForName = @"^[a-zA-Z áàäâãÁÀÄÂÃÅéèëêÉÈËÊíìïîÍÌÏÎóòöôõøÓÒÖÔÕúùüûÚÙÜÛçÇħñÑ]*$";

        /// <summary>
        /// Expresión regular para un subdomain.
        /// </summary>
        public const string ForSubdomain = @"^[-._a-zA-Z0-9]*$";

        /// <summary>
        /// Expresión regular para un domain.
        /// </summary>
        public const string ForDomain = @"(?=^.{1,254}$)(^(?:(?!\d+\.|-)[a-zA-Z0-9_\-]{1,63}(?<!-)\.?)+(?:[a-zA-Z]{2,})$)";

        /// <summary>
        /// Expresión regular para un hostname.
        /// </summary>
        public const string ForHostname = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";
    }
}
