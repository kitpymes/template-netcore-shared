// -----------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /*
      Clase de extensión QueryableExtensions
      Contiene las extensiones de IQueryable
    */

    /// <summary>
    /// Clase de extensión <c>QueryableExtensions</c>.
    /// Contiene las extensiones de IQueryable.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para IQueryable.</para>
    /// </remarks>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Filtra una secuencia de valores basada en un predicado.
        /// </summary>
        /// <typeparam name="T">Tipo de la lista.</typeparam>
        /// <param name="queryable">Instancia IQueryable.</param>
        /// <param name="where">Predicado a evaluar.</param>
        /// <returns>La lista con el filtro.</returns>
        public static IQueryable<T> ToWhere<T>(this IQueryable<T> queryable, Expression<Func<T, bool>>? where)
            where T : class
        {
            if (where != default)
            {
                queryable = queryable.Where(where);
            }

            return queryable;
        }

        /// <summary>
        /// Paginado de una lista.
        /// </summary>
        /// <typeparam name="T">Tipo de la lista.</typeparam>
        /// <param name="queryable">Instancia IQueryable.</param>
        /// <param name="property">Nombre del campo por el que se requiere ordenar.</param>
        /// <param name="ascending">Orden de la lista ascendente o descendente.</param>
        /// <param name="index">Desde que página empieza la lista.</param>
        /// <param name="size">La cantidad de páginas a listar.</param>
        /// <returns>La lista paginada.</returns>
        public static IQueryable<T> ToPaged<T>(
            this IQueryable<T> queryable,
            string property,
            bool ascending = true,
            int index = 1,
            int size = 20)
        {
            if (queryable.ToIsNullOrAny())
            {
                return queryable;
            }

            if (!string.IsNullOrWhiteSpace(property))
            {
                queryable = queryable.ToOrder(property, ascending);
            }

            queryable.Skip((index - 1) * size).Take(size);

            return queryable;
        }

        private static IQueryable<T> ToOrder<T>(
            this IQueryable<T> queryable,
            string property,
            bool ascending)
        {
            queryable.ToIsNullOrAnyThrow(nameof(queryable));
            property.ToIsNullOrEmptyThrow(nameof(property));

            var properties = property.Split('.');

            Type? propertyType = typeof(T);

            propertyType = properties.Aggregate(propertyType, (Type? propertyTypeCurrent, string propertyName) =>
                propertyTypeCurrent?.GetProperty(propertyName)?.PropertyType);

            if (propertyType == typeof(sbyte))
            {
                return queryable.ToOrder<T, sbyte>(properties, ascending);
            }

            if (propertyType == typeof(short))
            {
                return queryable.ToOrder<T, short>(properties, ascending);
            }

            if (propertyType == typeof(int))
            {
                return queryable.ToOrder<T, int>(properties, ascending);
            }

            if (propertyType == typeof(long))
            {
                return queryable.ToOrder<T, long>(properties, ascending);
            }

            if (propertyType == typeof(byte))
            {
                return queryable.ToOrder<T, byte>(properties, ascending);
            }

            if (propertyType == typeof(ushort))
            {
                return queryable.ToOrder<T, ushort>(properties, ascending);
            }

            if (propertyType == typeof(uint))
            {
                return queryable.ToOrder<T, uint>(properties, ascending);
            }

            if (propertyType == typeof(ulong))
            {
                return queryable.ToOrder<T, ulong>(properties, ascending);
            }

            if (propertyType == typeof(char))
            {
                return queryable.ToOrder<T, char>(properties, ascending);
            }

            if (propertyType == typeof(float))
            {
                return queryable.ToOrder<T, float>(properties, ascending);
            }

            if (propertyType == typeof(double))
            {
                return queryable.ToOrder<T, double>(properties, ascending);
            }

            if (propertyType == typeof(decimal))
            {
                return queryable.ToOrder<T, decimal>(properties, ascending);
            }

            if (propertyType == typeof(bool))
            {
                return queryable.ToOrder<T, bool>(properties, ascending);
            }

            if (propertyType == typeof(string))
            {
                return queryable.ToOrder<T, string>(properties, ascending);
            }

            return queryable.ToOrder<T, object>(properties, ascending);
        }

        private static IQueryable<T> ToOrder<T, TKey>(
            this IQueryable<T> queryable,
            IEnumerable<string> properties,
            bool ascending)
        {
            queryable.ToIsNullOrAnyThrow(nameof(queryable));
            properties.ToIsNullOrAnyThrow(nameof(properties));

            var parameters = Expression.Parameter(typeof(T));

            var body = properties.Aggregate<string, Expression>(parameters, Expression.Property);

            var expression = Expression.Lambda<Func<T, TKey>>(body, parameters).Compile();

            return ascending ? queryable.AsEnumerable().OrderBy(expression).AsQueryable() : queryable.AsEnumerable().OrderByDescending(expression).AsQueryable();
        }
    }
}
