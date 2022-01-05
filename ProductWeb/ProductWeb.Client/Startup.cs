using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using ProductWeb.Model.Interfaces;
using ProductWeb.Model.Services;

using ProductWeb.Repository;
using ProductWeb.Repository.Factories;
using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;
using ProductWeb.Repository.Repositories;

namespace ProductWeb.Client 
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IContextOptions>(contextOptions =>
                new ContextOptions
                {
                    ConnectionString = connectionString
                });
            var dbProvider = new DbProvider(Configuration);
            var currentSQL = dbProvider.GetSQL(connectionString);
            switch (currentSQL)
            {
                case DbProviderState.PostgreSQL:
                    services.AddScoped<IRepositoryContextFactory, PostreSQLContextFactory>();
                    break;
                case DbProviderState.MsSQL:
                    services.AddScoped<IRepositoryContextFactory, MsSQLContextFactory>();
                    break;
                default:
                    throw new Exception("SQl doesn't choose");
            }
            services.AddScoped<IBaseRepository>(provider =>
                new BaseRepository(connectionString,
                    provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=GetAllProducts}/{id?}");
            });
        }
    }
}
