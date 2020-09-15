using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Common.Hosting;
using Steeltoe.Extensions.Logging.DynamicSerilog;

namespace Zuehlke.Steeltoe.Research.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .UseCloudHosting()
                    .UseDefaultServiceProvider(options => options.ValidateScopes = true)
                    .ConfigureLogging(loggingBuilder => loggingBuilder.AddDynamicSerilog())
                    .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>());
        }
    }
}