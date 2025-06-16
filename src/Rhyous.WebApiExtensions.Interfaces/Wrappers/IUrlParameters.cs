using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Interface for the UrlParameters.</summary>
public interface IUrlParameters
{
    /// <summary>Gets the collection of Url parameters from a request.</summary>
    IQueryCollection? Collection { get; init; }

    /// <summary>Gets the values for a url parameter key.</summary>
    StringValues GetValues(string key);
}