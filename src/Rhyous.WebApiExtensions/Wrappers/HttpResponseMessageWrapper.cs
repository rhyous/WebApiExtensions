using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>A class wrapping an <see cref="HttpResponseMessage"/>.</summary>
[ExcludeFromCodeCoverage]
public class HttpResponseMessageWrapper : IHttpResponseMessage
{
    private readonly HttpResponseMessage _responseMessage;

    /// <summary>The constructor.</summary>
    /// <param name="responseMessage">The real <see cref="HttpResponseMessage"/>.</param>
    public HttpResponseMessageWrapper(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

    /// <summary>Gets the content of the HTTP response message.</summary>
    public HttpContent Content => _responseMessage.Content;

    /// <summary>Gets the headers of the HTTP response message.</summary>
    public HttpResponseHeaders Headers => _responseMessage.Headers;

    /// <summary>Gets the status code of the HTTP response message.</summary>
    public HttpStatusCode StatusCode => _responseMessage.StatusCode;
}
