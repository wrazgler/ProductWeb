using ProductWeb.Repository.Models;

namespace ProductWeb.Repository.Interfaces
{
    public interface IContextOptions
    {
        public string ConnectionString { get; set; }
    }
}
