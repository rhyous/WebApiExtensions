using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Rhyous.WebApiExtensions.Factories;
using Rhyous.WebApiExtensions.Interfaces;
using Shouldly;

namespace Rhyous.WebApiExtensions.Tests.Factories;

[TestClass]
public class ForwardedHostFactoryTests
{
    private MockRepository _mockRepository;

    private Mock<IHostSettings> _mockHostSettings;
    private Mock<IHttpRequest> _mockHttpRequest;
    private Mock<IRequestHeaders> _mockRequestHeaders;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _mockHostSettings = _mockRepository.Create<IHostSettings>();
        _mockHttpRequest = _mockRepository.Create<IHttpRequest>();
        _mockRequestHeaders = _mockRepository.Create<IRequestHeaders>();
    }

    private ForwardedHostFactory CreateFactory()
    {
        return new ForwardedHostFactory(
            _mockHostSettings.Object,
            _mockHttpRequest.Object,
            _mockRequestHeaders.Object);
    }

    #region Create
    [TestMethod]
    public void ForwardedHostFactory_Create_HostHeaderExistsWithPort_ReturnsForwardedHostWithPort()
    {
        // Arrange
        var factory = CreateFactory();
        var proto = "https";
        var host = "localhost";
        var port = 5000;
        var original = $"{host}:{port}";
        _mockHostSettings.Setup(m => m.AltXForwardedHost).Returns("X1-Forwarded-host");
        _mockHostSettings.Setup(m => m.AltXForwardedProto).Returns("X1-Forwarded-Proto");
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns("https://some.domain.tld:5111");
        var headersDict = new Dictionary<string, StringValues>
        {
            { Constants.XForwardedHost, original },
            { Constants.XForwardedProto, proto }
        };
        var headers = new HeaderDictionary(headersDict);
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        // Act
        var result = factory.Create();

        // Assert
        result.Forwarded.ShouldBe(original, nameof(ForwardedHost.Forwarded));
        result.Host.ShouldBe(host, nameof(ForwardedHost.Host));
        result.Port.ShouldBe(port, nameof(ForwardedHost.Port));
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    [DataRow("X1-Forwarded-Host", "X1-Forwarded-Proto")]
    [DataRow(Constants.XForwardedHost, Constants.XForwardedProto)]
    public void ForwardedHostFactory_Create_HostHeaderExistsWithOutPort_ReturnsForwardedHostWithOutPort(string forwardedHostKey, string forwardedProtoKey)
    {
        // Arrange
        var factory = CreateFactory();
        var proto = "https";
        var host = "localhost";
        var original = $"{host}";
        _mockHostSettings.Setup(m => m.AltXForwardedHost).Returns("X1-Forwarded-host");
        _mockHostSettings.Setup(m => m.AltXForwardedProto).Returns("X1-Forwarded-Proto");
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns($"https://{host}");
        var headersDict = new Dictionary<string, StringValues>
        {
            { forwardedHostKey, original },
            { forwardedProtoKey, proto }
        };
        var headers = new HeaderDictionary(headersDict);
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        // Act
        var result = factory.Create();

        // Assert
        result.Forwarded.ShouldBe(original, nameof(ForwardedHost.Forwarded));
        result.Proto.ShouldBe(proto, nameof(ForwardedHost.Proto));
        result.Host.ShouldBe(host, nameof(ForwardedHost.Host));
        result.Port.ShouldBe(-1, nameof(ForwardedHost.Port));
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void ForwardedHostFactory_Create_HostHeaderExistsContainsProtocol_ReturnsForwardedHostWithOutPort()
    {
        // Arrange
        var factory = CreateFactory();
        var proto = "https";
        var host = "localhost";
        var original = $"{proto}://{host}";
        _mockHostSettings.Setup(m => m.AltXForwardedHost).Returns("X1-Forwarded-host");
        _mockHostSettings.Setup(m => m.AltXForwardedProto).Returns("X1-Forwarded-Proto");
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns("https://some.domain.tld");
        var headersDict = new Dictionary<string, StringValues>
        {
            { Constants.XForwardedHost, original },
            { Constants.XForwardedProto, proto }
        };
        var headers = new HeaderDictionary(headersDict);
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        // Act
        var result = factory.Create();

        // Assert
        result.Forwarded.ShouldBe(host, nameof(ForwardedHost.Forwarded));
        result.Proto.ShouldBe(proto, nameof(ForwardedHost.Proto));
        result.Host.ShouldBe(host, nameof(ForwardedHost.Host));
        result.Port.ShouldBe(-1, nameof(ForwardedHost.Port));
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void ForwardedHostFactory_Create_HostHeaderMissing_UsesRequestUrl()
    {
        // Arrange
        var factory = CreateFactory();
        var headers = new HeaderDictionary();
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);
        var host = "localhost";
        var port = 8080;
        var original = $"{host}:{port}";
        var url = $"https://{original}/some/path?a=b";
        _mockHostSettings.Setup(m => m.AltXForwardedHost).Returns("X1-Forwarded-host");
        _mockHostSettings.Setup(m => m.AltXForwardedProto).Returns("X1-Forwarded-Proto");
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns(url);

        // Act
        var result = factory.Create();

        // Assert
        result.Forwarded.ShouldBe(original, nameof(ForwardedHost.Forwarded));
        result.Host.ShouldBe(host, nameof(ForwardedHost.Host));
        result.Port.ShouldBe(port, nameof(ForwardedHost.Port));
        _mockRepository.VerifyAll();
    }
    #endregion
}
