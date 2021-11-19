using ProductWeb.Repository.Models;

namespace ProductWeb.Repository.Interfaces
{
    public interface IRepositoryContextFactory
    {
        RepositoryContext CreateDbContext(string connectionString, DbProviderState dbProvider);
    }
}
