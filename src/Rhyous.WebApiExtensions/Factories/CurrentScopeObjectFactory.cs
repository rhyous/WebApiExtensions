using Microsoft.Extensions.DependencyInjection;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>A factory to create objects from the current scope.</summary>
/// <typeparam name="T">The type of object to create. Must be a class.</typeparam>
/// <remarks>
/// This is useful for resolving scoped services dynamically at runtime.
/// The generic T parameter is at the class level so the resolved type is not 'hidden'.
/// </remarks>
public class CurrentScopeObjectFactory<T> : ICurrentScopeObjectFactory<T>
    where T : class
{
    private readonly IServiceProvider _scope;

    /// <summary>The constructor.</summary>
    /// <param name="scope">An instance of <see cref="IServiceProvider"/> representing the current scope.</param>
    public CurrentScopeObjectFactory(IServiceProvider scope)
    {
        _scope = scope;
    }

    /// <summary>Resolves an instance of T from the current scope.</summary>
    /// <returns>An instance of T from the current scope.</returns>
    public T Resolve() => _scope.GetRequiredService<T>();

    /// <summary>Resolves an instance of a child type of T from the current scope.</summary>
    /// <param name="childType">The child type of T to resolve. Must be a subclass of T.</param>
    /// <returns>An instance of TChild from the current scope.</returns>
    public T Resolve(Type childType) => _scope.GetRequiredService(childType) as T ?? throw new ArgumentException(nameof(childType));

    /// <summary>Resolves an instance of a child type of T from the current scope.</summary>
    /// <typeparam name="TChild">The child type of T to resolve. Must be a subclass of T.</typeparam>
    /// <returns>An instance of TChild from the current scope.</returns>
    public T Resolve<TChild>() where TChild : T => _scope.GetRequiredService<TChild>();
}