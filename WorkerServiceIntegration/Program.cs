using BaseIntegrationCli360;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;

namespace WorkerServiceIntegrationCli360
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .UseSystemd()
            .ConfigureServices((hostContext, services) =>
            {
                services
                .Configure<ConfigurationConnection>(hostContext.Configuration.GetSection("ConnectionStrings"))
                .AddSingleton<IConfigurationConnection>(sp => sp.GetRequiredService<IOptions<ConfigurationConnection>>().Value);
                services.AddSingleton<IBaseFinal, BaseFinal>();
                services.AddSingleton<IBase1Integracao, Base1Integracao>();

                services.AddHostedService<Worker>();
            });
    }
}
