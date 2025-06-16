using System.Diagnostics.CodeAnalysis;
using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions;

/// <summary>Creates Guids.</summary>
[ExcludeFromCodeCoverage]
public class GuidFactory : IGuidFactory
{
    /// <summary>Creates a new instance of a <see cref="Guid"/>.</summary>
    /// <returns>A new instance of a <see cref="Guid"/>.</returns>
    public Guid Create()
    {
        return Guid.NewGuid();
    }
}
