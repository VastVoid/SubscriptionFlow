namespace Persistence.Common.Configuration.Connection.PostgreSql
{
    public class PostgreSqlConnectionStringFactory : IConnectionStringFactory
    {
        private readonly PostgreSqlConfiguration _configuration;

        public PostgreSqlConnectionStringFactory(PostgreSqlConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)} is not specified");
        }

        public string Create()
        {
            return $"Server={_configuration.Host};" +
                $"Database={_configuration.DatabaseName};" +
                $"Port={_configuration.Port};" +
                $"User Id={_configuration.User};" +
                $"Password={_configuration.Password}; " +
                $"Keepalive=60; Timeout=180;";
        }
    }
}
