using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;

namespace Rhyous.WebApiExtensions.Tests.Wrappers;

[TestClass]
public class RequestHeadersTests
{
    private MockRepository _mockRepository;

    private HeaderDictionary _headers;


    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _headers = new HeaderDictionary
        {
            { "Header1", "Value1" },
            { "Header2", "Value2" }
        };
    }

    private RequestHeaders CreateRequestHeaders()
    {
        return new RequestHeaders
        {
            Headers = _headers
        };
    }

    #region Headers
    /// <summary>Tests that the RequestHeaders class can be instantiated and contains the expected headers.</summary>
    [TestMethod]
    public void RequestHeaders_HasKey_ReturnsValue()
    {
        // Arrange
        IHeaderDictionary headers = new HeaderDictionary();
        var requestHeaders = CreateRequestHeaders();

        // Act
        var result = requestHeaders.Headers["Header1"];

        // Assert
        result.ToString().ShouldBe("Value1");
        _mockRepository.VerifyAll();
    }

    /// <summary>Tests that when a key does not exist in the headers, it returns an empty value.</summary>
    [TestMethod]
    public void RequestHeaders_DoesNotHaveKey_ReturnsValue()
    {
        // Arrange
        IHeaderDictionary headers = new HeaderDictionary();
        var requestHeaders = CreateRequestHeaders();

        // Act
        var result = requestHeaders.Headers["NonExistentHeader"];

        // Assert
        result.ToString().ShouldBeEmpty();
        _mockRepository.VerifyAll();
    }
    #endregion
}
