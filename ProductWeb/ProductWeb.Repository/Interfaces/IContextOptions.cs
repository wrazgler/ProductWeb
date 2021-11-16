namespace ProductWeb.Repository.Interfaces
{
    public interface IContextOptions
    {
        public bool IsPostgreSql { get; set; }
        public string ConnectionString { get; set; }
    }
}
