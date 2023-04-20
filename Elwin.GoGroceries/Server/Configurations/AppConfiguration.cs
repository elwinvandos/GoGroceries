using Elwin.GoGroceries.Infrastructure.Config;
using System.Reflection;

namespace Elwin.GoGroceries.API.Configurations
{
    internal static class AppConfiguration
    {
        internal static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{context.HostingEnvironment}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });
            return host;
        }

        internal static WebApplicationBuilder AddConfigurationServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<InfrastructureSettings>(builder.Configuration.GetSection("InfrastructureSettings"));
            // Add more app layer specific configs here
            return builder;
        }
    }
}
