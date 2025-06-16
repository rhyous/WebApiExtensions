namespace Rhyous.WebApiExtensions;

/// <summary></summary>
public static class UriExtensions
{
    private static Dictionary<string, int> _portMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        { "http", 80 },
        { "https", 443 }
    };

    /// <summary>
    /// Replaces the host and port of the given Uri with the new host and port specified.
    /// If the new host string includes a port, it will be extracted and used.
    /// </summary>
    /// <param name="originalUri">The original Uri to modify.</param>
    /// <param name="proto">The original protocol, such as http or https.</param>
    /// <param name="newHost">The new host.</param>
    /// <param name="newPort">The new port or -1 if there is no port.</param>
    /// <returns>A new Uri with the host (and optionally port) replaced.</returns>
    public static Uri Replace(this Uri originalUri, string proto, string newHost, int newPort = -1)
    {
        return newPort == -1
             ? new UriBuilder(originalUri) { Scheme = proto, Host = newHost, Port = _portMap[proto] }.Uri
             : new UriBuilder(originalUri) { Scheme = proto, Host = newHost, Port = newPort }.Uri;
    }
}
