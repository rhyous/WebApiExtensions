using Rhyous.WebApiExtensions.Models;

namespace Rhyous.WebApiExtensions.Interfaces;


/// <summary>Validates a startup requirement.</summary>
public interface IStartupValidator
{
    /// <summary>The name of the startup validator.</summary>
    string Name { get; }

    /// <summary>Validates a startup requirement.</summary>
    /// <returns>One or more <see cref="StartupValidationResult"/>s.</returns>
    Task<IEnumerable<StartupValidationResult>> ValidateAsync();
}