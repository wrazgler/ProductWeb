using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;

namespace ProductWeb.Repository.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly RepositoryContext _db;

        public CategoryRepository(RepositoryContext context)
        {
            _db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories.Include(c => c.Products);
        }

        public async Task<Category> GetById(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category> GetByName(string name)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public void Create(Category category)
        {
            _db.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
        }

        public IEnumerable<Category> Find(Func<Category, Boolean> predicate)
        {
            return _db.Categories
                .Include(c => c.Products)
                .Where(predicate).ToList();
        }

        public async Task Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category != null)
                _db.Categories.Remove(category);
        }
    }
}