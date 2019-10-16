using GunchartedCLI.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using GunchartedCLI.Commands;
using System.Reflection;

namespace GunchartedCLI
{
    class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostBuilderContext, configBuilder) =>
                {
                    configBuilder
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .AddJsonFile("appsettings.json");
                })
                .ConfigureServices((builder, services) =>
                {
                    services.AddSingleton<DownloadsFolderCleaner>();
                });

            try
            {
                return await hostBuilder.RunCommandLineApplicationAsync<MainCmd>(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
