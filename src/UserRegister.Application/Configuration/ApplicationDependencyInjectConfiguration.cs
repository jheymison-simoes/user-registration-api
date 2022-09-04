using UserRegister.Application.Services;
using UserRegister.Business.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using UserRegister.Business.Models;

namespace UserRegister.Application.Configuration;

public static class ApplicationDependencyInjectConfiguration
{
    public static void DependencyInjection(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<BaseService>();
        services.AddScoped<IExempleService, ExempleService>();
        services.AddScoped<IUserService, UserService>();
        #endregion

        #region Validators
        services.AddScoped<CreateUserValidator>();
        services.AddScoped<CreateUserAddressValidator>();
        services.AddScoped<CreateUserPhoneValidator>();
        #endregion
    }
}