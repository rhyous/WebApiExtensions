using Microsoft.AspNetCore.Http;
using Moq;
using Rhyous.WebApiExtensions.Extensions;
using Rhyous.WebApiExtensions.Models;

namespace Rhyous.WebApiExtensions.Tests.Extensions
{
    [TestClass]
    public class ResponseCookiesExtensionsTests
    {
        #region Append
        [TestMethod]
        public void ResponseCookiesExtensions_Append_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            IResponseCookies responseCookies = httpContext.Response.Cookies;
            Cookie cookie = new Cookie { Key = "Key1", Value = "value1", Options = new CookieOptions { } };

            // Act
            responseCookies.Append(cookie);

            // Assert
            var headers = httpContext.Response.Headers;
            Assert.AreEqual(1, headers.Count);
            var setCookieHeader = headers["Set-Cookie"].ToString();
            Assert.IsTrue(setCookieHeader.Contains("Key1=value1"));
        }

        [TestMethod]
        public void Append_NullOptions_AppendsCookieWithDefaultOptions()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            IResponseCookies responseCookies = httpContext.Response.Cookies;
            Cookie cookie = new Cookie { Key = "Key1", Value = "value1", Options = null };

            // Act
            responseCookies.Append(cookie);

            // Assert
            var headers = httpContext.Response.Headers;
            Assert.AreEqual(1, headers.Count);
            var setCookieHeader = headers["Set-Cookie"].ToString();
            Assert.IsTrue(setCookieHeader.Contains("Key1=value1"));
        }
        #endregion
    }
}
