using System.Diagnostics.CodeAnalysis;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions.Wrappers;

/// <summary>Provides a wrapper around the environment variables.</summary>
[ExcludeFromCodeCoverage]
public class EnvironmentWrapper : IEnvironment
{
    /// <summary>Gets the value of an environment variable.</summary>
    public string? GetEnvironmentVariable(string variable) => Environment.GetEnvironmentVariable(variable);

}
