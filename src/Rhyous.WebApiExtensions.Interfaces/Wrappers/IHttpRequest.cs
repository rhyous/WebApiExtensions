using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface for wrapping an <see cref="HttpContext"/>.</summary>
public interface IHttpRequest
{
    /// <summary>Represents the current HTTP context.</summary>
    HttpContext HttpContext { get; }

    /// <summary>Represents the HTTP method of the request.</summary>
    string Method { get; set; }

    /// <summary>Represents the scheme (HTTP or HTTPS) of the request.</summary>
    string Scheme { get; set; }

    /// <summary>Indicates whether the request is made over HTTPS.</summary>
    bool IsHttps { get; set; }

    /// <summary>Represents the host and port of the request.</summary>
    HostString Host { get; set; }

    /// <summary>Represents the base path of the request.</summary>
    PathString PathBase { get; set; }

    /// <summary>Represents the path of the request.</summary>
    PathString Path { get; set; }

    /// <summary>Represents the query string of the request.</summary>
    QueryString QueryString { get; set; }

    /// <summary>Represents the query parameters of the request.</summary>
    IQueryCollection Query { get; set; }

    /// <summary>Represents the protocol (HTTP/1.1, HTTP/2, etc.) of the request.</summary>
    string Protocol { get; set; }

    /// <summary>Represents the headers of the request.</summary>
    IHeaderDictionary Headers { get; }

    /// <summary>Represents the cookies of the request.</summary>
    IRequestCookieCollection Cookies { get; set; }

    /// <summary>Represents the length of the request body.</summary>
    long? ContentLength { get; set; }

    /// <summary>Represents the content type of the request body.</summary>
    string? ContentType { get; set; }

    /// <summary>Represents the request body as a stream.</summary>
    Stream Body { get; set; }

    /// <summary>Indicates whether the request has a form content type.</summary>
    bool HasFormContentType { get; }

    /// <summary>Represents the form data of the request.</summary>
    IFormCollection Form { get; set; }

    /// <summary>Asynchronously reads the form data of the request.</summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The form data of the request.</returns>
    Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets the display URL of the request.</summary>
    string GetDisplayUrl();
}
