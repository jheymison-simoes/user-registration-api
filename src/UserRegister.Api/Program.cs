
using UserRegister.Business.Enums;
using UserRegister.Business.Utils;

namespace UserRegister.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(Build)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

    private static void Build(IConfigurationBuilder builder)
    {
        var enviromentBuild = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (enviromentBuild == EnvironmentBuildEnum.Development.GetEnumDescription() ||
            enviromentBuild == EnvironmentBuildEnum.Docker.GetEnumDescription())
            builder.AddJsonFile($"appsettings.{enviromentBuild}.json", false);
    }
}