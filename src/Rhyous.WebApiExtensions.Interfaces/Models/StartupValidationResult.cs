using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions.Models;

/// <summary>Represents the result of a startup validation process.</summary>
/// <param name="IsValid">A value indicating whether the startup validation is successful. </param>
/// <param name="Name">The name of the <see cref="IStartupValidator"/>.</param>
/// <param name="Message">The message associated with the startup validation result.</param>
public record StartupValidationResult(bool IsValid, string Name, string Message);
