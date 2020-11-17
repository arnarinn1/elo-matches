using System.IO;
using Metrics.Logging.Endpoint.CompositionRoot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Metrics.Logging.Endpoint
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices(WireUp.RegisterServices);
    }
}
