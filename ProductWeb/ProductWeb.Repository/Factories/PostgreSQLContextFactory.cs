using Microsoft.EntityFrameworkCore;

using ProductWeb.Repository.Interfaces;

namespace ProductWeb.Repository.Factories
{
    public class PostreSQLContextFactory : IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}