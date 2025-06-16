using Microsoft.Extensions.DependencyInjection;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Extensions method for <see cref="IServiceCollection"/>.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Registers a module with the services collection.</summary>
    /// <typeparam name="T">The type of module to register, must implement <see cref="IDependencyInjectionModule"/>.</typeparam>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    /// <param name="constructorParams">A list of constructor parameters.</param>
    public static void RegisterModule<T>(this IServiceCollection services, params object[] constructorParams)
        where T : IDependencyInjectionModule
    {
        var module = Activator.CreateInstance(typeof(T), constructorParams) as IDependencyInjectionModule;
        module!.Register(services);
    }
}
