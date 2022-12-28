// -----------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Reflection;
using System.Text;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión ExceptionExtensions
    Contiene las extensiones de las Excepciones
*/

/// <summary>
/// Clase de extensión <c>EnumExtensions</c>.
/// Contiene las extensiones de los Enum.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para las excepciones.</para>
/// </remarks>
public static class ExceptionExtensions
{
    /// <summary>
    /// Obtiene el mensaje de la excepción.
    /// </summary>
    /// <param name="exception">Excepción que se obtiene el mensaje.</param>
    /// <returns>Mensaje de la excepción | ApplicationException: si el parámetro exception es nulo.</returns>
    public static string ToFullMessage(this Exception exception)
    {
        exception.ThrowIfNullOrEmpty();

        return exception.InnerException is null
            ? exception.Message
            : exception.Message + "\n\n --> " + exception.InnerException.ToFullMessage();
    }

    /// <summary>
    /// Obtiene un mensaje completo de la excepción.
    /// </summary>
    /// <param name="exception">Excepción que se obtiene el mensaje.</param>
    /// <param name="options">Opsiones del mensaje.</param>
    /// <returns>Mensaje completo de la excepción.</returns>
    public static string ToFullMessageDetail(this Exception exception, Action<ExceptionOptions>? options = null)
    {
        exception.ThrowIfNullOrEmpty();

        var settings = options.ToConfigureOrDefault(ExceptionOptions.Default);

        var stringBuilder = new StringBuilder();

        stringBuilder.AppendValue("Type", exception.GetType().FullName, settings);

        foreach (PropertyInfo property in exception
            .GetType()
            .GetProperties()
            .OrderByDescending(x => string.Equals(x.Name, nameof(exception.Message), StringComparison.Ordinal))
            .ThenByDescending(x => string.Equals(x.Name, nameof(exception.Source), StringComparison.Ordinal))
            .ThenBy(x => string.Equals(x.Name, nameof(exception.InnerException), StringComparison.Ordinal))
            .ThenBy(x => string.Equals(x.Name, nameof(AggregateException.InnerExceptions), StringComparison.Ordinal)))
        {
            var value = property.GetValue(exception, null);

            if (value is null)
            {
                if (settings.OmitNullValues)
                {
                    continue;
                }
                else
                {
                    value = string.Empty;
                }
            }

            stringBuilder.AppendValue(property.Name, value, settings);
        }

        return stringBuilder.ToString().TrimEnd('\r', '\n');
    }

    private static void AppendCollection(
        this StringBuilder stringBuilder,
        string propertyName,
        IEnumerable collection,
        ExceptionOptions options)
    {
        stringBuilder.AppendLine($"{options.Indent}{propertyName}:");

        var i = 0;

        foreach (var item in collection)
        {
            var innerPropertyName = $"[{i}]";

            if (item is Exception exception)
            {
                stringBuilder.AppendException(innerPropertyName, exception, options);
            }
            else
            {
                stringBuilder.AppendValue(innerPropertyName, item, options);
            }

            ++i;
        }
    }

    private static void AppendException(
        this StringBuilder stringBuilder,
        string propertyName,
        Exception exception,
        ExceptionOptions options)
    {
        stringBuilder.AppendLine($"{options.Indent}{propertyName} =>");

        var innerExceptionString = exception.ToFullMessageDetail(x => x.IndentLevel = options.IndentLevel + 2);

        stringBuilder.AppendLine(innerExceptionString);
    }

    private static void AppendValue(
        this StringBuilder stringBuilder,
        string propertyName,
        object? value,
        ExceptionOptions options)
    {
        if (value is not null)
        {
            if (value is DictionaryEntry dictionaryEntry)
            {
                stringBuilder.AppendLine($"{options.Indent}{propertyName} = {dictionaryEntry.Key} : {dictionaryEntry.Value}");
            }
            else if (value is Exception exception)
            {
                stringBuilder.AppendException(propertyName, exception, options);
            }
            else if (value is string)
            {
                stringBuilder.AppendLine($"{options.Indent}{propertyName}: {value.ToString()}");
            }
            else if (value is IEnumerable collection)
            {
                if (collection.GetEnumerator().MoveNext())
                {
                    stringBuilder.AppendCollection(propertyName, collection, options);
                }
            }
            else
            {
                stringBuilder.AppendLine($"{options.Indent}{propertyName}: {value.ToString().ToSerialize(x => x.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)}");
            }
        }
    }
}

/// <summary>
/// Configuración de las excepciones.
/// </summary>
public class ExceptionOptions
{
    /// <summary>
    /// Configuración por defecto.
    /// </summary>
    public static readonly ExceptionOptions Default = new ()
    {
        IndentSize = 1,
        IndentLevel = 1,
        OmitNullValues = true,
    };

    /// <summary>
    /// Obtiene o establece el número de veces que se aplica la sangría especificada por la propiedad IndentSize. El valor predeterminado es 1.
    /// </summary>
    public int IndentLevel { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que indica si no muestra las propiedades nulas. El valor predeterminado es true.
    /// </summary>
    public bool OmitNullValues { get; set; }

    internal string Indent => new (' ', IndentLevel * IndentSize);

    /// <summary>
    /// Obtiene o establece el número de espacios en una sangría. El valor predeterminado es cuatro.
    /// </summary>
    internal int IndentSize { get; set; }
}