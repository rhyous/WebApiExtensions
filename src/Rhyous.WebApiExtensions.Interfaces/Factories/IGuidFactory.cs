using System;

namespace Rhyous.WebApiExtensions.Interfaces;

/// <summary>Interface for creating Guids.</summary>
public interface IGuidFactory
{
    /// <summary>Creates a new instance of a <see cref="Guid"/>.</summary>
    /// <returns>A new instance of a <see cref="Guid"/>.</returns>
    Guid Create();
}
