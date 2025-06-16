using Rhyous.WebApiExtensions.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Rhyous.WebApiExtensions;

/// <summary>The constructor.</summary>
/// <param name="url">The url of the referrer.</param>
[ExcludeFromCodeCoverage]
public class Referer(string url) : IReferer
{
    /// <inheritdoc/>
    public string Url { get; } = url;
}
