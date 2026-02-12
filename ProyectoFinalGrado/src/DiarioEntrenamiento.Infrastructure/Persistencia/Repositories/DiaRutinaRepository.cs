using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class DiaRutinaRepository : IDiaRutinaRepository
{
    private readonly IUnitOfWork _uow;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public DiaRutinaRepository(IUnitOfWork uow, ISqlConnectionFactory sqlConnectionFactory)
    {
        _uow = uow;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task AddAsync(DiaRutina rutina, CancellationToken cancellationToken)
    {
        string sql=@"insert into ""DiaRutina""(""Uid"",""UidRutina"",""Nombre"",""DiaDeLaSemana"")
                    values(@Uid,@UidRutina,@Nombre,@DiaDeLaSemana)";
        var parameters = new
        {
            Uid= rutina.Id,
            UidRutina=rutina.Uid_rutina,
            Nombre=rutina.Nombre,
            DiaDeLaSemana=rutina.DiaDeLaSemana

        };
        if(_uow.Transaction is not null)
        {
            await _uow.Connection.ExecuteAsync(new CommandDefinition(sql,parameters,_uow.Transaction,cancellationToken:cancellationToken));
        }
        else
        {
            using var connection=await _sqlConnectionFactory.CrearConexion();
            await connection.ExecuteAsync(sql,parameters);
        }
    }

    public async Task DeleteAsync(Guid uid, CancellationToken cancellationToken)
    {
        string sql=@"Delete from ""DiaRutina"" where ""Uid""=@uid";
        if(_uow.Transaction is not null)
        {
            await _uow.Connection.ExecuteAsync
            (new CommandDefinition(sql,new{uid},_uow.Transaction, cancellationToken:cancellationToken));
        }
        else
        {
            using var connection=await _sqlConnectionFactory.CrearConexion();
            await connection.ExecuteAsync(sql,new{uid});
        }
    }

    public async Task<IReadOnlyCollection<DiaRutina>> GetAllAsync(Guid uid, CancellationToken cancellationToken = default)
    {
        string sql=@"select ""Uid"",""UidRutina"",""Nombre"",""DiaDeLaSemana"" from ""DiaRutina"" where ""UidRutina""=@uid";
        IEnumerable<DiaRutinaDto> dias=null;
        List<DiaRutina> ret=new List<DiaRutina>();
        if(_uow.Transaction is not null)
        { 
            dias=await _uow.Connection.QueryAsync<DiaRutinaDto>
            (new CommandDefinition(sql,new {uid},_uow.Transaction,cancellationToken:cancellationToken));
        }
        else
        {
           using var connection= await _sqlConnectionFactory.CrearConexion();
            dias=await connection.QueryAsync<DiaRutinaDto>(sql,new{uid});
        }
        foreach(var dia in dias)
        {
            DiaRutina d=DiaRutina.CargarDia(dia.Uid,dia.UidRutina,dia.Nombre,dia.DiaDeLaSemana);
            ret.Add(d);
        }
        return ret;
    }

    public async Task<DiaRutina> GetByIdAsync(Guid id)
    {
        string sql=@"Select ""Uid"", ""UidRutina"", ""Nombre"", ""DiaDeLaSemana"" from ""DiaRutina"" where ""Uid""=@Uid";
        using var connection = await _sqlConnectionFactory.CrearConexion();
        var dia=await connection.QueryFirstOrDefaultAsync<DiaRutinaDto>(sql, new {Uid=id});
        DiaRutina d=DiaRutina.CargarDia(dia.Uid,dia.UidRutina,dia.Nombre,dia.DiaDeLaSemana);
        return d;
    }

    public Task ModificarAsync(Guid uid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}