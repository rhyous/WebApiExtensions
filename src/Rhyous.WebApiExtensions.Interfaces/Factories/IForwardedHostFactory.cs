namespace Rhyous.WebApiExtensions.Interfaces.Factories;

/// <summary>A interface for the factory for creating the <see cref="IForwardedHost"/> from the Host header, which could be different from the
/// request host due to load balancers forwarding to this microservice.</summary>
public interface IForwardedHostFactory
{
    /// <summary>Creates an <see cref="IForwardedHost"/> instance.</summary>
    /// <returns>An <see cref="IForwardedHost"/> instance.</returns>
    IForwardedHost Create();
}