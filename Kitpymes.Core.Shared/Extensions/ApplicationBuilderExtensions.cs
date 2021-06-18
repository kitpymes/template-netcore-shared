// -----------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    /*
      Clase de extensión ApplicationBuilderExtensions
      Contiene las extensiones de la aplicación IApplicationBuilder
    */

    /// <summary>
    /// Clase de extensión <c>ApplicationBuilderExtensions</c>.
    /// Contiene las extensiones de la aplicación.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para de la aplicación.</para>
    /// </remarks>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Obtiene la configuración del ambiente de ejecución.
        /// </summary>
        /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
        /// <returns>IWebHostEnvironment | ApplicationException: si application es nulo | null: si no se encuentra el servicio de tipo IWebHostEnvironment.</returns>
        public static IWebHostEnvironment? ToEnvironment(this IApplicationBuilder application)
        => application.ToIsNullOrEmptyThrow(nameof(application)).ToService<IWebHostEnvironment>();

        /// <summary>
        /// Verifica si existe un servicio.
        /// </summary>
        /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
        /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
        /// <returns>true | false | ApplicationException: si application es nulo.</returns>
        public static bool ToExists<TService>(this IApplicationBuilder application)
        => application.ToIsNullOrEmptyThrow(nameof(application)).ToService<TService>() != null;

        /// <summary>
        /// Obtiene el servicio del tipo solicitado.
        /// </summary>
        /// <typeparam name="TService">Tipo de servicio a buscar.</typeparam>
        /// <param name="application">Extensión de la configurarción de la solicitud de una aplicación.</param>
        /// <returns>TService | null: si no encuentra el servicio de tipo TService | ApplicationException: si application es nulo.</returns>
        public static TService? ToService<TService>(this IApplicationBuilder application)
        => application.ToIsNullOrEmptyThrow(nameof(application)).ApplicationServices.GetService<TService>();
    }
}
