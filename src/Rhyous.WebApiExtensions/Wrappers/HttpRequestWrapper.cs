using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Represents a wrapper for the <see cref="HttpRequest"/> class.</summary>
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

    /// <inheritdoc />
    public HttpRequest Instance => _HttpRequest;

    /// <inheritdoc />
    public HttpContext HttpContext => _HttpRequest.HttpContext;

    /// <inheritdoc />
    public string Method
    {
        get => _HttpRequest.Method;
        set => _HttpRequest.Method = value;
    }

    /// <inheritdoc />
    public string Scheme
    {
        get => _HttpRequest.Scheme;
        set => _HttpRequest.Scheme = value;
    }

    /// <inheritdoc />
    public bool IsHttps
    {
        get => _HttpRequest.IsHttps;
        set => _HttpRequest.IsHttps = value;
    }

    /// <inheritdoc />
    public HostString Host
    {
        get => _HttpRequest.Host;
        set => _HttpRequest.Host = value;
    }

    /// <inheritdoc />
    public PathString PathBase
    {
        get => _HttpRequest.PathBase;
        set => _HttpRequest.PathBase = value;
    }

    /// <inheritdoc />
    public PathString Path
    {
        get => _HttpRequest.Path;
        set => _HttpRequest.Path = value;
    }

    /// <inheritdoc />
    public QueryString QueryString
    {
        get => _HttpRequest.QueryString;
        set => _HttpRequest.QueryString = value;
    }

    /// <inheritdoc />
    public IQueryCollection Query
    {
        get => _HttpRequest.Query;
        set => _HttpRequest.Query = value;
    }

    /// <inheritdoc />
    public string Protocol
    {
        get => _HttpRequest.Protocol;
        set => _HttpRequest.Protocol = value;
    }

    /// <inheritdoc />
    public IHeaderDictionary Headers => _HttpRequest.Headers;

    /// <inheritdoc />
    public IRequestCookieCollection Cookies
    {
        get => _HttpRequest.Cookies;
        set => _HttpRequest.Cookies = value;
    }

    /// <inheritdoc />
    public long? ContentLength
    {
        get => _HttpRequest.ContentLength;
        set => _HttpRequest.ContentLength = value;
    }

    /// <inheritdoc />
    public string? ContentType
    {
        get => _HttpRequest.ContentType;
        set => _HttpRequest.ContentType = value;
    }

    /// <inheritdoc />
    public Stream Body
    {
        get => _HttpRequest.Body;
        set => _HttpRequest.Body = value;
    }

    /// <inheritdoc />
    public bool HasFormContentType => _HttpRequest.HasFormContentType;

    /// <inheritdoc />
    public IFormCollection Form
    {
        get => _HttpRequest.Form;
        set => _HttpRequest.Form = value;
    }

    /// <inheritdoc />
    public Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        => _HttpRequest.ReadFormAsync(cancellationToken);

    /// <inheritdoc />
    public string GetDisplayUrl() => _HttpRequest.GetDisplayUrl();
}