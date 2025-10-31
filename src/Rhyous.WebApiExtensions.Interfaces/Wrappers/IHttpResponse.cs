using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface for wrapping the <see cref="HttpResponse"/> associated with the response.</summary>
public interface IHttpResponse
{
    /// <summary>The current <see cref="HttpResponse"/> wrapped instance.</summary>
    HttpResponse Instance { get; }

    /// <summary>The <see cref="Microsoft.AspNetCore.Http.HttpContext"/> associated with the <see cref="HttpResponse"/>.</summary>
    HttpContext HttpContext { get; }

    /// <summary>The status code of the response.</summary>
    int StatusCode { get; set; }

    /// <summary>The headers of the response.</summary>
    IHeaderDictionary Headers { get; }

    /// <summary>The body stream of the response.</summary>
    Stream Body { get; set; }

    /// <summary>The content length of the response.</summary>
    long? ContentLength { get; set; }

    /// <summary>The content type of the response.</summary>
    string? ContentType { get; set; }

    /// <summary>The response cookies.</summary>
    IResponseCookies Cookies { get; }

    /// <summary>A value indicating whether the response has started.</summary>
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
    void OnCompleted(Func<Task> callback);

    /// <summary>Adds a callback to be invoked when the response is completed.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    /// <param name="state">The state object to be passed to the callback method.</param>
    void OnCompleted(Func<object, Task> callback, object state);

    /// <summary>Registers an object to be disposed when the response is completed.</summary>
    /// <param name="disposable">The object to be disposed.</param>
    void RegisterForDispose(IDisposable disposable);

    /// <summary>Redirects the response to the specified location.</summary>
    /// <param name="location">The URL to redirect to.</param>
    void Redirect(string location);

    /// <summary>Redirects the response to the specified location.</summary>
    /// <param name="location">The URL to redirect to.</param>
    /// <param name="permanent">A flag indicating whether the redirect is permanent.</param>
    void Redirect(string location, bool permanent);
}
