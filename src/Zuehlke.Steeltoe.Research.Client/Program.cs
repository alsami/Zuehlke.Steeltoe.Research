using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
using Zuehlke.Steeltoe.Research.Client.Clients;
using Zuehlke.Steeltoe.Research.Client.Services;

namespace Zuehlke.Steeltoe.Research.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            await host.StartAsync();

            var client = host.Services.GetRequiredService<IResearchApiService>();

            await client.RequestApiRandomAsync(100);

            await host.StopAsync();
            
            Debugger.Break();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .AddServiceDiscovery(opt => opt.UseConsul())
                .ConfigureServices(ConfigureServices)
                .UseSerilog(ConfigureLogger);
        }

        private static void ConfigureLogger(HostBuilderContext hostBuilderContext,
            LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
        }

        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            services
                .AddScoped<IResearchApiService, ResearchApiService>()
                .AddHttpClient<ISteeltoeResearchApiClient, SteeltoeResearchApiClient>()
                .AddRoundRobinLoadBalancer();
        }
    }
}