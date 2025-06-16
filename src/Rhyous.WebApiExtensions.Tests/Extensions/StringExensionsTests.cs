using Shouldly;

namespace Rhyous.WebApiExtensions.Tests.Extensions;

[TestClass]
public class StringExensionsTests
{
    #region GetUrlParts
    [TestMethod]
    public void StringExtensions_GetUrlParts_NoPort_ReturnsAForwardedHost()
    {
        // Arrange
        var proto = "https";
        var host = "www.somedomain.tld";
        var url = $"{proto}://{host}/some/path?a=b";

        // Act
        var result = url.GetUrlParts();

        // Assert
        result.Forwarded.ShouldBe(host, nameof(ForwardedHost.Forwarded));
        result.Host.ShouldBe(host, nameof(ForwardedHost.Host));
        result.Port.ShouldBe(-1, nameof(ForwardedHost.Port));
        result.Proto.ShouldBe(proto);
    }

    [TestMethod]
    public void StringExtensions_GetUrlParts_WithPort_ReturnsAForwardedHost()
    {
        // Arrange
        var proto = "https";
        var host = "www.somedomain.tld";
        var port = 5000;
        var url = $"{proto}://{host}:{port}/some/path?a=b";

        // Act
        var result = url.GetUrlParts();

        // Assert
        result.Forwarded.ShouldBe($"{host}:{port}", nameof(ForwardedHost.Forwarded));
        result.Host.ShouldBe(host, nameof(ForwardedHost.Host));
        result.Port.ShouldBe(port, nameof(ForwardedHost.Port));
        result.Proto.ShouldBe(proto);
    }
    #endregion
}
