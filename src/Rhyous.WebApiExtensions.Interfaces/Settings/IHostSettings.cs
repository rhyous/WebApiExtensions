namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Interface for host configuration.</summary>
public interface IHostSettings
{
    /// <summary>The cookie domain.</summary>
    string AppPath { get; set; }

    /// <summary>The alternate X-Forwarded-Host header to use if the X-Forwarded-Host header is not set.</summary>
    /// <remarks>
    /// Sometimes X-Forwarded-Host (or X-Forwarded-Proto) aren't usable as they are replaced too many times
    /// due to multiple CDNs, loadbalancers, firewalls, etc. So this allows you to create your own.
    /// </remarks>
    string AltXForwardedHostPrefix { get; set; }

    /// <summary>The {prefix}-Forwarded-Host header.</summary>
    string AltXForwardedHost { get; }
    /// <summary>The {prefix}-Forwarded-Proto header.</summary>
    string AltXForwardedProto { get; }
}