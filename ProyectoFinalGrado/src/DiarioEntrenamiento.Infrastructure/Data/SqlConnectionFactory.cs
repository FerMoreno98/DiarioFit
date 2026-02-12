using System.Data;
using DiarioEntrenamiento.Application.Abstractions.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace DiarioEntrenamiento.Infrastructure.Data;

public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<IDbConnection> CrearConexion(CancellationToken cancellationToken = default)
    {
        IDbConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return Task.FromResult(connection);
    }
}