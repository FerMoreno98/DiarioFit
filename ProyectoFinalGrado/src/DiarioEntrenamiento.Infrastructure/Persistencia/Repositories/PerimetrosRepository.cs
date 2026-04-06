
using System.Runtime.InteropServices.Swift;
using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros.Repository;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;


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

    public async Task<List<Perimetro>> GetAllFromUserAsync(Guid UidUsuario)
    {
        string sql = @"
        SELECT 
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
            ""PantorrillaIzq"",
            ""FechaTomaDePerimetros""
            FROM
                ""Perimetros""
            where ""UidUsuario""=@UidUsuario";
        var connection = await _connection.CrearConexion();
        IEnumerable<PerimetrosDTO> perimetrosdto = await connection.QueryAsync<PerimetrosDTO>(sql, new { UidUsuario });
        List<Perimetro> ret = new List<Perimetro>();
        foreach (var p in perimetrosdto)
        {
            Perimetro perimetro = Perimetro.CrearFromDataBase(
                    p.Uid,
                    p.UidUsuario,
                    p.Cuello,
                    p.BrazoDchoRelajado,
                    p.BrazoDchoTension,
                    p.BrazoIzqRelajado,
                    p.BrazoIzqTension,
                    p.Pecho,
                    p.Hombro,
                    p.Cintura,
                    p.Cadera,
                    p.Abdomen,
                    p.MusloDcho,
                    p.MusloIzq,
                    p.PantorrillaDcha,
                    p.PantorrillaIzq,
                    p.FechaTomaDePerimetros
            );
            ret.Add(perimetro);

        }
        return ret;
    }
}
