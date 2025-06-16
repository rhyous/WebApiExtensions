namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Provides a wrapper around the environment variables.</summary>
public interface IEnvironment
{
    /// <summary>Gets the value of an environment variable.</summary>
    string? GetEnvironmentVariable(string variable);
}
