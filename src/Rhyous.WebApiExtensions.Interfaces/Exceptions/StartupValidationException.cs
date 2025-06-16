namespace Rhyous.WebApiExtensions.Exceptions;

/// <summary>Exception thrown when a startup validation fails.</summary>
public class StartupValidationException : Exception
{
    /// <summary>The constructor.</summary>
    /// <param name="message">The exception message.</param>
    public StartupValidationException(string? message) : base(message)
    {
    }
}
