using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface for wrapping the HttpResponse associated with the response.</summary>
public interface IHttpResponse
{
    /// <summary>Gets the HttpContext associated with the response.</summary>
    HttpContext HttpContext { get; }

    /// <summary>Gets or sets the status code of the response.</summary>
    int StatusCode { get; set; }

    /// <summary>Gets the headers of the response.</summary>
    IHeaderDictionary Headers { get; }

    /// <summary>Gets or sets the body stream of the response.</summary>
    Stream Body { get; set; }

    /// <summary>Gets or sets the content length of the response.</summary>
    long? ContentLength { get; set; }

    /// <summary>Gets or sets the content type of the response.</summary>
    string? ContentType { get; set; }

    /// <summary>Gets the response cookies.</summary>
    IResponseCookies Cookies { get; }

    /// <summary>Gets a value indicating whether the response has started.</summary>
    bool HasStarted { get; }

    /// <summary>Adds a callback to be invoked when the response is starting.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    /// <param name="state">The state object to be passed to the callback method.</param>
    void OnStarting(Func<object, Task> callback, object state);

    /// <summary>Adds a callback to be invoked when the response is starting.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    void OnStarting(Func<Task> callback);

    /// <summary>Adds a callback to be invoked when the response is completed.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    /// <param name="state">The state object to be passed to the callback method.</param>
    void OnCompleted(Func<object, Task> callback, object state);

    /// <summary>Registers an object to be disposed when the response is completed.</summary>
    /// <param name="disposable">The object to be disposed.</param>
    void RegisterForDispose(IDisposable disposable);

    /// <summary>Adds a callback to be invoked when the response is completed.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    void OnCompleted(Func<Task> callback);

    /// <summary>Redirects the response to the specified location.</summary>
    /// <param name="location">The URL to redirect to.</param>
    void Redirect(string location);

    /// <summary>Redirects the response to the specified location.</summary>
    /// <param name="location">The URL to redirect to.</param>
    /// <param name="permanent">A flag indicating whether the redirect is permanent.</param>
    void Redirect(string location, bool permanent);
}
