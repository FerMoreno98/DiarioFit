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

    public async Task<List<Ejercicio>> GetByIds(IEnumerable<Guid> ids)
    {
        string sql=@"Select ""IdEjercicio"" ,""Nombre"" from ""EjerciciosBase"" where ""IdEjercicio""=@id ";
        List<Ejercicio> ret=new List<Ejercicio>();
        using var connection=await _connectionFactory.CrearConexion();
        foreach(var id in ids)
        {
            var ejercicio=await connection.QueryFirstOrDefaultAsync<EjercicioDto>(sql, new {id});
            Ejercicio ejer=Ejercicio.CrearFromDataBase(ejercicio.IdEjercicio,ejercicio.Nombre);
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
    
}