using Rhyous.WebApiExtensions.Interfaces;

namespace Rhyous.WebApiExtensions.Configuration;

/// <summary>Configuration for the host.</summary>
public class HostSettings : IHostSettings
{
    /// <summary>The name of the configuration section for the host. This is settable so that you can change it to avoid conflicts.</summary>
    public static string Name { get; set; } = nameof(HostSettings);

    /// <inheritdoc />
    public string AppPath { get; set; } = "";

    /// <inheritdoc />
    /// <value>Default: X1, so X1-Forwarded-Host can be used.</value>
    public string AltXForwardedHostPrefix { get; set; } = "X1";

    /// <inheritdoc />
    public string AltXForwardedHost => $"{AltXForwardedHostPrefix}{Constants.ForwardedHost}";

    /// <inheritdoc />
    public string AltXForwardedProto => $"{AltXForwardedHostPrefix}{Constants.ForwardedProto}";
}