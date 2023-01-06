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
        /// Devuelve "{name} is null or empty".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <returns>"{name} is null or empty".</returns>
        public static string NullOrEmpty(string name) => $"{name} is null or empty";

        /// <summary>
        /// Devuelve "{name} is null or not values".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <returns>"{name} is null or not values".</returns>
        public static string NullOrAny(string name) => $"{name} is null or not values";

        /// <summary>
        /// Devuelve "{valueOrParamName} is not found".
        /// </summary>
        /// <param name="valueOrName">Valor o nombre del parámetro.</param>
        /// <returns>"{valueOrParamName} is not found".</returns>
        public static string NotFound(string valueOrName) => $"{valueOrName} is not found";

        /// <summary>
        /// Devuelve "{valueOrParamName} has invalid format".
        /// </summary>
        /// <param name="valueOrName">Valor o nombre del parámetro.</param>
        /// <returns>"{valueOrParamName} has invalid format".</returns>
        public static string InvalidFormat(string valueOrName) => $"{valueOrName} has invalid format";

        /// <summary>
        /// Devuelve "{name} and {nameCompare} not equals".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="nameCompare">Nombre del parámetro a comprar.</param>
        /// <returns>"{name} and {nameCompare} not equals".</returns>
        public static string NotEquals(string name, string nameCompare) => $"{name} and {nameCompare} not equals";

        /// <summary>
        /// Devuelve "{valueOrParamName} already exists".
        /// </summary>
        /// <param name="valueOrParamName">Valor o nombre del parámetro.</param>
        /// <returns>"{valueOrParamName} already exists".</returns>
        public static string AlreadyExists(string valueOrParamName) => $"{valueOrParamName} already exists";

        /// <summary>
        /// Devuelve "{name} must be greater than {min}".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <returns>"{name} is null or not values".</returns>
        public static string Less(string name, long min) => $"{name} must be greater than {min}";

        /// <summary>
        /// Devuelve "{name} must be greater or equal than {min}".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <returns>"{name} must be greater or equal than {min}".</returns>
        public static string LessOrEqual(string name, long min) => $"{name} must be greater or equal than {min}";

        /// <summary>
        /// Devuelve "{name} must be less than {max}".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="max">El valor máximo.</param>
        /// <returns>"{name} must be less than {max}".</returns>
        public static string Greater(string name, long max) => $"{name} must be less than {max}";

        /// <summary>
        /// Devuelve "{name} must be less or equal than {max}".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="max">El valor máximo.</param>
        /// <returns>"{name} must be less or equal than {max}".</returns>
        public static string GreaterOrEqual(string name, long max) => $"{name} must be less or equal than {max}";

        /// <summary>
        /// Devuelve "{name} must be in the range {min} to {max}".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <param name="max">El valor máximo.</param>
        /// <returns>"{name} must be in the range {min} to {max}".</returns>
        public static string InsideRange(string name, long min, long max)
            => $"{name} must be in the range {min} to {max}";

        /// <summary>
        /// Devuelve "{name} must be out the range {min} to {max}".
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="min">El valor mínimo.</param>
        /// <param name="max">El valor máximo.</param>
        /// <returns>"{name} must be out the range {min} to {max}".</returns>
        public static string OutRange(string name, long min, long max)
            => $"{name} must be out the range {min} to {max}";
    }
}
