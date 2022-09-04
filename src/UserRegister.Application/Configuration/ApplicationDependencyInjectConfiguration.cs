using UserRegister.Application.Services;
using UserRegister.Business.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace UserRegister.Application.Configuration;

public static class ApplicationDependencyInjectConfiguration
{
    public static void DependencyInjection(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<IExempleService, ExempleService>();
        #endregion
    }
}