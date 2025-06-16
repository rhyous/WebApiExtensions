using Microsoft.Extensions.Primitives;
using Rhyous.WebApiExtensions.Interfaces;
using Rhyous.WebApiExtensions.Interfaces.Factories;

namespace Rhyous.WebApiExtensions.Factories;

/// <summary>A model for storing the original host, from the Host header, which could be different from the
/// request host due to load balancers forwarding to this microservice.</summary>
public class ForwardedHostFactory : IForwardedHostFactory
{
    private readonly IHostSettings _hostConfiguration;
    private readonly IHttpRequest _httpRequest;
    private readonly IRequestHeaders _requestHeaders;

    /// <summary>The constructor.</summary>
    /// <param name="hostConfiguration">An instance of <see cref="IHostSettings"/>.</param>
    /// <param name="httpRequest">An instance of <see cref="IHttpRequest"/>.</param>
    /// <param name="requestHeaders">An instance of <see cref="IRequestHeaders"/>.</param>
    public ForwardedHostFactory(IHostSettings hostConfiguration,
                                IHttpRequest httpRequest,
                                IRequestHeaders requestHeaders)
    {
        _hostConfiguration = hostConfiguration;
        _httpRequest = httpRequest;
        _requestHeaders = requestHeaders;
    }

    /// <summary>Creates an <see cref="IForwardedHost"/> instance.</summary>
    /// <returns>An <see cref="IForwardedHost"/> instance.</returns>
    public IForwardedHost Create()
    {
        var urlParts = _httpRequest.GetDisplayUrl().GetUrlParts();
        if (_requestHeaders.Headers != null)
        {
            if (_requestHeaders.Headers.TryGetValue(_hostConfiguration.AltXForwardedHost, out StringValues forwardedHostValues)
                || _requestHeaders.Headers.TryGetValue(Constants.XForwardedHost, out forwardedHostValues))
            {
                // Update forwarded
                var forwarded = forwardedHostValues[0]!;
                if (forwarded.Contains(Constants.ProtoSeparator))
                {
                    forwarded = forwarded.Substring(forwarded.IndexOf(Constants.ProtoSeparator) + Constants.ProtoSeparator.Length);
                }
                urlParts.Forwarded = forwarded;
                // Update host
                var hostParts = forwarded.Split(Constants.PortSeparator, StringSplitOptions.RemoveEmptyEntries);
                urlParts.Host = hostParts[0];
                // Update port
                urlParts.Port = hostParts.Length == 2 ? Convert.ToInt32(hostParts[1]) : -1;
            }
            if (_requestHeaders.Headers.TryGetValue(_hostConfiguration.AltXForwardedProto, out StringValues protoValues)
                || _requestHeaders.Headers.TryGetValue(Constants.XForwardedProto, out protoValues))
            {
                urlParts.Proto = protoValues[0]!;
            }
        }
        return new ForwardedHost(urlParts.Forwarded, urlParts.Proto, urlParts.Host, urlParts.Port);
    }
}