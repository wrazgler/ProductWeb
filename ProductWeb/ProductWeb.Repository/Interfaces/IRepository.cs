using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWeb.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task<T> GetByName(string name);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task Delete(int id);
        void Create(T item);
        void Update(T item);
    }
}
