using Microsoft.EntityFrameworkCore;

using ProductWeb.Repository.Interfaces;

namespace ProductWeb.Repository.Factories
{
    public class RepositoryContextFactory: IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString, bool isPostgreSQL)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();

            if (isPostgreSQL)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
