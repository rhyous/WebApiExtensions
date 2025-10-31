using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface wrapper around <see cref="HttpContext"/>.</summary>
public interface IHttpContext
{
    /// <summary>The current <see cref="HttpContext"/> wrapped instance.</summary>
    HttpContext Instance { get; }

    /// <summary>Information about the underlying connection for this request.</summary>
    ConnectionInfo Connection { get; }

    /// <summary>The collection of HTTP features provided by the server and middleware available on this request.</summary>
    IFeatureCollection Features { get; }

    /// <summary>A key/value collection that can be used to share data within the scope of this request.</summary>
    IDictionary<object, object> Items { get; set; }

    /// <summary>A wrapped <see cref="HttpRequest"/> object as an <see cref="IHttpRequest"/> for this request.</summary>
    IHttpRequest Request { get; }

    /// <summary>Notifies when the connection underlying this request is aborted and thus request operations should be cancelled.</summary>
    CancellationToken RequestAborted { get; set; }

    /// <summary>The System.IServiceProvider that provides access to the request's service container.</summary>
    IServiceProvider RequestServices { get; set; }

    /// <summary>A wrapped <see cref="HttpResponse"/> object as an <see cref="IHttpResponse"/> for this request.</summary>
    IHttpResponse Response { get; }

    /// <summary>The object used to manage user session data for this request.</summary>
    ISession Session { get; set; }

    /// <summary>A unique identifier to represent this request in trace logs.</summary>
    string TraceIdentifier { get; set; }

    /// <summary>The user for this request.</summary>
    ClaimsPrincipal User { get; set; }

    /// <summary>An object that manages the establishment of WebSocket connections for this request.</summary>
    WebSocketManager WebSockets { get; }

    /// <summary>Aborts the connection underlying this request.</summary>
    void Abort();
}
