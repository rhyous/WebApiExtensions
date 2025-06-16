using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;


/// <summary>A model for the original host URL. If the host changed due a load balancing, this should be the host before the change.</summary>
/// <param name="forwarded">The original host the may or may not have a port. Expected syntax: "http://domain.tld" or "https://domain.tld:8080"</param>
/// <param name="proto">The original protocol, such as http or https.</param>
/// <param name="host">The host without the port.</param>
/// <param name="port">The port.</param>
public class ForwardedHost(string forwarded, string proto, string host, int port = -1) : IForwardedHost
{
    /// <summary>
    /// The original host URL. If the host changed due a load balancing, this should be the host before the change.
    /// Expected syntax: "http://domain.tld" or "https://domain.tld:8080".
    /// </summary>
    public string Forwarded { get; } = forwarded;

    /// <summary>The original protocol, such as http or https.</summary>
    public string Proto { get; set; } = proto;

    /// <summary>The original host URL, without the port. If the host changed due a load balancing, this should be the host before the change.</summary>
    public string Host { get; } = host;

    /// <summary>The original port. If the host changed due a load balancing, this should be the port before the change.</summary>
    public int Port { get; } = port;
}
