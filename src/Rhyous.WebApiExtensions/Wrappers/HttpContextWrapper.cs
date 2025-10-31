using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Encapsulates all HTTP-specific information about an individual HTTP request.</summary>
[ExcludeFromCodeCoverage]
public class HttpContextWrapper : IHttpContext
{
    private readonly HttpContext _httpContext;

    /// <summary>The constructor.</summary>
    /// <param name="httpContext">An instance of <see cref="HttpContext"/>.</param>
    public HttpContextWrapper(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    /// <summary>The current <see cref="HttpContext"/> wrapped instance.</summary>
    public HttpContext Instance => _httpContext;

    /// <inheritdoc/>
    public ConnectionInfo Connection => _httpContext.Connection;

    /// <inheritdoc/>
    public IFeatureCollection Features => _httpContext.Features;

    /// <inheritdoc/>
    public IDictionary<object, object> Items { get => _httpContext.Items; set => _httpContext.Items = value; }

    /// <inheritdoc/>
    public IHttpRequest Request => new HttpRequestWrapper(_httpContext.Request);

    /// <inheritdoc/>
    public CancellationToken RequestAborted { get => _httpContext.RequestAborted; set => _httpContext.RequestAborted = value; }

    /// <inheritdoc/>
    public IServiceProvider RequestServices { get => _httpContext.RequestServices; set => _httpContext.RequestServices = value; }

    /// <inheritdoc/>
    public IHttpResponse Response => new HttpResponseWrapper(_httpContext.Response);

    /// <inheritdoc/>
    public ISession Session { get => _httpContext.Session; set => _httpContext.Session = value; }

    /// <inheritdoc/>
    public string TraceIdentifier { get => _httpContext.TraceIdentifier; set => _httpContext.TraceIdentifier = value; }

    /// <inheritdoc/>
    public ClaimsPrincipal User { get => _httpContext.User; set => _httpContext.User = value; }

    /// <inheritdoc/>
    public WebSocketManager WebSockets => _httpContext.WebSockets;

    /// <inheritdoc/>
    public void Abort() => _httpContext.Abort();
}