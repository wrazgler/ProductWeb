using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;

namespace ProductWeb.Repository.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly RepositoryContext _db;

        public ProductRepository(RepositoryContext context)
        {
            _db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.Include(p => p.Categories);
        }

        public async Task<Product> GetById(int id)
        {
            return await _db.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetByName(string name)
        {
            return await _db.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public void Create(Product product)
        {
            _db.Products.Add(product);
        }

        public void Update(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
        }

        public IEnumerable<Product> Find(Func<Product, Boolean> predicate)
        {
            return _db.Products
                .Include(p => p.Categories)
                .Where(predicate).ToList();
        }

        public async Task Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
                _db.Products.Remove(product);
        }
    }
}
