
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions.Factories;

/// <summary>A factory that creates an <see cref="IRequestUrl"/> instance.</summary>
public class RequestUrlFactory : IRequestUrlFactory
{
    private readonly IForwardedHost _forwardedHost;
    private readonly IHostSettings _hostConfiguration;
    private readonly IHttpRequest _httpRequest;
    private readonly IRequestHeaders _requestHeaders;

    /// <summary>The constructor.</summary>
    /// <param name="forwardedHost">An instance of <see cref="IForwardedHost"/>.</param>
    /// <param name="hostConfiguration">>An instance of <see cref="IHostSettings"/>.</param>
    /// <param name="httpRequest">An instance of <see cref="IHttpRequest"/>.</param>
    /// <param name="requestHeaders">An instance of <see cref="IRequestHeaders"/>.</param>
    public RequestUrlFactory(IForwardedHost forwardedHost,
                             IHostSettings hostConfiguration,
                             IHttpRequest httpRequest,
                             IRequestHeaders requestHeaders)
    {
        _forwardedHost = forwardedHost;
        _hostConfiguration = hostConfiguration;
        _httpRequest = httpRequest;
        _requestHeaders = requestHeaders;
    }

    /// <summary>Creates an <see cref="IRequestUrl"/> instance.</summary>
    /// <returns>An <see cref="IRequestUrl"/> instance.</returns>
    /// <remarks>
    /// App Root when running this code on a local dev box is the same as the root.
    /// App Root when running deployed can vary depending on configuration, routing, url-rewriting, etc.
    /// https://www.domain.tld/path1/path2 could be rewritten to https://www.some-backend-service.tld/api,
    /// </remarks>
    public IRequestUrl Create()
    {
        var uri = new Uri(_httpRequest.GetDisplayUrl());
        var replacedUri = uri.Replace(_forwardedHost.Proto, _forwardedHost.Host, _forwardedHost.Port);
        var root = replacedUri.GetLeftPart(UriPartial.Authority);
        var appRoot = GetAppRoot(replacedUri);
        return new RequestUrl(_httpRequest.GetDisplayUrl(), replacedUri.ToString(), root, appRoot);
    }

    /// <summary>Constructs the application root URI up to the specified segment.</summary>
    /// <param name="uri">The input URI.</param>
    /// <returns>The application root URI as a string.</returns>
    internal string GetAppRoot(Uri uri)
    {
        var rootUrl = uri.GetLeftPart(UriPartial.Authority);
        UriBuilder appRootUriBuilder = new UriBuilder(rootUrl) { Port = -1 };
        if (IsLocalApiTest(rootUrl))
        {
            return appRootUriBuilder.Uri.ToString();
        }
        appRootUriBuilder.Path = _hostConfiguration.AppPath;
        return appRootUriBuilder.ToString();
    }

    /// <summary>Checks if the call is a Local Api Test or not.</summary>
    /// <param name="rootUrl"></param>
    /// <returns>True if a local Api tests, false otherwise.</returns>
    internal bool IsLocalApiTest(string rootUrl)
    {
        return rootUrl.StartsWith("https://localhost")
            || (_requestHeaders.Headers != null
                && _requestHeaders.Headers.Keys.Contains(Constants.LocalApiTest, StringComparer.OrdinalIgnoreCase));
    }
}
