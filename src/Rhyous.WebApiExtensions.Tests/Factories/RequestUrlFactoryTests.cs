using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Rhyous.WebApiExtensions.Factories;
using Rhyous.WebApiExtensions.Interfaces;
using Shouldly;

namespace Rhyous.WebApiExtensions.Tests.Factories;

[TestClass]
public class RequestUrlFactoryTests
{
    private MockRepository _mockRepository;

    private Mock<IForwardedHost> _mockForwardedHost;
    private Mock<IHostSettings> _mockHostConfiguration;
    private Mock<IHttpRequest> _mockHttpRequest;
    private Mock<IRequestHeaders> _mockRequestHeaders;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _mockForwardedHost = _mockRepository.Create<IForwardedHost>();
        _mockHostConfiguration = _mockRepository.Create<IHostSettings>();
        _mockHttpRequest = _mockRepository.Create<IHttpRequest>();
        _mockRequestHeaders = _mockRepository.Create<IRequestHeaders>();
    }

    private RequestUrlFactory CreateFactory()
    {
        return new RequestUrlFactory(
            _mockForwardedHost.Object,
            _mockHostConfiguration.Object,
            _mockHttpRequest.Object,
            _mockRequestHeaders.Object);
    }

    #region Create  
    [TestMethod]
    public void RequestUrlFactory_Create_OriginalUrlHasPort_ReturnsRequestUrlObject()
    {
        // Arrange  
        var factory = CreateFactory();
        var url = "https://www.somedomain.tld/some/path?a=b";
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns(url);
        _mockForwardedHost.Setup(m => m.Proto).Returns("https");
        _mockForwardedHost.Setup(m => m.Host).Returns("original.somedomain.tld");
        _mockForwardedHost.Setup(m => m.Port).Returns(8080);

        var headers = new HeaderDictionary();
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        _mockHostConfiguration.Setup(m => m.AppPath).Returns("path1/path2");

        // Act  
        var result = factory.Create();

        // Assert  
        var originalUrl = "https://original.somedomain.tld:8080/some/path?a=b";
        result.Url.ShouldBe(url);
        result.ForwardedUrl.ShouldBe(originalUrl);
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void RequestUrlFactory_Create_OriginalUrlWithoutPort_ReturnsRequestUrlObject()
    {
        // Arrange  
        var factory = CreateFactory();
        var url = "https://www.somedomain.tld/some/path?a=b";
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns(url);
        _mockForwardedHost.Setup(m => m.Proto).Returns("https");
        _mockForwardedHost.Setup(m => m.Host).Returns("original.somedomain.tld");
        _mockForwardedHost.Setup(m => m.Port).Returns(-1);

        var headers = new HeaderDictionary();
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        _mockHostConfiguration.Setup(m => m.AppPath).Returns("path1/path2");

        // Act  
        var result = factory.Create();

        // Assert  
        var originalUrl = "https://original.somedomain.tld/some/path?a=b";
        result.Url.ShouldBe(url);
        result.ForwardedUrl.ShouldBe(originalUrl);
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void RequestUrlFactory_Create_OriginalUrl_ProtocolChanges_ReturnsRequestUrlObject()
    {
        // Arrange  
        var factory = CreateFactory();
        var url = "http://www.somedomain.tld/some/path?a=b";
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns(url);
        _mockForwardedHost.Setup(m => m.Proto).Returns("https");
        _mockForwardedHost.Setup(m => m.Host).Returns("original.somedomain.tld");
        _mockForwardedHost.Setup(m => m.Port).Returns(-1);

        var headers = new HeaderDictionary();
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        _mockHostConfiguration.Setup(m => m.AppPath).Returns("path1/path2");

        // Act  
        var result = factory.Create();

        // Assert  
        var originalUrl = "https://original.somedomain.tld/some/path?a=b";
        result.Url.ShouldBe(url);
        result.ForwardedUrl.ShouldBe(originalUrl);
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void RequestUrlFactory_Create_AppRoot_ReturnsRequestWithPath()
    {
        // Arrange  
        var factory = CreateFactory();
        var url = "https://www.somehost.tld/path1/path2/the-rest-of-the-url";
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns(url);
        _mockForwardedHost.Setup(m => m.Proto).Returns("https");
        _mockForwardedHost.Setup(m => m.Host).Returns("www.somehost.tld");
        _mockForwardedHost.Setup(m => m.Port).Returns(-1);

        var headers = new HeaderDictionary();
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        _mockHostConfiguration.Setup(m => m.AppPath).Returns("path1/path2");

        // Act  
        var result = factory.Create();

        // Assert  
        result.AppRootUrl.ShouldBe("https://www.somehost.tld/path1/path2");
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void RequestUrlFactory_Create_AppRoot_CloudOrCdnAlternateUrl_ReturnsRequestWithPath()
    {
        // Arrange  
        var factory = CreateFactory();
        var url = "https://cloud.internal-domain.tld/path1/path2/the-rest-of-the-url";
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns(url);
        _mockForwardedHost.Setup(m => m.Proto).Returns("https");
        _mockForwardedHost.Setup(m => m.Host).Returns("somepublicsite.domaint.tld");
        _mockForwardedHost.Setup(m => m.Port).Returns(-1);

        _mockHostConfiguration.Setup(m => m.AppPath).Returns("path1/path2");
        var headers = new HeaderDictionary();

        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        // Act  
        var result = factory.Create();

        // Assert  
        result.AppRootUrl.ShouldBe("https://somepublicsite.domaint.tld/path1/path2");
        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void RequestUrlFactory_Create_AppRoot_IsLocalApiTests_ReturnsRequestWithoutPath()
    {
        // Arrange  
        var factory = CreateFactory();
        var url = "https://www.somehost.tld/path1/path2/the-rest-of-the-url";
        _mockHttpRequest.Setup(m => m.GetDisplayUrl()).Returns(url);
        _mockForwardedHost.Setup(m => m.Proto).Returns("https");
        _mockForwardedHost.Setup(m => m.Host).Returns("www.somehost.tld");
        _mockForwardedHost.Setup(m => m.Port).Returns(-1);

        var headers = new HeaderDictionary
           {
               { Constants.LocalApiTest, new StringValues("true") }
           };
        _mockRequestHeaders.Setup(m => m.Headers).Returns(headers);

        // Act  
        var result = factory.Create();

        // Assert  
        result.AppRootUrl.ShouldBe("https://www.somehost.tld/");
        _mockRepository.VerifyAll();
    }
    #endregion
}
