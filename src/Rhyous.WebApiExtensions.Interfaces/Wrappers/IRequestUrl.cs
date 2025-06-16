namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Interface model for the request URL.</summary>
public interface IRequestUrl
{
    /// <summary>The URL.</summary>
    string Url { get; }

    /// <summary>The original request URL. If the host changed due to load balancing, this uses the original host name and the port if specified.</summary>
    string ForwardedUrl { get; }

    /// <summary>The root URL.</summary>
    string RootUrl { get; }

    /// <summary>The app root URL.</summary>
    string AppRootUrl { get; }
}
