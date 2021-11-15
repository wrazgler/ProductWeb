namespace ProductWeb.Repository.Interfaces
{
    public interface IContextOptions
    {
        public bool IsPostgreSQL { get; set; }
        public string ConnectionString { get; set; }
    }
}
