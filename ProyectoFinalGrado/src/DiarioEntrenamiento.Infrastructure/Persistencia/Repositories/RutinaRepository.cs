using System.Collections;
using System.Security.AccessControl;
using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Rutinas.ObtenerDatosHomePage;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

internal sealed class RutinaRepository : IRutinaRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private IUnitOfWork _uow;

    public RutinaRepository(ISqlConnectionFactory connectionFactory, IUnitOfWork uow)
    {
        _connectionFactory = connectionFactory;
        _uow = uow;
    }

    public async Task AddAsync(Rutina rutina, CancellationToken cancellationToken)
    {

        string sql = @"insert into ""Rutinas""
        (""Uid"",""UidUsuario"",""Nombre"",""FechaInicio"",""FechaFin"")
        values(@Uid,@UidUsuario,@Nombre,@FechaInicio,@FechaFin)";
        var parametros = new
        {
            Uid = rutina.Id,
            UidUsuario = rutina.UidUsuario,
            Nombre = rutina.Nombre,
            FechaInicio = rutina.FechaInicio.ToDateTime(TimeOnly.MinValue),
            FechaFin=rutina.FechaFin.ToDateTime(TimeOnly.MinValue),
        };
        if (_uow.Transaction is not null)
        {
            await _uow.Connection.ExecuteAsync(new CommandDefinition(sql, parametros, _uow.Transaction, cancellationToken: cancellationToken));
        }
        else
        {
            using var connection = await _connectionFactory.CrearConexion();
            await connection.ExecuteAsync(sql, parametros);
        }
        
    }

    public async Task DeleteAsync(Guid uid, CancellationToken cancellationToken=default)
    {
        string sql=@"Delete from ""Rutinas"" where ""Uid""=@uid";
        if(_uow.Transaction is not null)
        {
            await _uow.Connection.ExecuteAsync
            (new CommandDefinition(sql,new{uid},_uow.Transaction, cancellationToken:cancellationToken));
        }
        else
        {
        using var connection=await _connectionFactory.CrearConexion();
        await connection.ExecuteAsync(sql,new {uid});
        }
    }

    public async Task<Rutina> GetCurrentAysnc(Guid UidUsuario,CancellationToken cancellationToken = default)
    {
        string sql=@"select 
        ""Uid"",""UidUsuario"",""Nombre"",""FechaInicio"",""FechaFin"" 
        from ""Rutinas"" 
        where ""UidUsuario""=@UidUsuario and
        ""FechaInicio""<=NOW() and 
        ""FechaFin"">NOW() ";
        IEnumerable<RutinaDto> resultado=null;
        RutinaDto ret=null;
        Rutina rutina=null;
        if(_uow.Transaction is not null)
        {
             resultado=await _uow.Connection.QueryAsync<RutinaDto>
             (new CommandDefinition(sql,new{UidUsuario},_uow.Transaction,cancellationToken:cancellationToken));
        }
        else
        {
            using var connection = await _connectionFactory.CrearConexion();
            resultado=await connection.QueryAsync<RutinaDto>(sql,new{UidUsuario});
        }
        if (resultado.Count() > 1)
        {
            ret= resultado.MaxBy(r=>r.FechaInicio);
        
        }
        if(resultado.Count() == 0)
        {
            return rutina;
        }
        if (resultado.Count() == 1)
        {
            ret= resultado.Single();
        }
        rutina=Rutina.CrearFromDataBase(ret.Uid,ret.UidUsuario,ret.Nombre,DateOnly.FromDateTime(ret.FechaInicio),DateOnly.FromDateTime(ret.FechaFin));
        return rutina;
    }

    public async Task ModificarAsync(Rutina rutina, CancellationToken cancellationToken)
    {
       string sql=@"Update 
       ""Rutinas"" set 
       ""Nombre""=@Nombre,
       ""FechaInicio""=@FechaInicio,
       ""FechaFin""=@FechaFin 
       where ""Uid""=@UidRutina";
       using var connection=await _connectionFactory.CrearConexion();
       // No entiendo por que, cuando pasan por aqui se me resta un dia
       // asi que le sumo un dia a las fechas
       // Hacer refactor para manejador de fechas
       var parametros = new
       {
           UidRutina=rutina.Id,
           Nombre=rutina.Nombre,
           FechaInicio=rutina.FechaInicio.ToDateTime(TimeOnly.MinValue).AddDays(1),
           FechaFin=rutina.FechaFin.ToDateTime(TimeOnly.MinValue).AddDays(1)
       };
       await connection.ExecuteAsync(sql,parametros);
    }
    public async Task<Rutina> GetByIdAsync(Guid id,CancellationToken cancellationToken = default)
    {
        string sql=@"select 
        ""Uid"",""UidUsuario"",""Nombre"",""FechaInicio"",""FechaFin"" 
        from ""Rutinas"" where ""Uid""=@id ";
        
        RutinaDto ret=null;
        Rutina rutina=null;
        if(_uow.Transaction is not null)
        {
             ret=await _uow.Connection.QueryFirstOrDefaultAsync<RutinaDto>
             (new CommandDefinition(sql,new{id},_uow.Transaction,cancellationToken:cancellationToken));
        }
        else
        {
            using var connection = await _connectionFactory.CrearConexion();
            ret=await connection.QueryFirstOrDefaultAsync<RutinaDto>(sql,new{id});
        }
        rutina=Rutina.CrearFromDataBase(ret.Uid,ret.UidUsuario,ret.Nombre,DateOnly.FromDateTime(ret.FechaInicio),DateOnly.FromDateTime(ret.FechaFin));
        return rutina;

    }

    public async Task<Guid> GetUidRutinaPorUidDia(Guid UidDia)
    {
        string sql=@"Select ""UidRutina"" from ""DiaRutina"" where ""Uid""=@Uid";
        using var connection = await _connectionFactory.CrearConexion();
        return await connection.QueryFirstOrDefaultAsync<Guid>(sql,new{Uid=UidDia});
    }

    public async Task<bool> ExisteRutinaEnEsaFecha(Guid UidUsuario,DateTime FechaInicio, DateTime FechaFin)
    {
        string sql=@"Select count(1) from ""Rutinas"" where
                    ""FechaInicio""<=@FechaFin and
                    ""FechaFin"">=@FechaInicio and
                    ""UidUsuario""=@Uid";
        using var connection= await _connectionFactory.CrearConexion();
        int resultado=await connection.ExecuteScalarAsync<int>(sql,new {FechaInicio,FechaFin,Uid=UidUsuario});
        return resultado>0;
    }
        public async Task<RutinaHomeDto> ObtenerDatosHomePageRutina(Guid UidUsuario)
        {
            string sql=@"Select ""Uid"" UidRutina, ""Nombre"" NombreRutina from ""Rutinas"" 
            where ""UidUsuario""=@UidUsuario and
            ""FechaInicio""<=NOW() and 
            ""FechaFin"">NOW()
            ";
            using var connection=await _connectionFactory.CrearConexion();
            return await connection.QueryFirstOrDefaultAsync<RutinaHomeDto>(sql,new{UidUsuario});
        }
        public async Task<IEnumerable<DiaRutinaHomeDto>> ObtenerDatosHomePageDiaRutina(Guid UidRutina)
        {
             string sql=@"select ""Uid"" UidDia,""Nombre"" NombreDiaRutina, ""DiaDeLaSemana"" from ""DiaRutina"" 
                    where ""UidRutina""=@UidRutina";
            using var connection=await _connectionFactory.CrearConexion();
            return await connection.QueryAsync<DiaRutinaHomeDto>(sql,new{UidRutina});
        }
        public async Task<IEnumerable<EjercicioDiaRutinaHomeDto>> ObtenerDatosHomePageEjercicioDiaRutina(Guid UidDia)
        {
            string sql=@"select eb.""Nombre"" Ejercicio, edr.""Series"", edr.""ObjetivoReps"" 
            from ""EjerciciosDiaRutina"" edr join ""EjerciciosBase"" eb
            on ""IdEjercicio""=edr.""UidEjercicios""
            where ""UidDia""=@UidDia ";
            using var connection=await _connectionFactory.CrearConexion();
            return await connection.QueryAsync<EjercicioDiaRutinaHomeDto>(sql,new{UidDia});
        }
        public async Task<Guid>ObtenerUidRutinaPorUidDia(Guid UidDia)
        {
            string sql=@"select ""UidRutina"" from ""DiaRutina"" where ""Uid""=@UidDia";
            using var connection=await _connectionFactory.CrearConexion();
            return await connection.QueryFirstOrDefaultAsync<Guid>(sql,new {UidDia});
        }

    public async Task<List<Rutina>> ObtenerMesociclosUsuario(Guid UidUsuario)
    {
        string sql=@"Select
        ""Uid"",
        ""UidUsuario"",
        ""Nombre"",
        ""FechaInicio"",
        ""FechaFin""
         from ""Rutinas""
          where ""UidUsuario""=@UidUsuario
          order by ""FechaInicio"" DESC";
        using var connection=await _connectionFactory.CrearConexion();
        var rutinas=await connection.QueryAsync<RutinaDto>(sql,new {UidUsuario});
        List<Rutina> ret=new List<Rutina>();
        foreach(var rutina in rutinas)
        {
            Rutina elemento=Rutina.CrearFromDataBase
            (
            rutina.Uid,
            rutina.UidUsuario,
            rutina.Nombre,
            DateOnly.FromDateTime(rutina.FechaInicio),
            DateOnly.FromDateTime(rutina.FechaFin)
            );
            ret.Add(elemento);
        }
        return ret;


    }
    // public async Task EstablecerCurrent(Guid UidUsuario,CancellationToken cancellationToken=default)
    // {
    //     string sql=@"update ""Rutinas""
    //      set ""Current""= case
    //      when ""Uid""=(
    //      select ""Uid""
    //      From ""Rutinas""
    //      Where ""UidUsuario""=@UidUsuario
    //     and ""FechaInicio""<=NOW() and ""FechaFin"">NOW()
    //     order by ""FechaInicio""
    //     limit 1
    //      )then TRUE
    //      else FALSE
    //      end
    //     Where ""UidUsuario""=@UidUsuario";
    //     using var connection= await _connectionFactory.CrearConexion();
    //     await connection.ExecuteAsync(sql, new {UidUsuario});
    // }
}