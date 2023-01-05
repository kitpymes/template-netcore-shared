// -----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Kitpymes.Core.Shared;

/*
    Clase de extensión DateTimeExtensions
    Contiene las extensiones de la clase DateTime
*/

/// <summary>
/// Clase de extensión <c>DateTimeExtensions</c>.
/// Contiene las extensiones de la clase DateTime.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la clase DateTime.</para>
/// </remarks>
public static class DateTimeExtensions
{
    /// <summary>
    /// Verifica si la fecha es fin de semana.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsWeekend(this DateTime input)
    => input.DayOfWeek == DayOfWeek.Sunday || input.DayOfWeek == DayOfWeek.Saturday;

    /// <summary>
    /// Obtiene la edad de una fecha de nacimiento.
    /// </summary>
    /// <param name="input">Fecha de nacimiento.</param>
    /// <returns>int.</returns>
    public static int ToAge(this DateTime input)
    {
        var result = DateTime.Today.Year - input.Year;

        if (DateTime.Today.Month < input.Month
            || (DateTime.Today.Month == input.Month && DateTime.Today.Day < input.Day))
        {
            result--;
        }

        return result;
    }

    /// <summary>
    /// Verifica si la fecha es el último día del mes.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <returns>true | false.</returns>
    public static bool ToIsLastDayOfTheMonth(this DateTime input)
    => input == new DateTime(input.Year, input.Month, 1).AddMonths(1).AddDays(-1);

    /// <summary>
    /// Obtiene el último día del mes.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToEndOfTheMonth(this DateTime input)
    => new DateTime(input.Year, input.Month, 1).AddMonths(1).AddDays(-1);

    /// <summary>
    /// Obtiene el primer día de la semana.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToStartOfWeek(this DateTime input)
    {
        int dayOfWeek = (int)input.DayOfWeek;

        var days = dayOfWeek == 0 ? 7 : dayOfWeek;

        var result = input.AddDays(1 - days);

        return result;
    }

    /// <summary>
    /// Obtiene la fecha del día de mañana.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToYesterday(this DateTime input)
    => input.AddDays(-1);

    /// <summary>
    /// Obtiene la fecha del día de ayer.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <returns>DateTimer.</returns>
    public static DateTime ToTomorrow(this DateTime input)
    => input.AddDays(1);

    /// <summary>
    /// Agrega la hora a una fecha.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <param name="time">Hora a agregar.</param>
    /// <returns>DateTime | ApplicationException: si el parámetro time es nulo o vacio.</returns>
    public static DateTime ToSetTime(this DateTime input, string time)
    {
        time.ThrowIfNullOrEmpty();

        var parts = time.Split(':');

        if (parts.Length < 3)
        {
            return input;
        }

        if (!int.TryParse(parts[0], out var hours))
        {
            return input;
        }

        if (!int.TryParse(parts[1], out var minutes))
        {
            return input;
        }

        if (!int.TryParse(parts[2], out var seconds))
        {
            return input;
        }

        var result = ToSetTime(input, hours, minutes, seconds);

        return result;
    }

    /// <summary>
    /// Agrega la hora a una fecha.
    /// </summary>
    /// <param name="input">Fecha a verificar.</param>
    /// <param name="hours">Hora a agregar.</param>
    /// <param name="minutes">Minutos a agregar.</param>
    /// <param name="seconds">Segundos a agregar.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToSetTime(this DateTime input, int hours, int minutes, int seconds)
    {
        if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
        {
            return input;
        }

        var timeSpan = new TimeSpan(hours, minutes, seconds);

        var result = input.Date + timeSpan;

        return result;
    }
}
