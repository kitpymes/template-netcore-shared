// -----------------------------------------------------------------------
// <copyright file="GuidExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión GuidExtensions
    Contiene las extensiones de la estrucura de Guid
*/

/// <summary>
/// Clase de extensión <c>GuidExtensions</c>.
/// Contiene las extensiones de la estrucura de Guid.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la estrucura de Guid.</para>
/// </remarks>
public static class GuidExtensions
{
    /// <summary>
    /// Crea un Guid de manera secuencial.
    /// </summary>
    /// <param name="input">Valor de Guid.</param>
    /// <returns>Guid secuencial.</returns>
    public static Guid ToSecuencial(this Guid input)
    {
        var counter = DateTime.UtcNow.Ticks;

        var bytes = input.ToByteArray();

        var counterBytes = BitConverter.GetBytes(Interlocked.Increment(ref counter));

        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(counterBytes);
        }

        bytes[08] = counterBytes[1];
        bytes[09] = counterBytes[0];
        bytes[10] = counterBytes[7];
        bytes[11] = counterBytes[6];
        bytes[12] = counterBytes[5];
        bytes[13] = counterBytes[4];
        bytes[14] = counterBytes[3];
        bytes[15] = counterBytes[2];

        return new Guid(bytes);
    }
}
