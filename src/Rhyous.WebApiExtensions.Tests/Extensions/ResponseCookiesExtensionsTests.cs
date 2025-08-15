using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using Rhyous.Collections;
using Rhyous.WebApiExtensions.Extensions;
using Rhyous.WebApiExtensions.Models;

namespace Rhyous.WebApiExtensions.Tests.Extensions
{
    [TestClass]
    public class ResponseCookiesExtensionsTests
    {
        private MockRepository _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
        }

        #region Append
        [TestMethod]
        public void ResponseCookiesExtensions_Append_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            IResponseCookies responseCookies = new ResponseCookies(new HeaderDictionary(), null);
            Cookie cookie = new Cookie { Key = "Key1", Value = "value1", Options = new CookieOptions { } };

            // Act
            responseCookies.Append(cookie);

            // Assert
            var headers = responseCookies.GetPropertyValue("Headers") as HeaderDictionary;
            Assert.AreEqual(1, headers.Count);
            _mockRepository.VerifyAll();
        }
        #endregion
    }
}
