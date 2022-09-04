using Microsoft.Extensions.DependencyInjection;
using UserRegister.Business.Interfaces.Repositories;
using UserRegister.Data.Repositories;

namespace UserRegister.Data.Configuration;

public static class DataInjectionConfiguration
{
    public static void DependencyInjection(this IServiceCollection services)
    {
        InjectionDependencyRepository(services);
        InjectionDependencyUniOfWork(services);
    }

    private static void InjectionDependencyRepository(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
    
    private static void InjectionDependencyUniOfWork(IServiceCollection services)
    {
    }
}