using Microsoft.AspNetCore.Http;

namespace Rhyous.WebApiExtensions.Tests.TestHelpers;

internal class TestHttpContextFactory
{
    public HttpContext Create()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Scheme = "https";
        httpContext.Request.Host = new HostString("example.com");
        httpContext.Request.Path = "/api/resource";
        httpContext.Request.QueryString = new QueryString("?key=value");
        return httpContext;
    }
}
