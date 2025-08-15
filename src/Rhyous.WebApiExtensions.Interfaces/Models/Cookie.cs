using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Models;

/// <summary>Represents a Cookie with a key, value, and options.</summary>
public class Cookie
{
    /// <summary>The Cookie name or key.</summary>
    public string Key { get; set; } = "";
    /// <summary>The Cookie value.</summary>
    public string Value { get; set; } = "";
    /// <summary>The Cookie options.</summary>
    public CookieOptions? Options { get; set; }
}
