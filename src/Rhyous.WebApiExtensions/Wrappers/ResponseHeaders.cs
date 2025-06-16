using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Wrapper class for Http response headers.</summary>
[ExcludeFromCodeCoverage]
public class ResponseHeaders : IResponseHeaders
{
    /// <summary>Initializes a new instance of the <see cref="ResponseHeaders"/> class.</summary>
    public IHeaderDictionary? Headers { get; init; }
}
