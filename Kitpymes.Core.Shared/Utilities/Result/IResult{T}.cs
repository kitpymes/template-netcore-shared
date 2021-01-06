// -----------------------------------------------------------------------
// <copyright file="IResult{T}.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared.Util
{
    /// <summary>
    /// Resultado.
    /// </summary>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        /// Obtiene un objeto con datos.
        /// </summary>
        T Data { get; }
    }
}
