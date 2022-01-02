using Microsoft.Extensions.Configuration;

namespace ProductWeb.Repository.Models
{
    public class DbProvider
    {
        private readonly string _postgreSQL;
        private readonly string _msSQL;

        public DbProvider(IConfiguration configuration)
        {
            _postgreSQL = configuration.GetConnectionString("PostgreSQLConnection");
            _msSQL = configuration.GetConnectionString("MSSQLConnection");
        }

        public DbProviderState GetSQL(string connectionString)
        {
            var databaseState = DbProviderState.PostgreSQL;

            if (connectionString == _msSQL)
            {
                databaseState = DbProviderState.MsSQL;
            }

            return databaseState;
        }
    }
}
