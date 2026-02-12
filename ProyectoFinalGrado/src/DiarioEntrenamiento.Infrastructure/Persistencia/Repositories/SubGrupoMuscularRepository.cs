using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.GruposMusculares;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class SubGrupoMuscularRepository : ISubGrupoMuscularRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public SubGrupoMuscularRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<SubGrupoMuscular>> GetSubGruposBy(int idGrupo)
    {
        string sql1=@"select ""Id"",""NombreSubGrupo"" from ""SubGrupoMuscular"" where ""IdGrupoMuscular""=@IdGrupo";
        string sql2=@"select ""Id"",""NombreGrupo"" from ""GrupoMuscular"" where ""Id""=@IdGrupo";
        using var connection = await _connectionFactory.CrearConexion();
        var subGrupos=await connection.QueryAsync<SubGrupoMuscularDto>(sql1,new {IdGrupo=idGrupo});
        var grupo=await connection.QueryFirstOrDefaultAsync<GrupoMuscularDto>(sql2,new{IdGrupo=idGrupo});
        GrupoMuscular nuevogrupo=GrupoMuscular.CrearFromDataBase(grupo.Id,grupo.NombreGrupo);
        List<SubGrupoMuscular> ret=new List<SubGrupoMuscular>();
        foreach(var subGrupo in subGrupos)
        {
            ret.Add(nuevogrupo.CrearSubGrupoFromDataBase(subGrupo.Id,idGrupo,subGrupo.NombreSubGrupo));
        }
        return ret;

    }
}