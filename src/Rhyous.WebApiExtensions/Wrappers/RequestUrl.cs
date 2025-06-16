using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>A model for the request URL.</summary>
public class RequestUrl(string url, string forwardedUrl, string rootUrl, string appRootUrl) : IRequestUrl
{
    /// <inheritdoc/>
    public string Url { get; } = url;

    /// <inheritdoc/>
    public string ForwardedUrl { get; } = forwardedUrl;

    /// <inheritdoc/>
    public string RootUrl { get; } = rootUrl;

    /// <inheritdoc/>
    public string AppRootUrl { get; } = appRootUrl;
}
