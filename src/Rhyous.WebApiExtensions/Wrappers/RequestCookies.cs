using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>A class to wrap the request cookies.</summary>
[ExcludeFromCodeCoverage]
public class RequestCookies : IRequestCookies
{
    /// <summary>Gets the collection of request cookies.</summary>
    public IRequestCookieCollection? Cookies { get; init; }
}
