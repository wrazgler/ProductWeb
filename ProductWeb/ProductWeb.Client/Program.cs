using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

using ProductWeb.Repository;
using ProductWeb.Repository.Interfaces;

namespace ProductWeb.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var factory = services.GetRequiredService<IRepositoryContextFactory>();
                    var contextOptions = services.GetRequiredService<IContextOptions>();
                    using (var context = factory.CreateDbContext(contextOptions.ConnectionString, contextOptions.DbProvider))
                    {
                        DbInitializer.Initialize(context);
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .Build();
    }
}
