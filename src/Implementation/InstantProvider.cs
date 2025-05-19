namespace Roblox.Time;

using System;

/// <summary>
/// An Implmenetation of IInstantProvider based on the local server time
/// </summary>
/// <seealso cref="IInstantProvider" />
public class InstantProvider : IInstantProvider
{
    /// <inheritdoc cref="IInstantProvider.GetCurrentUtcInstant"/>
    public UtcInstant GetCurrentUtcInstant() => new(DateTime.UtcNow);
}

