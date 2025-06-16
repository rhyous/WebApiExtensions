using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface to wrap the request headers.</summary>
public interface IRequestHeaders
{
    /// <summary>Gets the collection of request headers.</summary>
    IHeaderDictionary? Headers { get; }
}
