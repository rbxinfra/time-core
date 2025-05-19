
namespace Roblox.Time;

using System;

using Newtonsoft.Json;

/// <summary>
/// Exception thrown when a non-UTC DateTime is provided to a UtcInstant
/// </summary>
/// <seealso cref="ArgumentException" />
public class NonUtcDateTimeArgumentException : ArgumentException
{
}

/// <summary>
/// Represents an instant in time, specified in UTC time. This is a useful alternative to the
/// DateTime class to ensure that all times are explicitly in UTC and treated consistently
/// </summary>
public class UtcInstant : IComparable<UtcInstant>
{
    private readonly DateTime _DateTime;

    /// <summary>
    /// The number of ticks that represent this instant
    /// </summary>
    public long Ticks => _DateTime.Ticks;

    /// <summary>
    /// Constructs a new UtcInstant from the DateTime provided
    /// </summary>
    /// <param name="dateTime">UTC Kind DateTime to be represented by the Instant</param>
    /// <exception cref="NonUtcDateTimeArgumentException">Thrown when dateTime is not a UTC DateTime</exception>
    public UtcInstant(DateTime dateTime)
    {
        _DateTime = dateTime;

        if (dateTime.Kind != DateTimeKind.Utc)
            throw new NonUtcDateTimeArgumentException();
    }

    /// <summary>
    /// Constructs a new UtcInstant from the <see cref="DateTimeOffset" /> provided.
    /// </summary>
    /// <param name="dateTimeOffset"></param>
    public UtcInstant(DateTimeOffset dateTimeOffset)
    {
        _DateTime = dateTimeOffset.UtcDateTime;
    }

    /// <summary>
    /// Constructs a new UtcInstant from the provided ticks
    /// </summary>
    /// <param name="ticks"></param>
    [JsonConstructor]
    public UtcInstant(long ticks)
    {
        _DateTime = new DateTime(ticks, DateTimeKind.Utc);
    }

    /// <summary>
    /// Converts the <see cref="UtcInstant" /> to a <see cref="DateTime" />
    /// </summary>
    /// <returns>The <see cref="DateTime" /> representation of the instant</returns>
    public DateTime ToDateTime() => _DateTime;

    /// <summary>
    /// Converts the <see cref="UtcInstant" /> to a <see cref="DateTimeOffset" />
    /// </summary>
    /// <returns>The <see cref="DateTimeOffset" /> representation of the instant</returns>
    public DateTimeOffset ToDateTimeOffset() => new(_DateTime);

    /// <summary>
    /// Compares this <see cref="UtcInstant" /> to another <see cref="UtcInstant" />
    /// </summary>
    /// <param name="other">The other <see cref="UtcInstant" /> to compare to</param>
    /// <returns>0 if the instants are equal, -1 if this instant is earlier, 1 if this instant is later</returns>
    public int CompareTo(UtcInstant other)
    {
        if (this == other)  return 0;
        if (other == null) return 1;

        return ToDateTime().CompareTo(other.ToDateTime());
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => _DateTime.ToString();

    /// <inheritdoc cref="object.GetHashCode"/>
    public override int GetHashCode() => _DateTime.GetHashCode();

    /// <inheritdoc cref="object.Equals(object)"/>
    /// 
    public override bool Equals(object obj)
    {
        var instant = obj as UtcInstant;
        if (instant == null) return false;

        return Ticks == instant.Ticks;
    }

    /// <summary>
    /// Implicit conversion from <see cref="UtcInstant" /> to <see cref="DateTime" />
    /// </summary>
    /// <param name="utcInstant">The <see cref="UtcInstant" /> to convert</param>
    /// <returns>The <see cref="DateTime" /> representation of the instant</returns>
    public static implicit operator DateTime(UtcInstant utcInstant) => utcInstant._DateTime;

    /// <summary>
    /// Implicit conversion from <see cref="UtcInstant" /> to <see cref="DateTimeOffset" />
    /// </summary>
    /// <param name="utcInstant">The <see cref="UtcInstant" /> to convert</param>
    /// <returns>The <see cref="DateTimeOffset" /> representation of the instant</returns>
    public static implicit operator DateTimeOffset(UtcInstant utcInstant) => new(utcInstant._DateTime);

    /// <summary>
    /// Equality operator for <see cref="UtcInstant" />
    /// </summary>
    /// <param name="left">The left <see cref="UtcInstant" /></param>
    /// <param name="right">The right <see cref="UtcInstant" /></param>
    /// <returns>True if the instants are equal, false otherwise</returns>
    public static bool operator ==(UtcInstant left, UtcInstant right)
    {
        if (left == right) return true;
        if (left == null || right == null) return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator for <see cref="UtcInstant" />
    /// </summary>
    /// <param name="left">The left <see cref="UtcInstant" /></param>
    /// <param name="right">The right <see cref="UtcInstant" /></param>
    /// <returns>True if the instants are not equal, false otherwise</returns>
    public static bool operator !=(UtcInstant left, UtcInstant right) => !(left == right);

    /// <summary>
    /// Method to coerce a <see cref="DateTime"/> into a <see cref="UtcInstant"/>, even if provided with a non-UTC time. Note that this
    /// may produce an incorrect result if an Unspecified type of <see cref="DateTime"/> is provided - that is unavoidable
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to coerce</param>
    /// <param name="onDateTimeCoerced">an <see cref="Action{T}"/> that will be called if a <see cref="DateTime"/> coercion occurs, passing in the <see cref="DateTimeKind"/> of the <see cref="DateTime"/> provided</param>
    /// <returns>The UtcInstant representation of the <see cref="DateTime"/></returns>
    public static UtcInstant CoerceFrom(DateTime dateTime, Action<DateTimeKind> onDateTimeCoerced = null)
    {
        switch (dateTime.Kind)
        {
            case DateTimeKind.Utc:
                return new UtcInstant(dateTime);
            default:
                onDateTimeCoerced?.Invoke(dateTime.Kind);
                return new UtcInstant(dateTime.ToUniversalTime());
        }
    }
}
