using ProductWeb.Repository.Interfaces;
using ProductWeb.Repository.Models;

namespace ProductWeb.Repository
{
    public class ContextOptions : IContextOptions
    {
        public string ConnectionString { get; set; }
    }
}
