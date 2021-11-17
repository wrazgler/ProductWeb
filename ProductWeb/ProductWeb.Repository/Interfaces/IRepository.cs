using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWeb.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> GetByName(string name);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task<IEnumerable<T>> FindAsync(Func<T, Boolean> predicate);
        Task Delete(int id);
        void Add(T item);
        Task AddAsync(T item);
        void Update(T item);
        Task UpdateAsync(T item);
    }
}
