using Microsoft.AspNetCore.Http;
using Rhyous.WebApiExtensions.Models;

namespace Rhyous.WebApiExtensions.Extensions
{
    /// <summary>Extension methods for <see cref="IResponseCookies"/> to simplify appending cookies.</summary>
    public static class ResponseCookiesExtensions
    {
        /// <summary>Appends a cookie to the response cookies collection.</summary>
        /// <param name="responseCookies">The <see cref="IResponseCookies"/>.</param>
        /// <param name="cookie">The Cookie model.</param>
        public static void Append(this IResponseCookies responseCookies, Cookie cookie)
        {
            responseCookies.Append(cookie.Key, cookie.Value, cookie.Options);
        }
    }
}
