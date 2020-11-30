// -----------------------------------------------------------------------
// <copyright file="IResultMessage.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    /// <summary>
    /// Devuelve un objeto con un mensaje.
    /// </summary>
    public interface IResultMessage : IResult
    {
        /// <summary>
        /// Obtiene un mensaje.
        /// </summary>
        string? Message { get; }

        /// <summary>
        /// Obtiene un detalle.
        /// </summary>
        object? Details { get; }
    }
}
