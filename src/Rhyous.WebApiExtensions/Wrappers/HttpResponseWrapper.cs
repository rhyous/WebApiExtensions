using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Wrapper class for HttpResponse.</summary>
[ExcludeFromCodeCoverage]
public class HttpResponseWrapper : IHttpResponse
{
    private readonly HttpResponse _HttpResponse;

    /// <summary>Initializes a new instance of the <see cref="HttpResponseWrapper"/> class.</summary>
    /// <param name="httpResponse">The original HttpResponse object.</param>
    public HttpResponseWrapper(HttpResponse httpResponse)
    {
        _HttpResponse = httpResponse;
    }

    /// <summary>Gets the HttpContext associated with the response.</summary>
    public HttpContext HttpContext => _HttpResponse.HttpContext;

    /// <summary>Gets or sets the status code of the response.</summary>
    public int StatusCode { get => _HttpResponse.StatusCode; set => _HttpResponse.StatusCode = value; }

    /// <summary>Gets the headers of the response.</summary>
    public IHeaderDictionary Headers => _HttpResponse.Headers;

    /// <summary>Gets or sets the body stream of the response.</summary>
    public Stream Body { get => _HttpResponse.Body; set => _HttpResponse.Body = value; }

    /// <summary>Gets or sets the content length of the response.</summary>
    public long? ContentLength { get => _HttpResponse.ContentLength; set => _HttpResponse.ContentLength = value; }

    /// <summary>Gets or sets the content type of the response.</summary>
    public string? ContentType { get => _HttpResponse.ContentType; set => _HttpResponse.ContentType = value; }

    /// <summary>Gets the response cookies.</summary>
    public IResponseCookies Cookies => _HttpResponse.Cookies;

    /// <summary>Gets a value indicating whether the response has started.</summary>
    public bool HasStarted => _HttpResponse.HasStarted;

    /// <summary>Registers a callback to be invoked when the response is completed.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    /// <param name="state">The state object to be passed to the callback method.</param>
    public void OnCompleted(Func<object, Task> callback, object state) => _HttpResponse.OnCompleted(callback, state);

    /// <summary>Registers a callback to be invoked when the response is completed.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    public void OnCompleted(Func<Task> callback) => _HttpResponse.OnCompleted(callback);

    /// <summary>Registers a callback to be invoked when the response is starting.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    /// <param name="state">The state object to be passed to the callback method.</param>
    public void OnStarting(Func<object, Task> callback, object state) => _HttpResponse.OnStarting(callback, state);

    /// <summary>Registers a callback to be invoked when the response is starting.</summary>
    /// <param name="callback">The callback method to be invoked.</param>
    public void OnStarting(Func<Task> callback) => _HttpResponse.OnStarting(callback);

    /// <summary>Redirects the response to the specified location.</summary>
    /// <param name="location">The URL to redirect to.</param>
    public void Redirect(string location) => _HttpResponse.Redirect(location);

    /// <summary>Redirects the response to the specified location.</summary>
    /// <param name="location">The URL to redirect to.</param>
    /// <param name="permanent">A flag indicating whether the redirect is permanent.</param>
    public void Redirect(string location, bool permanent) => _HttpResponse.Redirect(location, permanent);

    /// <summary>Registers an object for disposal.</summary>
    /// <param name="disposable">The object to be disposed.</param>
    public void RegisterForDispose(IDisposable disposable) => _HttpResponse.RegisterForDispose(disposable);
}