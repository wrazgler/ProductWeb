using ProductWeb.Repository.Interfaces;

namespace ProductWeb.Repository
{
    public class ContextOptions : IContextOptions
    {
        public string ConnectionString { get; set; }
    }
}
