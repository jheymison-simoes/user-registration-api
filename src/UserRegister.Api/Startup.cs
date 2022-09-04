using UserRegister.Api.Configuration;
using UserRegister.Data;

namespace UserRegister.Api;

public class Startup
{
    private IConfiguration Configuration { get; }
        
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiConfiguration(Configuration);
        services.AddResourceConfiguration();
        services.DependencyInjection(Configuration);
        services.AddSwaggerConfiguration();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SqlContext context)
    {
        app.UseApiConfiguration(env);
        app.UseSwaggerConfiguration();
    }
}
