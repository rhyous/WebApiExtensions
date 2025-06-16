using Microsoft.Extensions.DependencyInjection;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Represents a module that registers services for a microservice.</summary>
public interface IDependencyInjectionModule
{
    /// <summary>Registers the services for this microservice.</summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    void Register(IServiceCollection services);
}
