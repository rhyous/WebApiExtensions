using System.Diagnostics.CodeAnalysis;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Wrapper class for the <see cref="DateTimeOffset"/>.</summary>
[ExcludeFromCodeCoverage]
public class DateTimeOffsetWrapper : IDateTimeOffset
{
    /// <summary>Gets the current date and time.</summary>
    public DateTimeOffset Now => DateTimeOffset.Now;
}