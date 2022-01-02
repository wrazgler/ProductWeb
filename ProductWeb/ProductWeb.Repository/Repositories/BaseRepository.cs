using System;
using System.Threading.Tasks;

using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;

namespace ProductWeb.Repository.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public RepositoryContext Database { get; }
        protected IRepositoryContextFactory ContextFactory { get; }
        protected CategoryRepository CategoryRepository { get; set; }
        protected ProductRepository ProductRepository { get; set; }

        public BaseRepository(string connectionString, IRepositoryContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
            Database = ContextFactory.CreateDbContext(connectionString);
        }

        public IRepository<Category> Categories
        {
            get { return CategoryRepository ??= new CategoryRepository(Database); }
        }

        public IRepository<Product> Products
        {
            get { return ProductRepository ??= new ProductRepository(Database); }
        }
        
        public async Task Save()
        {
            await Database.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed) 
                return;

            if (disposing)
            {
                Database.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}