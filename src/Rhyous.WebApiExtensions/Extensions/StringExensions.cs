namespace Rhyous.WebApiExtensions;

/// <summary>Extensions for <see cref="string"/>.</summary>
public static class StringExensions
{

    /// <summary>Splits a URL into its components: Forwarded, Proto, Host, and Port.</summary>
    /// <param name="url">The url to split.</param>
    /// <returns>The Url parts.</returns>
    public static (string Forwarded, string Proto, string Host, int Port) GetUrlParts(this string url)
    {
        var urlparts = url.Split(Constants.UrlSeparator, StringSplitOptions.RemoveEmptyEntries);
        var hostParts = urlparts[1].Split(Constants.PortSeparator, StringSplitOptions.RemoveEmptyEntries);
        var forwarded = urlparts[1];
        var proto = urlparts[0].Trim(':');
        var host = hostParts[0];
        var port = hostParts.Length == 2 ? Convert.ToInt32(hostParts[1]) : -1;
        return new(forwarded, proto, host, port);
    }

#if NETSTANDARD2_0
    /// <summary>Splits a string into an array of substrings based on the specified separator character.</summary>
    /// <param name="s">The string.</param>
    /// <param name="separator">The separator character to split by.</param>
    /// <param name="options">The <see cref="StringSplitOptions"/>.</param>
    /// <returns>An array of split strings.</returns>
    /// <remarks>This is a shim for netstandard2.0.</remarks>
    internal static string[] Split(this string s, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        return s.Split([separator], options);
    }
#endif
}
