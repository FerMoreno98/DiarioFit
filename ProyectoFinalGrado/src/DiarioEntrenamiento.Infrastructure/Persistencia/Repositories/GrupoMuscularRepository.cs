using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.GruposMusculares;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class GrupoMuscularRepository : IGrupoMuscularRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GrupoMuscularRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<GrupoMuscular>> GetAll()
    {
        string sql=@"Select ""Id"",""NombreGrupo"" from ""GrupoMuscular""";
        using var connection= await _connectionFactory.CrearConexion();
        IEnumerable<GrupoMuscularDto> resultado= await connection.QueryAsync<GrupoMuscularDto>(sql);
        List<GrupoMuscular> ret=new List<GrupoMuscular>();
        foreach(var grupo in resultado)
        {
            GrupoMuscular nuevogrupo=GrupoMuscular.CrearFromDataBase(grupo.Id,grupo.NombreGrupo);
            ret.Add(nuevogrupo);

        }
        return ret;
    }

    public Task<GrupoMuscular> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<GrupoMuscular> ObtenerGrupoPorSubGrupo(int idSubGrupo)
    {
        string sql=@"select g.""Id"",
        g.""NombreGrupo"" 
        from ""GrupoMuscular"" g join ""SubGrupoMuscular"" s
        on g.""Id""=s.""IdGrupoMuscular"" 
        where s.""Id""=@IdSubGrupo";
        using var connection=await _connectionFactory.CrearConexion();
        GrupoMuscularDto grupo= await connection.QueryFirstOrDefaultAsync<GrupoMuscularDto>(sql,new{idSubGrupo});
        GrupoMuscular ret=GrupoMuscular.CrearFromDataBase(grupo.Id,grupo.NombreGrupo);
        return ret;


    }
}