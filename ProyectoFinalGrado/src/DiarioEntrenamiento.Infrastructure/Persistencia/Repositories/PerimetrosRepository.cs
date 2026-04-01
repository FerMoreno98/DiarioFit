
using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros.Repository;


namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class PerimetrosRepository : IPerimetrosRepository
{  
    private readonly ISqlConnectionFactory _connection;
    private readonly IUnitOfWork _uow;

    public PerimetrosRepository(ISqlConnectionFactory connection, IUnitOfWork uow)
    {
        _connection = connection;
        _uow = uow;
    }

public async Task Add(Perimetro perimetro)
{
    string sql = @"
        INSERT INTO ""Perimetros""
        (
            ""Uid"",
            ""UidUsuario"",
            ""Cuello"",
            ""BrazoDchoRelajado"",
            ""BrazoDchoTension"",
            ""BrazoIzqRelajado"",
            ""BrazoIzqTension"",
            ""Pecho"",
            ""Hombro"",
            ""Cintura"",
            ""Cadera"",
            ""Abdomen"",
            ""MusloDcho"",
            ""MusloIzq"",
            ""PantorrillaDcha"",
            ""PantorrillaIzq""
        )
        VALUES
        (
            @Uid,
            @UidUsuario,
            @Cuello,
            @BrazoDchoRelajado,
            @BrazoDchoTension,
            @BrazoIzqRelajado,
            @BrazoIzqTension,
            @Pecho,
            @Hombro,
            @Cintura,
            @Cadera,
            @Abdomen,
            @MusloDcho,
            @MusloIzq,
            @PantorrillaDcha,
            @PantorrillaIzq
        );
    ";

    var connection = await _connection.CrearConexion();

    var parameters = new
    {
        Uid = perimetro.Id,
        perimetro.UidUsuario,

        perimetro.Cuello,
        perimetro.BrazoDchoRelajado,
        perimetro.BrazoDchoTension,
        perimetro.BrazoIzqRelajado,
        perimetro.BrazoIzqTension,

        perimetro.Pecho,
        perimetro.Hombro,
        perimetro.Cintura,
        perimetro.Cadera,
        perimetro.Abdomen,

        perimetro.MusloDcho,
        perimetro.MusloIzq,
        perimetro.PantorrillaDcha,
        perimetro.PantorrillaIzq
    };

    await connection.ExecuteAsync(sql, parameters);
}
}
