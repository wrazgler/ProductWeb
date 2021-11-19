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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnection = "MsSql"; // PostgreSql

            DbProviderState dbProvider;
            string connection;

            switch (dbConnection)
            {
                case "PostgreSql":
                    dbProvider = DbProviderState.PostgreSql;
                    connection = Configuration.GetConnectionString("PostgreSQLConnection");
                    break;

                case "MsSql":
                    dbProvider = DbProviderState.MsSql;
                    connection = Configuration.GetConnectionString("MSSQLConnection");
                    break;

                default:
                    throw new Exception("База данных не выбрана");
            }

            services.AddScoped<IContextOptions>(contextOptions => 
                new ContextOptions 
                { 
                    ConnectionString = connection,
                    DbProvider = dbProvider
                }) ;
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddScoped<IBaseRepository>(provider =>
                new BaseRepository(connection, dbProvider,
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
