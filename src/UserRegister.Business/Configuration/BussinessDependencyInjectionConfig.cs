using UserRegister.Business.EntityModels;
using Microsoft.Extensions.DependencyInjection;

namespace UserRegister.Business.Configuration
{
    public static class BussinessDependencyInjectionConfig
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<UserValidador>();
            services.AddScoped<AddressValidator>();
            services.AddScoped<UserPhoneValidator>();
        }
    }
}
