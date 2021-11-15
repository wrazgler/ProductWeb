namespace ProductWeb.Repository.Interfaces
{
    public interface IRepositoryContextFactory
    {
        RepositoryContext CreateDbContext(string connectionString, bool IsPostgreSQL);
    }
}
