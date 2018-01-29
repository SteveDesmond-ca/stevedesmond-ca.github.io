using System.IO;
using System.Runtime;
using Microsoft.AspNetCore.Hosting;

namespace Web
{
    public static class Program
    {
        public static void Main()
        {
            GCSettings.LatencyMode = GCLatencyMode.LowLatency;

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}