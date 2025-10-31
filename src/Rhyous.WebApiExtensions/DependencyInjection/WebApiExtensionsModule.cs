using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rhyous.WebApiExtensions.Configuration;
using Rhyous.WebApiExtensions.Factories;
using Rhyous.WebApiExtensions.Interfaces;
using Rhyous.WebApiExtensions.Interfaces.Factories;
using Rhyous.WebApiExtensions.Wrappers;

namespace Rhyous.WebApiExtensions.DependencyInjection;

/// <summary>Registers the services for the Web API extensions.</summary>
public class WebApiExtensionsModule : IDependencyInjectionModule
{
    private readonly IConfiguration _configuration;

    /// <summary>The constructor.</summary>
    /// <param name="configuration">An instance of <see cref="IConfiguration"/>.</param>
    public WebApiExtensionsModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>Registers the services for this microservice.</summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/>.</param>
    public void Register(IServiceCollection services)
    {
        // Configuration Settings
        services.Configure<HostSettings>(_configuration.GetSection(HostSettings.Name));
        services.AddSingleton<IHostSettings>(p => p.GetRequiredService<IOptions<HostSettings>>().Value);

        // Current Scope Object Factory
        services.AddScoped(typeof(ICurrentScopeObjectFactory<>), typeof(CurrentScopeObjectFactory<>));

        // Http
        services.AddScoped<HttpContext>(scope => scope.GetService<IHttpContextAccessor>()!.HttpContext!);
        services.AddScoped<IHttpContext>(scope => new HttpContextWrapper(scope.GetService<HttpContext>()!));
        services.AddScoped<IHttpRequest>(scope => scope.GetService<IHttpContext>()!.Request);

        // Request
        services.AddScoped<IRequestCookies>(scope => new RequestCookies { Cookies = scope.GetService<IHttpRequest>()!.Cookies });
        services.AddScoped<IRequestHeaders>(scope => new RequestHeaders { Headers = scope.GetService<IHttpRequest>()!.Headers });
        services.AddScoped<IUrlParameters>(scope => new UrlParameters { Collection = scope.GetService<IHttpRequest>()!.Query });
        services.AddScoped<IRequestUrlFactory, RequestUrlFactory>();
        services.AddScoped<IRequestUrl>(scope => scope.GetService<IRequestUrlFactory>()!.Create());
        services.AddScoped<IForwardedHostFactory, ForwardedHostFactory>();
        services.AddScoped<IForwardedHost>(scope => scope.GetService<IForwardedHostFactory>()!.Create());
        services.AddScoped<IReferer>(scope => new Referer(scope.GetService<IRequestHeaders>()!.Headers![nameof(Referer)]!));

        // Response
        services.AddScoped<IHttpResponse>(scope => scope.GetService<IHttpContext>()!.Response);
        services.AddScoped<IResponseHeaders>(scope => new ResponseHeaders { Headers = scope.GetService<IHttpResponse>()!.Headers });
        services.AddScoped<IResponseCookies>(scope => scope.GetService<IHttpResponse>()!.Cookies); // No wrapper needed as IResponseCookies is already an interface

        // Other Wrappers
        services.AddSingleton<IDateTimeOffset, DateTimeOffsetWrapper>();
        services.AddSingleton<IEnvironment, EnvironmentWrapper>();
    }
}
