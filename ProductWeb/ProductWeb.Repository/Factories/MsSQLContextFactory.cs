using Microsoft.EntityFrameworkCore;

using ProductWeb.Repository.Interfaces;

namespace ProductWeb.Repository.Factories
{
    public class MsSQLContextFactory : IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
