using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Wrapper class for URL parameters from an http request.</summary>
public class UrlParameters : IUrlParameters
{
    /// <summary>Gets the collection of http request URL parameters.</summary>
    public IQueryCollection? Collection { get; init; }

    /// <summary>Gets the values for a url parameter key.</summary>
    public StringValues GetValues(string key)
    {
        var values = StringValues.Empty;
        _ = Collection != null
         && Collection.Any()
         && Collection.TryGetValue(key, out values);
        return values;
    }
}
