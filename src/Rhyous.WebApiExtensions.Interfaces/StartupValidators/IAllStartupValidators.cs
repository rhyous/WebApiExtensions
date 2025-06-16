
namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Validates all startup requirement.</summary>
public interface IAllStartupValidators
{
    /// <summary>Validates all startup requirement.</summary>
    /// <returns>A completed <see cref="Task"/>.</returns>
    Task ValidateAsync();
}