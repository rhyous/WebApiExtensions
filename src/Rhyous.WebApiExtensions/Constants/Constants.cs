namespace Rhyous.WebApiExtensions;

internal static class Constants
{
    public const string Host = nameof(Host);
    public const string LocalApiTest = nameof(LocalApiTest);

    // Used when XForwardedHeaders is overwritten by multiple layer 7 middleware
    // Example: Fastly, then nginx, then K8s ingress
    public const string ForwardedHost = "-Forwarded-Host";
    public const string ForwardedProto = "-Forwarded-Proto";
    // The regular X-Forwarded-Host and X-Forwarded-Proto headers
    public const string XForwardedHost = "X-Forwarded-Host";
    public const string XForwardedProto = "X-Forwarded-Proto";


    public const char PortSeparator = ':';
    public const string ProtoSeparator = "://";
    public const char UrlSeparator = '/';
}
