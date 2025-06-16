using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Rhyous.WebApiExtensions.DependencyInjection;
using Rhyous.WebApiExtensions.Interfaces;
using Rhyous.WebApiExtensions.Interfaces.Factories;
using Rhyous.WebApiExtensions.Tests.TestHelpers;

namespace Rhyous.WebApiExtensions.Tests.DependencyInjection;

[TestClass]
public class WebApiExtensionsModuleTests
{
    private MockRepository _mockRepository;

    private static Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    private IServiceCollection _services;
    private IServiceProvider _serviceProvider;
    private IConfiguration _configuration;
    private TelemetryClient _TelemetryClient;
    private TestHttpContextFactory _testHttpContextFactory;

    [TestInitializeAttribute]
    public void TestInitialize()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);

        _testHttpContextFactory = new TestHttpContextFactory();

        SetupConfiguration();

        _services = new ServiceCollection();
        // Register upstream registrations (i.e. things that should be pre-registered.)
        _services.AddSingleton<IConfiguration>(_configuration);
        _services.AddOptions();
        _mockHttpContextAccessor = _mockRepository.Create<IHttpContextAccessor>();
        _services.AddScoped<IHttpContextAccessor>((c) => _mockHttpContextAccessor.Object);
        _mockHttpContextAccessor.Setup(x => x.HttpContext)
                        .Returns(() => _testHttpContextFactory.Create());

        _TelemetryClient = new TelemetryClient(new TelemetryConfiguration());
        _services.AddSingleton<TelemetryClient>(_TelemetryClient);

        _services.AddSingleton<ILoggerFactory, LoggerFactory>();
        _services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));


        // Register this module.
        _services.RegisterModule<WebApiExtensionsModule>(_configuration);

        // Build
        _serviceProvider = _services.BuildServiceProvider();
    }

    private void SetupConfiguration()
    {
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               { "HostConfiguration:AppPath", "some/path" },
               }).Build();
    }

    #region Register
    [TestMethod]
    public void WebApiExtensionsModule_Register_IHostConfiguration_Singleton()
    {
        AssertSingleton<IHostSettings>();
    }
    #endregion

    #region Http
    [TestMethod]
    public void WebApiExtensionsModule_Register_HttpContext_Scoped()
    {
        AssertScoped<HttpContext>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IHttpRequest_Scoped()
    {
        AssertScoped<IHttpRequest>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IRequestCookies_Scoped()
    {
        AssertScoped<IRequestCookies>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IRequestHeaders_Scoped()
    {
        AssertScoped<IRequestHeaders>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IUrlParameters_Scoped()
    {
        AssertScoped<IUrlParameters>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IHttpResponse_Scoped()
    {
        AssertScoped<IHttpResponse>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IResponseHeaders_Scoped()
    {
        AssertScoped<IResponseHeaders>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IRequestUrlFactory_Scoped()
    {
        AssertScoped<IRequestUrlFactory>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IRequestUrl_Scoped()
    {
        AssertScoped<IRequestUrl>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IForwardedHostFactory_Scoped()
    {
        AssertScoped<IForwardedHostFactory>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IReferer_Scoped()
    {
        AssertScoped<IReferer>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IForwardedHost_Scoped()
    {
        AssertScoped<IForwardedHost>();
    }
    #endregion

    #region Other Wrappers


    [TestMethod]
    public void WebApiExtensionsModule_Register_IDateTimeOffset_Singleton()
    {
        AssertSingleton<IDateTimeOffset>();
    }

    [TestMethod]
    public void WebApiExtensionsModule_Register_IEnvironment_Singleton()
    {
        AssertSingleton<IEnvironment>();
    }
    #endregion

    #region Helper Methods

    private void AssertSingleton<T>() where T : class
    {
        var scope1 = _serviceProvider.CreateScope();
        var instance1 = scope1.ServiceProvider.GetService<T>();
        var scope2 = _serviceProvider.CreateScope();
        var instance2 = scope2.ServiceProvider.GetService<T>();

        Assert.IsNotNull(instance1);
        Assert.AreSame(instance2, instance1);
    }

    private void AssertScoped<T>() where T : class
    {
        var scope1 = _serviceProvider.CreateScope();
        var instance1 = scope1.ServiceProvider.GetService<T>();
        var scope2 = _serviceProvider.CreateScope();
        var instance2 = scope2.ServiceProvider.GetService<T>();
        var instance2Again = scope2.ServiceProvider.GetService<T>();

        Assert.IsNotNull(instance1);
        Assert.IsNotNull(instance2);

        Assert.AreNotSame(instance2, instance1);
        Assert.AreSame(instance2Again, instance2);
    }

    private void AssertTransient<T>() where T : class
    {
        var scope = _serviceProvider.CreateScope();
        var instance1 = scope.ServiceProvider.GetService<T>();
        var instance2 = scope.ServiceProvider.GetService<T>();

        Assert.IsNotNull(instance1);
        Assert.IsNotNull(instance2);
        Assert.AreNotSame(instance2, instance1);
    }
    #endregion
}
