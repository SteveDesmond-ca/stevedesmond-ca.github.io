using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using CoreAPM.NET.CoreMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Models;

namespace Web
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("credentials.json")
                .AddApplicationInsightsSettings(true)
                .Build();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreAPM(_configuration);
            services.AddDbContext<DB>();
            services.AddMemoryCache();
            services.AddMvc();
            services.AddApplicationInsightsTelemetry(_configuration);

            services.AddSingleton<Func<DB>>(() => new DB());
            services.AddSingleton<ICache, Cache>();
            services.AddSingleton(_configuration);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void Configure(IApplicationBuilder app)
        {
            app.UseCoreAPM();
            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" } });
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePagesWithRedirects("/");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index", id = 0 });
                routes.MapRoute("page", "{url}",
                    new { controller = "Home", action = "Page" });
                routes.MapRoute("sub-page", "{category}/{url}",
                    new { controller = "Home", action = "SubPage" });
            });
        }
    }
}