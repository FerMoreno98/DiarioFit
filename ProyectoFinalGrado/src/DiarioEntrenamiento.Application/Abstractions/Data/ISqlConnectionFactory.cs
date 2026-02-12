using System.Data;

namespace DiarioEntrenamiento.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    Task<IDbConnection> CrearConexion(CancellationToken cancellationToken= default);
}

