using System.Diagnostics.CodeAnalysis;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>An interfce for a factory to create objects from the current scope.</summary>
/// <typeparam name="T">The type of object to create. Must be a class.</typeparam>
/// <remarks>
/// This is useful for resolving scoped services dynamically at runtime.
/// The generic T parameter is at the class level so the resolved type is not 'hidden'.
/// </remarks>
[SuppressMessage("Major Code Smell", "S3246:Generic type parameters should be co/contravariant when possible", Justification = "Resolving a child overload Resolve<TChild>() doesn't allow this.")]
public interface ICurrentScopeObjectFactory<T> where T : class
{
    /// <summary>Creates an instance of T from the current scope.</summary>
    /// <returns>An instance of T from the current scope.</returns>
    T Create();

    /// <summary>Creates an instance of T from the current scope.</summary>
    /// <param name="key">An key to resolve a keyed service. If your DI container does not support keyed services, this parameter is ignored. Null is a valid key different than a registration that is not keyed.</param>
    /// <returns>An instance of T from the current scope.</returns>
    T Create(object? key);

    /// <summary>Creates an instance of a child type of T from the current scope.</summary>
    /// <param name="childType">The child type of T to resolve. Must be a subclass of T.</param>
    /// <returns>An instance of TChild from the current scope.</returns>
    T Create(Type childType);

    /// <summary>Creates an instance of a child type of T from the current scope.</summary>
    /// <param name="childType">The child type of T to resolve. Must be a subclass of T.</param>
    /// <param name="key">An key to resolve a keyed service. If your DI container does not support keyed services, this parameter is ignored. Null is a valid key different than a registration that is not keyed.</param>
    /// <returns>An instance of TChild from the current scope.</returns>
    T Create(Type childType, object? key);

    /// <summary>Creates an instance of a child type of T from the current scope.</summary>
    /// <typeparam name="TChild">The child type of T to resolve. Must be a subclass of T.</typeparam>
    /// <returns>An instance of TChild from the current scope.</returns>
    T Create<TChild>() where TChild : T;

    /// <summary>Creates an instance of a child type of T from the current scope.</summary>
    /// <typeparam name="TChild">The child type of T to resolve. Must be a subclass of T.</typeparam>
    /// <param name="key">An key to resolve a keyed service. If your DI container does not support keyed services, this parameter is ignored. Null is a valid key different than a registration that is not keyed.</param>
    /// <returns>An instance of TChild from the current scope.</returns>
    T Create<TChild>(object? key) where TChild : T;
}