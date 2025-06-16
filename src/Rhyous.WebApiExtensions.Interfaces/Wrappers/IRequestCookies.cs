using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Interface for the request cookies.</summary>
public interface IRequestCookies
{
    /// <summary>Gets the collection of request cookies.</summary>
    IRequestCookieCollection? Cookies { get; }
}
