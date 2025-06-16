namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>A factory that creates an <see cref="IRequestUrl"/> instance.</summary>
public interface IRequestUrlFactory
{

    /// <summary>Creates an <see cref="IRequestUrl"/> instance.</summary>
    /// <returns>An <see cref="IRequestUrl"/> instance.</returns>
    IRequestUrl Create();
}