// -----------------------------------------------------------------------
// <copyright file="IResult.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    /// <summary>
    /// Resultado.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Obtiene un valor que indica si el proceso fue correcto.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Serializa un objeto.
        /// </summary>
        /// <returns>string.</returns>
        string ToJson();
    }
}
