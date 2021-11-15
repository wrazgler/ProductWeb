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

        public BaseRepository(string connectionString, bool IsPostgreSQL, IRepositoryContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
            Database = ContextFactory.CreateDbContext(connectionString, IsPostgreSQL);
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (CategoryRepository == null)
                    CategoryRepository = new CategoryRepository(Database);
                return CategoryRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (ProductRepository == null)
                    ProductRepository = new ProductRepository(Database);
                return ProductRepository;
            }
        }
        
        public async Task Save()
        {
            await Database.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Database.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}