namespace Persistence.Common.Configuration.Connection.PostgreSql
{
    public record PostgreSqlConfiguration(string Host, int Port, string DatabaseName,
        string User, string Password);
}