using Microsoft.EntityFrameworkCore;

using ProductWeb.Repository.Interfaces;

namespace ProductWeb.Repository.Factories
{
    public class RepositoryContextFactory: IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();

            if (connectionString == "Host=localhost;Port=5433;Database=DeckListDb;Username=postgres;Password=12345")
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
