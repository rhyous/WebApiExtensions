using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface for wrapping an <see cref="HttpResponseMessage"/>.</summary>
public interface IHttpResponseMessage
{
    /// <summary>The content of the HTTP response message.</summary>
    HttpContent Content { get; }
    /// <summary>The headers of the HTTP response message.</summary>
    HttpResponseHeaders Headers { get; }
    /// <summary>The status code of the HTTP response message.</summary>
    HttpStatusCode StatusCode { get; }
}
