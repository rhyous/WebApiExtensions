using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface for wrapping the response headers.</summary>
public interface IResponseHeaders
{
    /// <summary>Gets the headers of the response.</summary>
    IHeaderDictionary? Headers { get; }
}
