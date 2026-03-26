using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class EjercicioRepository : IEjercicioRepository
{

    private readonly ISqlConnectionFactory _connectionFactory;

    public EjercicioRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<Ejercicio>> GetByIds(List<Guid> ids)
    {
        string sql=@"
        SELECT ""IdEjercicio"", ""Nombre""
        FROM ""EjerciciosBase""
        WHERE ""IdEjercicio"" = ANY(@ids);";
        using var connection=await _connectionFactory.CrearConexion();
        IEnumerable<EjercicioDto> ejercicio=await connection.QueryAsync<EjercicioDto>(sql, new { ids = ids.ToArray() });
        List<Ejercicio> ret=new List<Ejercicio>();
        foreach(var ejerci in ejercicio)
        {
            Ejercicio ejer=Ejercicio.CrearFromDataBase(ejerci.IdEjercicio,ejerci.Nombre);
            ret.Add(ejer);
        }
        return ret;
    }

    public async Task<List<Ejercicio>> GetbySubGrupoMuscular(int idSubgrupo)
    {
        string sql=@"select ""IdEjercicio"", ""Nombre"" from ""EjerciciosBase"" 
                        where ""IdEjercicio"" in 
                        (select ""idEjercicioBase""
                        from ""EjercicioBase-SubgrupoMuscular""
                        where ""idSubGrupoMuscular""=@idSubgrupo)";
        using var connection=await _connectionFactory.CrearConexion();
        var ejercicios= await connection.QueryAsync<EjercicioDto>(sql,new { idSubgrupo });
        List<Ejercicio> ret=new List<Ejercicio>();
        foreach(var ejer in ejercicios)
        {
            var ejercicio=Ejercicio.CrearFromDataBase(ejer.IdEjercicio,ejer.Nombre);
            ret.Add(ejercicio);
        }
        return ret;
    }
    public async Task<string> GetNombreById(Guid IdEjercicio)
    {
        string sql=@"select ""Nombre"" from ""EjerciciosBase"" where ""IdEjercicio""=@IdEjercicio";
        using var connection=await _connectionFactory.CrearConexion();
        return await connection.QueryFirstOrDefaultAsync<string>(sql,new {IdEjercicio});
    }
    public async Task<Guid> GetUidByNombre(string Nombre)
    {
        string sql=@"Select ""IdEjercicio"" from ""EjerciciosBase"" where ""Nombre""=@Nombre";
        using var connection=await _connectionFactory.CrearConexion();
        return await connection.QueryFirstOrDefaultAsync<Guid>(sql,new{Nombre});
    }

    public async Task<IEnumerable<(string,string)>> ObtenerRelacionEjercioGrupoMuscular()
    {
        string sql =
            @"select distinct 
        (select ""Nombre"" from ""EjerciciosBase"" where ""IdEjercicio""=ebsgm.""idEjercicioBase""),
        ""NombreGrupo""
        from ""GrupoMuscular"" gm join ""SubGrupoMuscular"" sgm
            on gm.""Id"" = sgm.""IdGrupoMuscular""
        join ""EjercicioBase-SubgrupoMuscular"" ebsgm
            on ebsgm.""idSubGrupoMuscular"" = sgm.""Id""";
        using var connection = await _connectionFactory.CrearConexion();
        var result= await connection.QueryAsync<(string ejercicio,string grupo)>(sql);
        return result;
    }
}