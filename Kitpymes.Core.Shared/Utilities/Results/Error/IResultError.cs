// -----------------------------------------------------------------------
// <copyright file="IResultError.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    using System.Collections.Generic;

    /// <summary>
    /// Devuelve un objeto con errores.
    /// </summary>
    public interface IResultError : IResult
    {
        /// <summary>
        /// Obtiene la cantidad de errores.
        /// </summary>
        int? Count { get; }

        /// <summary>
        /// Obtiene la lista de errores.
        /// </summary>
        IDictionary<string, string>? Errors { get; }
    }
}
