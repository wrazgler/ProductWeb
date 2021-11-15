using Microsoft.EntityFrameworkCore;

using ProductWeb.Repository.Models;

namespace ProductWeb.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }
    }
}
