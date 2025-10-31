using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Wrapper class for <see cref="HttpResponse"/>.</summary>
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

    /// <inheritdoc />
    public HttpResponse Instance => _HttpResponse;

    /// <inheritdoc />
    public HttpContext HttpContext => _HttpResponse.HttpContext;

    /// <inheritdoc />
    public int StatusCode { get => _HttpResponse.StatusCode; set => _HttpResponse.StatusCode = value; }

    /// <inheritdoc />
    public IHeaderDictionary Headers => _HttpResponse.Headers;

    /// <inheritdoc />
    public Stream Body { get => _HttpResponse.Body; set => _HttpResponse.Body = value; }

    /// <inheritdoc />
    public long? ContentLength { get => _HttpResponse.ContentLength; set => _HttpResponse.ContentLength = value; }

    /// <inheritdoc />
    public string? ContentType { get => _HttpResponse.ContentType; set => _HttpResponse.ContentType = value; }

    /// <inheritdoc />
    public IResponseCookies Cookies => _HttpResponse.Cookies;

    /// <inheritdoc />
    public bool HasStarted => _HttpResponse.HasStarted;

    /// <inheritdoc />
    public void OnCompleted(Func<object, Task> callback, object state) => _HttpResponse.OnCompleted(callback, state);

    /// <inheritdoc />
    public void OnCompleted(Func<Task> callback) => _HttpResponse.OnCompleted(callback);

    /// <inheritdoc />
    public void OnStarting(Func<object, Task> callback, object state) => _HttpResponse.OnStarting(callback, state);

    /// <inheritdoc />
    public void OnStarting(Func<Task> callback) => _HttpResponse.OnStarting(callback);

    /// <inheritdoc />
    public void Redirect(string location) => _HttpResponse.Redirect(location);

    /// <inheritdoc />
    public void Redirect(string location, bool permanent) => _HttpResponse.Redirect(location, permanent);

    /// <inheritdoc />
    public void RegisterForDispose(IDisposable disposable) => _HttpResponse.RegisterForDispose(disposable);
}