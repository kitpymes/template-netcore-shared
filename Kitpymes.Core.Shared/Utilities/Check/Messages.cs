// -----------------------------------------------------------------------
// <copyright file="Messages.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    /*
        Clase de utilidades
        Contiene mensajes de validaciones
    */

    /// <summary>
    /// Clase de utilidades <c>Messages</c>.
    /// Contiene mensajes de validaciones.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las utilidades necesarias para los mensajes.</para>
    /// </remarks>
    public static class Messages
    {
        /// <summary>
        /// Devuelve "{paramName} is null or empty".
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>"{paramName} is null or empty".</returns>
        public static string NullOrEmpty(string paramName) => $"{paramName} is null or empty";

        /// <summary>
        /// Devuelve "{paramName} is null or not values".
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <returns>"{paramName} is null or not values".</returns>
        public static string NullOrAny(string paramName) => $"{paramName} is null or not values";

        /// <summary>
        /// Devuelve "{valueOrParamName} is not found".
        /// </summary>
        /// <param name="valueOrParamName">Valor o nombre del parámetro.</param>
        /// <returns>"{valueOrParamName} is not found".</returns>
        public static string NotFound(string valueOrParamName) => $"{valueOrParamName} is not found";

        /// <summary>
        /// Devuelve "{valueOrParamName} has invalid format".
        /// </summary>
        /// <param name="valueOrParamName">Valor o nombre del parámetro.</param>
        /// <returns>"{valueOrParamName} has invalid format".</returns>
        public static string InvalidFormat(string valueOrParamName) => $"{valueOrParamName} has invalid format";

        /// <summary>
        /// Devuelve "{paramName} and {paramNameCompare} not equals".
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <param name="paramNameCompare">Nombre del parámetro a comprar.</param>
        /// <returns>"{paramName} and {paramNameCompare} not equals".</returns>
        public static string NotEquals(string paramName, string paramNameCompare) => $"{paramName} and {paramNameCompare} not equals";

        /// <summary>
        /// Devuelve "{valueOrParamName} already exists".
        /// </summary>
        /// <param name="valueOrParamName">Valor o nombre del parámetro.</param>
        /// <returns>"{valueOrParamName} already exists".</returns>
        public static string AlreadyExists(string valueOrParamName) => $"{valueOrParamName} already exists";

        /// <summary>
        /// Devuelve "{paramName} must be greater than {min}".
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <returns>"{paramName} is null or not values".</returns>
        public static string Less(string paramName, long min) => $"{paramName} must be greater than {min}";

        /// <summary>
        /// Devuelve "{paramName} must be less than {max}".
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <param name="max">El valor máximo.</param>
        /// <returns>"{paramName} is null or not values".</returns>
        public static string Greater(string paramName, long max) => $"{paramName} must be less than {max}";

        /// <summary>
        /// Devuelve "{paramName} must be in the range {min} to {max}".
        /// </summary>
        /// <param name="paramName">Nombre del parámetro.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <param name="max">El valor máximo.</param>
        /// <returns>"{paramName} must be in the range {min} to {max}".</returns>
        public static string Range(string paramName, long min, long max) => $"{paramName} must be in the range {min} to {max}";
    }
}
