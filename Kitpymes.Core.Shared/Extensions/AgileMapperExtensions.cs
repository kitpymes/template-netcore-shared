// -----------------------------------------------------------------------
// <copyright file="AgileMapperExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AgileObjects.AgileMapper;
    using AgileObjects.AgileMapper.Api.Configuration;
    using AgileObjects.AgileMapper.Api.Configuration.Projection;
    using AgileObjects.AgileMapper.Extensions;

    /*
        Clase de extensión AgileMapperExtensions
        Contiene las extensiones del mapeador de objetos AgileMapper
    */

    /// <summary>
    /// Clase de extensión <c>AgileMapperExtensions</c>.
    /// Contiene las extensiones del mapeador de objetos AgileMapper.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para el mapeador de objetos AgileMapper.</para>
    /// </remarks>
    public static class AgileMapperExtensions
    {
        /// <summary>
        /// Clona un objeto del tipo TSource.
        /// </summary>
        /// <typeparam name="TSource">El tipo del objeto fuente a clonar.</typeparam>
        /// <param name="source">Objeto a clonar.</param>
        /// <param name="configurations">Configuraciones de mapeo.</param>
        /// <returns>Nuevo objeto clonado.</returns>
        public static TSource ToMapClone<TSource>(this TSource source, params Expression<Action<IFullMappingInlineConfigurator<TSource, TSource>>>[] configurations)
            where TSource : class
        => source.DeepClone(configurations);

        /// <summary>
        /// Crea un nuevo objeto del tipo TDestination a partir de los datos de un objeto del tipo TSource.
        /// </summary>
        /// <typeparam name="TSource">El tipo del objeto fuente a mapear.</typeparam>
        /// <typeparam name="TDestination">El tipo del objeto destino a mapear.</typeparam>
        /// <param name="source">Objeto fuente a mapear.</param>
        /// <param name="configurations">Configuraciones de mapeo.</param>
        /// <returns>Nuevo objeto del tipo TDestination.</returns>
        public static TDestination ToMapNew<TSource, TDestination>(this TSource source, params Expression<Action<IFullMappingInlineConfigurator<TSource, TDestination>>>[] configurations)
            where TSource : class
            where TDestination : class
        => source.Map().ToANew(configurations);

        /// <summary>
        /// Crea un nuevo objeto del tipo TDestination a partir de los datos de un objeto.
        /// </summary>
        /// <typeparam name="TDestination">El tipo del objeto destino a mapear.</typeparam>
        /// <param name="source">Objeto fuente a mapear.</param>
        /// <param name="configurations">Configuraciones de mapeo.</param>
        /// <returns>Nuevo objeto destino mapeado.</returns>
        public static TDestination ToMapNew<TDestination>(this object source, params Expression<Action<IFullMappingInlineConfigurator<object, TDestination>>>[] configurations)
            where TDestination : class
        => source.Map().ToANew(configurations);

        /// <summary>
        /// Actualiza un objeto del tipo TDestination desde un objeto del tipo TSource.
        /// Actualiza los valores de las propiedades del objeto del tipo TDestination, cuyo valores de las propiedades del objeto del tipo TSource NO sean igual a 'default' (null, empty, 0, etc).
        /// </summary>
        /// <typeparam name="TSource">El tipo del objeto fuente a mapear.</typeparam>
        /// <typeparam name="TDestination">El tipo del objeto destino a mapear.</typeparam>
        /// <param name="source">Objeto fuente a mapear.</param>
        /// <param name="destination">Objeto destino a mapear.</param>
        /// <param name="configurations">Configuraciones de mapeo.</param>
        /// <returns>Nuevo objeto actualizado.</returns>
        public static TDestination ToMapUpdate<TSource, TDestination>(this TSource source, TDestination destination, params Expression<Action<IFullMappingInlineConfigurator<TSource, TDestination>>>[] configurations)
            where TSource : class
            where TDestination : class
        => source.Map().Over(destination, configurations);

        /// <summary>
        /// Combina un objeto del tipo TSource en un objeto del tipo TDestination.
        /// Actualiza los valores de las propiedades del objeto del tipo TDestination que sean igual a 'default' (null, empty, 0, etc).
        /// </summary>
        /// <typeparam name="TSource">El tipo del objeto fuente a mapear.</typeparam>
        /// <typeparam name="TDestination">El tipo del objeto destino a mapear.</typeparam>
        /// <param name="source">Objeto fuente a mapear.</param>
        /// <param name="destination">Objeto destino a mapear.</param>
        /// <param name="configurations">Configuraciones de mapeo.</param>
        /// <returns>Nuevo objeto combinado.</returns>
        public static TDestination ToMapMerge<TSource, TDestination>(this TSource source, TDestination destination, params Expression<Action<IFullMappingInlineConfigurator<TSource, TDestination>>>[] configurations)
            where TSource : class
            where TDestination : class
        => source.Map().OnTo(destination, configurations);

        /// <summary>
        /// Mapea una lista.
        /// </summary>
        /// <typeparam name="TSource">El tipo del objeto fuente a mapear.</typeparam>
        /// <typeparam name="TDestination">El tipo del objeto destino a mapear.</typeparam>
        /// <param name="queryable">Lista fuente a mapear.</param>
        /// <param name="configuration">Configuracion del mapeo.</param>
        /// <returns>Nueva lista destino mapeada.</returns>
        public static IQueryable<TDestination> ToMapList<TSource, TDestination>(this IQueryable<TSource> queryable, Expression<Action<IFullProjectionInlineConfigurator<TSource, TDestination>>>? configuration = null)
            where TSource : class
            where TDestination : class
        => configuration is null ? queryable.Project().To<TDestination>() : queryable.Project().To(configuration);
    }
}
