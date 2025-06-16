using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;

namespace Rhyous.WebApiExtensions.Tests.Wrappers;

[TestClass]
public class ResponseHeadersTests
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

    private ResponseHeaders CreateResponseHeaders()
    {
        return new ResponseHeaders
        {
            Headers = _headers
        };
    }

    #region Headers
    /// <summary>Tests that the ResponseHeaders class can be instantiated and contains the expected headers.</summary>
    [TestMethod]
    public void ResponseHeaders_HasKey_ReturnsValue()
    {
        // Arrange
        IHeaderDictionary headers = new HeaderDictionary();
        var ResponseHeaders = CreateResponseHeaders();

        // Act
        var result = ResponseHeaders.Headers["Header1"];

        // Assert
        result.ToString().ShouldBe("Value1");
        _mockRepository.VerifyAll();
    }

    /// <summary>Tests that when a key does not exist in the headers, it returns an empty value.</summary>
    [TestMethod]
    public void ResponseHeaders_DoesNotHaveKey_ReturnsValue()
    {
        // Arrange
        IHeaderDictionary headers = new HeaderDictionary();
        var ResponseHeaders = CreateResponseHeaders();

        // Act
        var result = ResponseHeaders.Headers["NonExistentHeader"];

        // Assert
        result.ToString().ShouldBeEmpty();
        _mockRepository.VerifyAll();
    }
    #endregion
}
