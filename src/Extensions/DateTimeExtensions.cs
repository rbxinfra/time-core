namespace Roblox.Time;

using System;

/// <summary>
/// Extension methods for <see cref="DateTime" />
/// </summary>
public static class DateTimeExtensions
{
    private const int _TicksPerNanosecond = 100;
    private static readonly DateTime _UnixEpochStartTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Converts the <see cref="DateTime" /> to a Unix Epoch time in seconds
    /// </summary>
    /// <param name="startDate">The start <see cref="DateTime" /></param>
    /// <returns>Unix Epoch time in seconds</returns>
    public static long ToUnixEpochTimeInMilliseconds(this DateTime startDate) 
        => (long)Math.Round((startDate.ToUniversalTime() - _UnixEpochStartTime).TotalMilliseconds);

    /// <summary>
    /// Converts the <see cref="DateTime" /> to a Unix Epoch time in nanoseconds
    /// </summary>
    /// <param name="startDate">The start <see cref="DateTime" /></param>
    /// <returns>Unix Epoch time in nanoseconds</returns>
    public static long ToUnixEpochTimeInNanoseconds(this DateTime startDate)
        => (startDate.ToUniversalTime() - _UnixEpochStartTime).Ticks * _TicksPerNanosecond;

    /// <summary>
    /// Converts a Unix time string to a <see cref="DateTime" />
    /// </summary>
    /// <param name="unixTimeString">The Unix time string</param>
    /// <returns>The <see cref="DateTime" /> representation of the Unix time</returns>
    public static DateTime ToMillisecondsDateTime(this string unixTimeString) 
        => long.Parse(unixTimeString).ToMillisecondsDateTime();

    /// <summary>
    /// Converts a Unix time in milliseconds to a <see cref="DateTime" />
    /// </summary>
    /// <param name="unixTimeLong">The Unix time in milliseconds</param>
    /// <returns>The <see cref="DateTime" /> representation of the Unix time</returns>
    public static DateTime ToMillisecondsDateTime(this long unixTimeLong) 
        => _UnixEpochStartTime + TimeSpan.FromMilliseconds(unixTimeLong);

    /// <summary>
    /// Converts a Unix time string to a <see cref="DateTime" />
    /// </summary>
    /// <param name="unixTimeString">The Unix time string</param>
    /// <returns>The <see cref="DateTime" /> representation of the Unix time</returns>
    public static DateTime ToNanosecondsDateTime(this string unixTimeString) 
        => long.Parse(unixTimeString).ToNanosecondsDateTime();

    /// <summary>
    /// Converts a Unix time in nanoseconds to a <see cref="DateTime" />
    /// </summary>
    /// <param name="unixTimeLong">The Unix time in nanoseconds</param>
    /// <returns>The <see cref="DateTime" /> representation of the Unix time</returns>
    public static DateTime ToNanosecondsDateTime(this long unixTimeLong) 
        => _UnixEpochStartTime + TimeSpan.FromTicks(unixTimeLong / _TicksPerNanosecond);
}
