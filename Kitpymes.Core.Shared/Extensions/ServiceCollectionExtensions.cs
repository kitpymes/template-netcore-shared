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
    using System.Linq;
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
        /// <returns>IWebHostEnvironment | nulo.</returns>
        public static IWebHostEnvironment? ToEnvironment(this IServiceProvider services)
        => services.ToIsNullOrEmptyThrow(nameof(services)).GetService<IWebHostEnvironment>();

        /// <summary>
        /// Obtiene los datos del ambiente de ejecución.
        /// </summary>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <returns>IWebHostEnvironment | nulo.</returns>
        public static IWebHostEnvironment? ToEnvironment(this IServiceCollection services)
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
        /// <returns>TService | nulo.</returns>
        public static TService? ToService<TService>(this IServiceCollection services)
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
            var errors = new List<string>();

            if (services.ToIsNullOrEmpty())
            {
                errors.Add(Util.Messages.NullOrEmpty(nameof(services)));
            }

            if (assemblies.ToIsNullOrAny())
            {
                errors.Add(Util.Messages.NullOrAny(nameof(assemblies)));
            }

            if (errors.Any())
            {
                Util.Check.Throw(errors);
            }

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
            var errors = new List<string>();

            if (services.ToIsNullOrEmpty())
            {
                errors.Add(Util.Messages.NullOrEmpty(nameof(services)));
            }

            if (directoryPath.ToIsNullOrEmpty())
            {
                errors.Add(Util.Messages.NullOrEmpty(nameof(directoryPath)));
            }
            else
            {
                if (!directoryPath.ToIsDirectory())
                {
                    errors.Add(Util.Messages.NotFound(directoryPath));
                }
            }

            if (files.ToIsNullOrAny())
            {
                errors.Add(Util.Messages.NullOrAny(nameof(files)));
            }

            if (errors.Any())
            {
                Util.Check.Throw(errors);
            }

            if (files != null)
            {
                var configurationBuilder = new ConfigurationBuilder()
                  .SetBasePath(directoryPath)
                  .ToJsonFiles(directoryPath, files)
                  .AddEnvironmentVariables()
                  .Build();

                services.ToConfiguration(configurationBuilder);
            }

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
            var errors = new List<string>();

            if (services.ToIsNullOrEmpty())
            {
                errors.Add(Util.Messages.NullOrEmpty(nameof(services)));
            }

            if (configuration.ToIsNullOrAny())
            {
                errors.Add(Util.Messages.NullOrAny(nameof(configuration)));
            }

            if (errors.Any())
            {
                Util.Check.Throw(errors);
            }

            var list = new Dictionary<string, string?>();

            foreach (var (key, value) in configuration)
            {
                list.Add(key, value);
            }

            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(list)
                .Build();

            services.ToConfiguration(configurationBuilder);

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
        /// <typeparam name="TConfigureSettings">Tipo de clase para configuraciones custom (Usada para obtener datos de la db en el momento de inicio del sistema).</typeparam>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <param name="defaultSettings">Configuración por defecto.</param>
        /// <returns>TSettings | null.</returns>
        public static TSettings? ToSettings<TSettings, TConfigureSettings>(this IServiceCollection services, Action<TSettings>? defaultSettings = null)
            where TSettings : class, new()
            where TConfigureSettings : class, IConfigureOptions<TSettings>
        {
            if (defaultSettings is null)
            {
                services.ToSettings((TSettings)null!);
            }
            else
            {
                services.ToSettings(defaultSettings.ToConfigureOrDefault());
            }

            var result = services
                .AddSingleton<IConfigureOptions<TSettings>, TConfigureSettings>()
                .ToService<IOptions<TSettings>>()?.Value;

            return result;
        }

        /// <summary>
        /// Configura las clases que coinciden con los archivos de configuración.
        /// </summary>
        /// <typeparam name="TSettings">Tipo de clase a configurar.</typeparam>
        /// <param name="services">Especifica el contrato para una colección de descriptores de servicio.</param>
        /// <param name="defaultSettings">Configuración por defecto.</param>
        /// <returns>IServiceCollection.</returns>
        public static TSettings ToSettings<TSettings>(this IServiceCollection services, TSettings? defaultSettings = null)
            where TSettings : class, new()
        {
            TSettings? settings;

            if (!services.ToExists<TSettings>())
            {
                services.AddOptions<TSettings>();

                var section = services.ToService<IConfiguration>()?.GetSection(typeof(TSettings).Name);

                if (section.Exists())
                {
                    services.Configure<TSettings>(section);

                    var service = services.ToService<IOptions<TSettings>>();

                    settings = service?.Value;

                    if (defaultSettings is not null)
                    {
                        settings = settings?.ToMapUpdate(defaultSettings);
                    }
                }
                else
                {
                    settings = defaultSettings ?? new TSettings();
                }

                if (settings != null)
                {
                    services.AddSingleton(settings);
                }
            }

            var result = services.ToService<TSettings>().ToIsNullOrEmptyThrow(typeof(TSettings).Name);

            return result;
        }

        private static IConfigurationBuilder ToJsonFiles(
            this IConfigurationBuilder builder,
            string directoryPath,
            params (string jsonFileName, bool optional, bool reloadOnChange)[] files)
        {
            foreach (var (jsonFileName, optional, reloadOnChange) in files)
            {
                var filePath = $"{directoryPath}/{jsonFileName}.json";

                filePath.ToIsFileThrow(nameof(filePath));

                builder.AddJsonFile(filePath, optional, reloadOnChange);
            }

            return builder;
        }

        #endregion ToSettings
    }
}
