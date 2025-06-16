namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interface model for the original host URL. If the host changed due a load balancing, this should be the host before the change.</summary>

public interface IForwardedHost
{
    /// <summary>
    /// The original host URL. If the host changed due a load balancing, this should be the host before the change.
    /// Expected syntax: "http://domain.tld" or "https://domain.tld:8080".
    /// </summary>
    string Forwarded { get; }

    /// <summary>The original protocol, such as http or https.</summary>
    string Proto { get; set; }

    /// <summary>The original host URL, without the port. If the host changed due a load balancing, this should be the host before the change.</summary>
    string Host { get; }

    /// <summary>The original port. If the host changed due a load balancing, this should be the port before the change.</summary>
    /// <remarks>If the port is -1, it means the Host didn't include the port. For most https calls, it will be -1 as the Host usually doesn't include :443.</remarks>
    int Port { get; }
}