using System.IO;
using System.Runtime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            GCSettings.LatencyMode = GCLatencyMode.LowLatency;

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            await host.RunAsync();
        }
    }
}