using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

    /// <summary>Registers a configuration section as a singleton service.</summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <typeparam name="TConcrete"></typeparam>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    /// <param name="config">An instance of <see cref="IConfiguration"/> from which to get the section.</param>
    /// <param name="configSectionName">Optional. The name of the configuration section to bind to the concrete type. The default is the class name of the concrete type.</param>
    /// <param name="registerConcrete">Optional. If true, registers the concrete type as well as the interface. Default is false. It is not recommended to register the concrete type.</param>
    public static void RegisterConfiguration<TInterface, TConcrete>(this IServiceCollection services, IConfiguration config, string? configSectionName = null, bool registerConcrete = false)
        where TConcrete : class, TInterface
        where TInterface : class
    {
        configSectionName ??= typeof(TConcrete).Name;
        services.Configure<TConcrete>(config.GetSection(configSectionName));
        services.AddSingleton<TInterface>(c => c.GetService<IOptions<TConcrete>>()!.Value);
        if (registerConcrete)
            services.AddSingleton<TConcrete>(c => c.GetService<IOptions<TConcrete>>()!.Value);

    }
}
