using Microsoft.Extensions.Configuration;

namespace Versatile.Specflow.ExtentReports.CryptoEngine.AppSettings
{
    public static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager() => AppSetting = new ConfigurationBuilder()
            .SetBasePath(SystemProperties.PathProject)
            .AddJsonFile("appsettings.json")
            .Build();
    }
}
