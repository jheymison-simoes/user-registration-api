using System.Globalization;
using System.Resources;
using UserRegister.Api.Resource;
using UserRegister.Application.Configuration;
using UserRegister.Business.Configuration;
using UserRegister.Data.Configuration;

namespace UserRegister.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection DependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddSingleton(new ResourceManager(typeof(ApiResource)));
        services.AddScoped(provider =>
        {
            var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;

            if (httpContext == null)
            {
                return CultureInfo.InvariantCulture;
            }

            return httpContext.Request.Headers.TryGetValue("language", out var language)
                ? new CultureInfo(language)
                : CultureInfo.InvariantCulture;
        });

        ApplicationDependencyInjectConfiguration.DependencyInjection(services);
        BussinessDependencyInjectionConfig.DependencyInjection(services);
        DataInjectionConfiguration.DependencyInjection(services);

        return services;
    }
}