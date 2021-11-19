using Microsoft.EntityFrameworkCore;
using System;

using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;

namespace ProductWeb.Repository.Factories
{
    public class RepositoryContextFactory: IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString, DbProviderState dbProvider)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();

            switch (dbProvider)
            {
                case DbProviderState.PostgreSql:
                    optionsBuilder.UseNpgsql(connectionString);
                    break;

                case DbProviderState.MsSql:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;

                default:
                    throw new Exception("База данных не выбрана");
            }

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
