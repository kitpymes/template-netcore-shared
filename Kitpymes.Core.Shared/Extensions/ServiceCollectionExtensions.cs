// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Scrutor;

    /*
      Clase de extensión ServiceCollectionExtensions
      Contiene las extensiones de la colección de servicios
    */

    /// <summary>
    /// Clase de extensión <c>ServiceCollectionExtensions</c>.
    /// Contiene las extensiones de la colección de servicios.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para la colección  de descriptores de los servicios.</para>
    /// </remarks>
    public static class ServiceCollectionExtensions
    {
        #region ToEnvironment

        /// <summary>
        /// Obtiene los datos del ambiente de ejecución.
        /// </summary>
        /// <param name="services">Define un mecanismo para recuperar un objeto de servicio.</param>
        /// <returns>El servicio del tipo IWebHostEnvironment o nulo.</returns>
        public static IWebHostEnvironment ToEnvironment(this IServiceProvider services)
        => services.ToIsNullOrEmptyThrow(nameof(services)).GetService<IWebHostEnvironment>();

        /// <summary>
        /// Obtiene los datos del ambiente de ejecución.
        /// </summary>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <returns>El servicio del tipo IWebHostEnvironment o nulo.</returns>
        public static IWebHostEnvironment ToEnvironment(this IServiceCollection services)
        => services.ToIsNullOrEmptyThrow(nameof(services)).ToService<IWebHostEnvironment>();

        #endregion ToEnvironment

        #region ToExists

        /// <summary>
        /// Verifica si existe un servicio.
        /// </summary>
        /// <typeparam name="TService">Tipo del servicio a buscar.</typeparam>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <returns>Verdadero o falso.</returns>
        public static bool ToExists<TService>(this IServiceCollection services)
        => services.ToIsNullOrEmptyThrow(nameof(services)).ToService<TService>() != null;

        #endregion ToExists

        #region ToService

        /// <summary>
        /// Obtiene el servicio del tipo solicitado.
        /// </summary>
        /// <typeparam name="TService">Tipo del servicio a buscar.</typeparam>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <returns>El servicio o nulo.</returns>
        public static TService ToService<TService>(this IServiceCollection services)
        => services.ToIsNullOrEmptyThrow(nameof(services)).BuildServiceProvider().GetService<TService>();

        /// <summary>
        /// Registra los servicios que la clase coincidan con la interfaz, utilizando el patrón (ClassName coincide con IClassName).
        /// </summary>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <param name="assemblies">Los assemblies se los servicios que queremos agregar a la colección.</param>
        /// <param name="lifetime">Especifica la vida útil de un servicio.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection ToServiceMatchingInterface(
            this IServiceCollection services,
            Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.ToIsNullOrEmptyThrow(nameof(services));
            assemblies.ToIsNullOrAnyThrow(nameof(assemblies));

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface((service, filter) =>
                    filter.Where(implementation => implementation.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)))
                .WithLifetime(lifetime));

            return services;
        }

        #endregion ToService

        #region ToConfiguration

        /// <summary>
        /// Agrega los servicios de configuración de archivos json.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="directoryPath">Ruta del directorio donde se encuentran los atchivos json.</param>
        /// <param name="files">Los archivos json.</param>
        /// <returns>La colección de servicios.</returns>
        public static IServiceCollection ToConfiguration(
            this IServiceCollection services,
            string directoryPath,
            params (string jsonFileName, bool optional, bool reloadOnChange)[] files)
        {
            services.ToIsNullOrEmptyThrow(nameof(services));
            directoryPath.ToIsDirectoryThrow(nameof(directoryPath));

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(directoryPath)
                .ToJsonFiles(directoryPath, files)
                .AddEnvironmentVariables()
                .Build();

            services.ToConfiguration(configurationBuilder);

            return services;
        }

        /// <summary>
        /// Agrega los servicios de configuración de las claves y su respectivo valor (key-value).
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="configuration">Las claves y sus valores (key-value).</param>
        /// <returns>La colección de servicios.</returns>
        public static IServiceCollection ToConfiguration(
            this IServiceCollection services,
            params (string key, string? value)[] configuration)
        {
            var list = new Dictionary<string, string?>();

            foreach (var (key, value) in configuration)
            {
                list.Add(key, value);
            }

            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(list)
                .Build();

            services.ToIsNullOrEmptyThrow(nameof(services)).ToConfiguration(configurationBuilder);

            return services;
        }

        /// <summary>
        /// Agrega los servicios de configuración.
        /// </summary>
        /// <param name="services">Colección de servicios.</param>
        /// <param name="configuration">Representa un conjunto de propiedades de configuración de la aplicación clave / valor.</param>
        /// <returns>La colección de servicios.</returns>
        public static IServiceCollection ToConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.ToIsNullOrEmptyThrow(nameof(services)).TryAddSingleton(configuration);

            return services;
        }

        #endregion ToConfiguration

        #region ToSettings

        /// <summary>
        /// Configura las clases que coinciden con los archivos de configuración.
        /// </summary>
        /// <typeparam name="TSettings">Tipo de clase a configurar.</typeparam>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <param name="defaultSettings">Configuración por defecto.</param>
        /// <returns>Una instancia del tipo TSettings.</returns>
        public static TSettings ToSettings<TSettings>(this IServiceCollection services, Action<TSettings>? defaultSettings = null)
            where TSettings : class, new()
        => services
            .ToIsNullOrEmptyThrow(nameof(services))
            .ToSettings(defaultSettings.ToConfigureOrDefault())
            .ToService<TSettings>();

        /// <summary>
        /// Configura las clases que coinciden con los archivos de configuración.
        /// </summary>
        /// <typeparam name="TSettings">Tipo de clase a configurar.</typeparam>
        /// <typeparam name="TConfigureSettings">Tipo de clase para configuraciones custom (Usada para obtener datos de la db en el momento de inicio del sistema).</typeparam>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <param name="defaultSettings">Configuración por defecto.</param>
        /// <returns>Una instancia del tipo TSettings.</returns>
        public static TSettings ToSettings<TSettings, TConfigureSettings>(this IServiceCollection services, Action<TSettings>? defaultSettings = null)
            where TSettings : class, new()
            where TConfigureSettings : class, IConfigureOptions<TSettings>
        => services
            .ToIsNullOrEmptyThrow(nameof(services))
            .ToSettings(defaultSettings.ToConfigureOrDefault())
            .AddSingleton<IConfigureOptions<TSettings>, TConfigureSettings>()
            .ToService<IOptions<TSettings>>().Value;

        /// <summary>
        /// Configura las clases que coinciden con los archivos de configuración.
        /// </summary>
        /// <typeparam name="TSettings">Tipo de clase a configurar.</typeparam>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <param name="defaultSettings">Configuración por defecto.</param>
        /// <returns>Una instancia del tipo TSettings.</returns>
        public static IServiceCollection ToSettings<TSettings>(this IServiceCollection services, TSettings? defaultSettings)
            where TSettings : class, new()
        {
            TSettings settings;

            if (!services.ToIsNullOrEmptyThrow(nameof(services)).ToExists<TSettings>())
            {
                services.AddOptions<TSettings>();

                var section = services.ToService<IConfiguration>()?.GetSection(typeof(TSettings).Name);

                if (section.Exists())
                {
                    services.Configure<TSettings>(section);

                    settings = services.ToService<IOptions<TSettings>>().Value;

                    if (defaultSettings != null)
                    {
                        settings = settings.ToMapUpdate(defaultSettings);
                    }
                }
                else
                {
                    settings = defaultSettings ?? new TSettings();
                }

                services.AddSingleton(settings);
            }

            return services;
        }

        private static IConfigurationBuilder ToJsonFiles(
            this IConfigurationBuilder builder,
            string directoryPath,
            params (string jsonFileName, bool optional, bool reloadOnChange)[] files)
        {
            foreach (var (jsonFileName, optional, reloadOnChange) in files)
            {
                var filePath = $"{directoryPath}/{jsonFileName}.json";

                filePath.ToIsFileThrow(nameof(jsonFileName));

                builder.AddJsonFile(filePath, optional, reloadOnChange);
            }

            return builder;
        }

        #endregion ToSettings
    }
}
