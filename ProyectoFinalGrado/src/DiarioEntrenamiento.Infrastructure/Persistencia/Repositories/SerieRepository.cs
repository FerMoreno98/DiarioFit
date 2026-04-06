using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.DTOs;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class SerieRepository : ISerieRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private IUnitOfWork _uow;

    public SerieRepository(ISqlConnectionFactory connectionFactory, IUnitOfWork uow)
    {
        _connectionFactory = connectionFactory;
        _uow = uow;
    }

    public async Task<bool> EsSerieRepetida(Guid UidSesion, Guid UidEjercicio, int Serie)
    {
        string sql=@"Select Count(1) from ""RegistroDatosSesionEjercicio"" 
                    where ""UidRegistroDatosSesion""=@UidSesion and
                    ""UidEjercicio""=@UidEjercicio
                    and ""Serie""=@Serie";
        using var connection=await _connectionFactory.CrearConexion();
        int resultado= await connection.ExecuteScalarAsync<int>(sql,new {UidSesion,UidEjercicio,Serie});
        return resultado>0;
    }

    public async Task<List<SerieRealizada>> ObtenerHistoricoSeriesDeUnEjercicio(List<Guid> UidEjercicio)
    {
        string sql= @"select 
        ""Uid"",
        ""UidEjercicio"",
        ""UidRegistroDatosSesion"" UidSesion,
        ""Peso"",
        ""Repeticiones"",
        ""Rir""
        from ""RegistroDatosSesionEjercicio""
        where 
        ""UidEjercicio""=ANY(@UidEjercicio)
        and ""Serie""=1";
        using var connection=await _connectionFactory.CrearConexion();
        IEnumerable<DTOs.SerieDto> seriedto = await connection.QueryAsync<DTOs.SerieDto>(sql, new { UidEjercicio });
        List<SerieRealizada> ret = new List<SerieRealizada>();
        foreach (var s in seriedto)
        {
            SerieRealizada serie = SerieRealizada.CrearFromDataBase(s.Uid, s.UidEjercicio, s.UidSesion, s.Peso, s.Repeticiones, s.Rir, 1);
            ret.Add(serie);
        }
        return ret;
    }

    public async Task RegistrarSerie(Guid UidSerie,Guid UidSesion, Guid uidEjercicio, decimal? Peso, int? Repeticiones, string? Rir, int Serie)
    {
        string sql=@"insert into ""RegistroDatosSesionEjercicio""
                    (
                    ""Uid"",
                    ""UidRegistroDatosSesion"",
                    ""Peso"",
                    ""Repeticiones"",
                    ""Rir"",
                    ""UidEjercicio"",
                    ""Serie""
                    )values
                    (
                    @UidSerie,
                    @UidSesion,
                    @Peso,
                    @Repeticiones,
                    @Rir,
                    @UidEjercicio,
                    @Serie
                    )";
            var parametros = new
            {
                UidSerie=UidSerie,
                UidSesion=UidSesion,
                uidEjercicio=uidEjercicio,
                Peso=Peso,
                Repeticiones=Repeticiones,
                Rir=Rir,
                Serie=Serie
            };
            using var connection=await _connectionFactory.CrearConexion();
            await connection.ExecuteAsync(sql,parametros);
    }

    public async Task UpdateSerie(Guid UidSesion, Guid UidEjercicio, decimal? Peso, int? Repeticiones, string? Rir, int Serie)
    {
        string sql=@"update ""RegistroDatosSesionEjercicio"" set
                     ""Peso""=@Peso,
                    ""Repeticiones""=@Repeticiones,
                    ""Rir""=@Rir 
                    where ""UidRegistroDatosSesion""=@UidSesion
                    and ""UidEjercicio""=@UidEjercicio
                    and ""Serie""=@Serie";
        var parametros = new
        {
            UidSesion=UidSesion,
            UidEjercicio=UidEjercicio,
            Peso=Peso,
            Repeticiones=Repeticiones,
            Rir=Rir,
            Serie=Serie
        };
        using var connection=await _connectionFactory.CrearConexion();
        await connection.ExecuteAsync(sql,parametros);
    }
}