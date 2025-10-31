using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface for wrapping an <see cref="HttpRequest"/>.</summary>
public interface IHttpRequest
{
    /// <summary>The current <see cref="HttpRequest"/> wrapped instance.</summary>
    HttpRequest Instance { get; }

    /// <summary>The current <see cref="Microsoft.AspNetCore.Http.HttpContext"/> associated with this <see cref="HttpRequest"/>.</summary>
    HttpContext HttpContext { get; }

    /// <summary>The HTTP method of the request.</summary>
    string Method { get; set; }

    /// <summary>The scheme (HTTP or HTTPS) of the request.</summary>
    string Scheme { get; set; }

    /// <summary>Indicates whether the request is made over HTTPS.</summary>
    bool IsHttps { get; set; }

    /// <summary>The host and port of the request.</summary>
    HostString Host { get; set; }

    /// <summary>The base path of the request.</summary>
    PathString PathBase { get; set; }

    /// <summary>The path of the request.</summary>
    PathString Path { get; set; }

    /// <summary>The query string of the request.</summary>
    QueryString QueryString { get; set; }

    /// <summary>The query parameters of the request.</summary>
    IQueryCollection Query { get; set; }

    /// <summary>The protocol (HTTP/1.1, HTTP/2, etc.) of the request.</summary>
    string Protocol { get; set; }

    /// <summary>The headers of the request.</summary>
    IHeaderDictionary Headers { get; }

    /// <summary>The cookies of the request.</summary>
    IRequestCookieCollection Cookies { get; set; }

    /// <summary>The length of the request body.</summary>
    long? ContentLength { get; set; }

    /// <summary>The content type of the request body.</summary>
    string? ContentType { get; set; }

    /// <summary>The request body as a stream.</summary>
    Stream Body { get; set; }

    /// <summary>Indicates whether the request has a form content type.</summary>
    bool HasFormContentType { get; }

    /// <summary>The form data of the request.</summary>
    IFormCollection Form { get; set; }

    /// <summary>Asynchronously reads the form data of the request.</summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The form data of the request.</returns>
    Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets the display URL of the request.</summary>
    string GetDisplayUrl();
}
