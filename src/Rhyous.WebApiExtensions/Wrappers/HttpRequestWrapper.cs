using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Represents a wrapper for the HttpRequest class.</summary>
[ExcludeFromCodeCoverage]
public class HttpRequestWrapper : IHttpRequest
{
    private readonly HttpRequest _HttpRequest;

    /// <summary>The constuctor.</summary>
    /// <param name="httpRequest">An instance of an <see cref="HttpRequest"/></param>
    public HttpRequestWrapper(HttpRequest httpRequest)
    {
        _HttpRequest = httpRequest!;
    }

    /// <summary>Gets the HttpContext associated with the HttpRequest.</summary>
    public HttpContext HttpContext => _HttpRequest.HttpContext;

    /// <summary>Gets or sets the HTTP method of the request.</summary>
    public string Method
    {
        get => _HttpRequest.Method;
        set => _HttpRequest.Method = value;
    }

    /// <summary>Gets or sets the scheme of the request.</summary>
    public string Scheme
    {
        get => _HttpRequest.Scheme;
        set => _HttpRequest.Scheme = value;
    }

    /// <summary>Gets or sets a value indicating whether the request is using HTTPS.</summary>
    public bool IsHttps
    {
        get => _HttpRequest.IsHttps;
        set => _HttpRequest.IsHttps = value;
    }

    /// <summary>Gets or sets the host of the request.</summary>
    public HostString Host
    {
        get => _HttpRequest.Host;
        set => _HttpRequest.Host = value;
    }

    /// <summary>Gets or sets the base path of the request.</summary>
    public PathString PathBase
    {
        get => _HttpRequest.PathBase;
        set => _HttpRequest.PathBase = value;
    }

    /// <summary>Gets or sets the path of the request.</summary>
    public PathString Path
    {
        get => _HttpRequest.Path;
        set => _HttpRequest.Path = value;
    }

    /// <summary>Gets or sets the query string of the request.</summary>
    public QueryString QueryString
    {
        get => _HttpRequest.QueryString;
        set => _HttpRequest.QueryString = value;
    }

    /// <summary>Gets or sets the query collection of the request.</summary>
    public IQueryCollection Query
    {
        get => _HttpRequest.Query;
        set => _HttpRequest.Query = value;
    }

    /// <summary>Gets or sets the protocol of the request.</summary>
    public string Protocol
    {
        get => _HttpRequest.Protocol;
        set => _HttpRequest.Protocol = value;
    }

    /// <summary>Gets the headers of the request.</summary>
    public IHeaderDictionary Headers => _HttpRequest.Headers;

    /// <summary>Gets or sets the cookies of the request.</summary>
    public IRequestCookieCollection Cookies
    {
        get => _HttpRequest.Cookies;
        set => _HttpRequest.Cookies = value;
    }

    /// <summary>Gets or sets the content length of the request.</summary>
    public long? ContentLength
    {
        get => _HttpRequest.ContentLength;
        set => _HttpRequest.ContentLength = value;
    }

    /// <summary>Gets or sets the content type of the request.</summary>
    public string? ContentType
    {
        get => _HttpRequest.ContentType;
        set => _HttpRequest.ContentType = value;
    }

    /// <summary>Gets or sets the body of the request.</summary>
    public Stream Body
    {
        get => _HttpRequest.Body;
        set => _HttpRequest.Body = value;
    }

    /// <summary>Gets a value indicating whether the request has form content type.</summary>
    public bool HasFormContentType => _HttpRequest.HasFormContentType;

    /// <summary>Gets or sets the form collection of the request.</summary>
    public IFormCollection Form
    {
        get => _HttpRequest.Form;
        set => _HttpRequest.Form = value;
    }

    /// <summary>Asynchronously reads the form collection of the request.</summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        => _HttpRequest.ReadFormAsync(cancellationToken);

    /// <summary>Gets the display URL of the request.</summary>
    /// <returns>The display URL of the request.</returns>
    public string GetDisplayUrl() => _HttpRequest.GetDisplayUrl();
}