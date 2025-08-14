# WebApiExtensions
A project to store common extensions to WebApi projects


## Dependency Injection Modules

This library adds a module type, IDependencyInjectionModule, that can be used to register services in the DI container more easily.
It also adds and extension method that allows for registering a module.


### Registering a module
In your main.cs (or in Startup.cs if you are using older dotnet versions), you can register your module like this:)
```csharp
// Register the module
builder.Services.AddModule<MyModule>(); // Any parameters the MyModule constructor needs can be passed here.
```

### WebApiExtensionsModule
There is a WebApiExtensionsModule.cs that is available and registers all the interface and concretes that are used in the WebApiExtensions library.

It takes in an `IConfiguration` instance.
```csharp
  // Register the WebApiExtensionsModule module
  builder.Services.AddModule<MyModule>(builder.Configuration);
```

### Creating Your Own Module
You can create your own module. We highly recommend keeping all your registrations in a separate module to keep your main.cs or Startup.cs clean and organized.
```csharp
public class MyModule : IDependencyInjectionModule
{
  public void RegisterServices(IServiceCollection services)
  {
    // Register your services here
    services.AddTransient<IMyService, MyService>();
  }
}
```
### Unit Testing a Dependency Injection Module
If you want to unit test a module, you can use the `ServiceCollection` class to create a new service collection and register your module in it.
For an example of how to unit tests your Dependency Injection module, look at [WebApiExtensionsModuleTests.cs](./src/Rhyous.WebApiExtensions.Tests/DependencyInjection/WebApiExtensionsModuleTests.cs) for an example of how to do this.

> Hint: Unit Testing your dependency injection module will help keep your DI registrations stable, accurrate, and working as expected. It is highly recommended to unit test your modules.

### Separating Registration and Instantiation
Be careful to not couple the registration of your services with their instantiation.
The module should only be responsible for registering the services, not for creating instances of them or even instances of dependencies.
This allows for better testability and separation of concerns.

> Hint: Use lambda's to register services that require parameters. Avoid calling `new` in the module registration method. However, if you must call new, do it in a lambda to ensure that the instance is created when it is resolved, not when it is registered.

```csharp
services.AddTransient<IMyService>(provider => new MyService(provider.GetRequiredService<ISomeDependency>()));
services.AddScoped<IMyScopedService>(provider => new MyScopedService(provider.GetRequiredService<ISomeDependency>()));
services.AddSingletone<IMySingeltonService>(provider => new MySingeltonService(provider.GetRequiredService<ISomeDependency>()));
```
> This way, even if you call `new`, it won't be called during registration, but instead when the instance is resolved.

## HttpContext Wrappers - Including in Module Registration
The library also adds some useful wrappers to the `HttpContext` class. It wraps a lot of the common operations that are done in a WebApi project, such as getting the user, getting the request body, etc.
It converts a lot of this into interfaces, making it easier to register, inject, mock, and unit test.

Here is a list of the wrapper interfaces that are added:
- [IDateTimeOffset.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IDateTimeOffset.cs)
- [IForwardedHost.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IForwardedHost.cs)
- [IHttpRequest.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IHttpRequest.cs)
- [IHttpResponse.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IHttpResponse.cs)
- [IHttpResponseMessage.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IHttpResponseMessage.cs)
- [IReferer.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IReferer.cs)
- [IRequestCookies.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IRequestCookies.cs)
- [IRequestHeaders.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IRequestHeaders.cs)
- [IRequestUrl.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IRequestUrl.cs)
- [IResponseHeaders.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IResponseHeaders.cs)
- [IUrlParameters.cs](./src/Rhyous.WebApiExtensions.Interfaces/Wrappers/IUrlParameters.cs)


> Hint: It is quite tedious to unit test HttpContextAccessor, so don't use it. Instead, injection what you need. These registrations will allow you to inject the wrappers you need, such as ``IHttpRequestHeadders``, which are easy to use and mock. Also, coupling to a concrete is not recommended and HttpContextAccessor is a concrete.

## ForwardedHost
The library also adds a `ForwardedHost` class that can be used to get the forwarded host from the request headers. This is useful when you are behind a reverse proxy and need to get the original host from the request headers.
This uses X-Forwarded-Host and X-Forwarded-Proto headers to determine the original host and protocol.
However as X-Forwarded-Host and X-Forwarded-Proto can fail when passing through multiple proxies, it also allows you to create your own custom <MyPrefix>-Forwarded-Host and <MyPrefix>-Forwarded-Proto headers that will not be wiped out by multiple proxies.

If you do not configure the prefix, X1 is used by default. It is fine if you don't use it, but if you do, you will need to configure it in your WebApi project. See HostSettings.cs.

HostSettings.cs can be configured in the normal way with appSetting.json.
```json
{
  "HostSettings": {
    "ForwardedHostPrefix": "X1"
  }
}
```
Or by using the environment variable:
```
HostSettings__ForwardedHostPrefix=X1
```

## Startup Validation
This addes some basic classes around startup validation.
It is often better to fail earlier in the startup process than to fail later when the application is running.
This library adds a `StartupValidator` class that can be used to validate the startup process. It allows you to register validation rules and will throw an exception if any of the rules fail.
### Creating a Startup Validation Rule
You can register a startup validation rule by implementing the `IStartupValidationRule` interface and registering it in your module.
```csharp
public class MyStartupValidationRule : IStartupValidationRule
{
  public void Validate()
  {
    var result1 = new StartupValidationResult
    {
      IsValid = false, // or false if the validation fails
      Message = "My validation rule failed."
    };
    
    var result2 = new StartupValidationResult
    {
      IsValid = false, // or false if the validation fails
      Message = "My validation rule failed."
    };
    return [result1, result2];
  }
}
```

### Registring a Startup Validation Rule
Simply register it in your module like this:
```csharp
public class MyModule : IDependencyInjectionModule
{
  public void RegisterServices(IServiceCollection services)
  {
    // Register it as an IStartupValidator
    services.AddSingleton<IStartupValidator, MyStartupValidationRule>();
  }
}
```

### Running the Startup Validation
In your main.cs (or Startup.cs), you can run the startup validation like this:
```csharp
// Run the startup validation
var startupValidator = app.Services.GetRequiredService<IStartupValidator>();
await startupValidator.ValidateAsync();
```

If any of the validation rules fail, an exception will be thrown with the details of the validation failures and your app will not start.
