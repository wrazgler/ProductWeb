using System;
using System.Threading.Tasks;

using ProductWeb.Repository.Models;

namespace ProductWeb.Repository.Interfaces
{
    public interface IBaseRepository : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        Task Save();
    }
}
