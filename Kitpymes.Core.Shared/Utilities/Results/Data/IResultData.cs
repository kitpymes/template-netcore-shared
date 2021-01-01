// -----------------------------------------------------------------------
// <copyright file="IResultData.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System.Collections.Generic;

    /// <summary>
    /// Devuelve un objeto con datos.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto.</typeparam>
    public interface IResultData<out T> : IResult
        where T : class
    {
        /// <summary>
        /// Obtiene un objeto con datos.
        /// </summary>
        T? Data { get; }

        /// <summary>
        /// Obtiene la lista de errores.
        /// </summary>
        IEnumerable<string>? Errors { get; }
    }
}
