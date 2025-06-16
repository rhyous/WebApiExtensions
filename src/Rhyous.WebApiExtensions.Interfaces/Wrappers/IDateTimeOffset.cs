using System;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface wrapper for <see cref="DateTimeOffset"/>.</summary>
public interface IDateTimeOffset
{
    /// <summary>Gets the current date and time.</summary>
    DateTimeOffset Now { get; }
}