using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>A class to wrap the request headers.</summary>

[ExcludeFromCodeCoverage]
public class RequestHeaders : IRequestHeaders
{
    /// <summary>Gets the collection of request headers.</summary>
    public IHeaderDictionary? Headers { get; init; }
}
