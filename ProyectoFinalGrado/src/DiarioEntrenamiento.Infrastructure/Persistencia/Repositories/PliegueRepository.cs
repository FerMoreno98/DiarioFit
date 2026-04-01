using System;
using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues.Repository;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class PliegueRepository : IPliegueRepository
{
    private readonly ISqlConnectionFactory _connection;
    private readonly IUnitOfWork _uow;

    public PliegueRepository(ISqlConnectionFactory connection, IUnitOfWork uow)
    {
        _connection = connection;
        _uow = uow;
    }

    public async Task Add(Pliegue pliegue)
    {
         string sql = @"
        INSERT INTO ""Pliegues""
        (
            ""Uid"",
            ""Abdominal"",
            ""Suprailiaco"",
            ""Tricipital"",
            ""Subescapular"",
            ""Muslo"",
            ""Pantorrilla"",
            ""UidUsuario""
        )
        VALUES
        (
            @Uid,
            @Abdominal,
            @Suprailiaco,
            @Tricipital,
            @Subescapular,
            @Muslo,
            @Pantorrilla,
            @UidUsuario
        );
    ";
        var connection=await _connection.CrearConexion();
        var parameters = new
        {
        Uid = pliegue.Id,

        pliegue.Abdominal,
        pliegue.Suprailiaco,
        pliegue.Tricipital,
        pliegue.Subescapular,
        pliegue.Muslo,
        pliegue.Pantorrilla,

        pliegue.UidUsuario
        };
        await connection.ExecuteAsync(sql,parameters);
    }

    public async Task<List<Pliegue>> GetAllFromUserAsync(Guid UidUsuario)
    {
        string sql= @"select  
                        ""Uid"",
                        ""Abdominal"",
                        ""Suprailiaco"",
                        ""Tricipital"",
                        ""Subescapular"",
                        ""Muslo"",
                        ""Pantorrilla"",
                        ""UidUsuario""
                        from ""Pliegues""
                        where ""UidUsuario""=@UidUsuario";
        var connection=await _connection.CrearConexion();
        IEnumerable<PliegueDTO> plieguesDTO=await connection.QueryAsync<PliegueDTO>(sql,new {UidUsuario});
        List<Pliegue> ret=new List<Pliegue>();
        foreach(var pliegue in plieguesDTO)
        {
            var entidadPliegue=Pliegue.CrearFromDataBase(
                pliegue.Uid,
                pliegue.Uid,
                pliegue.Abdominal,
                pliegue.Suprailiaco,
                pliegue.Tricipital,
                pliegue.Subescapular,
                pliegue.Muslo,
                pliegue.Pantorrilla,
                pliegue.FechaTomaPliegues
                );
            ret.Add(entidadPliegue);
        }
        return ret;
    }
}
