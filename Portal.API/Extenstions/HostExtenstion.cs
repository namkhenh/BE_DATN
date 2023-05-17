using Common.Logging.Configurations;
using Common.Share.Settings;
using Hangfire;
using Serilog;

namespace Portal.API.Extenstions
{
  public static class HostExtenstion
  {
    public static void AddAppConfigurations(this ConfigureHostBuilder host)
    {
      host.ConfigureAppConfiguration((context, config) =>
      {
        var env = context.HostingEnvironment;
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
      }).UseSerilog(Seriloger.Configure);
    }
  }
}
