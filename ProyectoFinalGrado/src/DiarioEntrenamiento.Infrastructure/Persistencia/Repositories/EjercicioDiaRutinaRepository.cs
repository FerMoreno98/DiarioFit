using System.Collections;
using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class EjercicioDiaRutinaRepository : IEjercicioDiaRutinaRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IUnitOfWork _UOW;

    public EjercicioDiaRutinaRepository(ISqlConnectionFactory connectionFactory, IUnitOfWork uOW)
    {
        _connectionFactory = connectionFactory;
        _UOW = uOW;
    }

    public async Task AddAsync(EjercicioDiaRutina ejercicio, CancellationToken cancellationToken)
    {
        string sql=@"insert into ""EjerciciosDiaRutina""
                    (""Uid"",""UidDia"",""UidEjercicios"",""Orden"",""Series"",""ObjetivoReps"",""ObjetivoRIR"",""TiempoDescanso"")
                    values(@Uid,@UidDia,@UidEjercicios,@Orden,@Series,@ObjetivoReps,@ObjetivoRIR,@TiempoDescanso)";
        var parametros = new
        {
            Uid=ejercicio.Id,
            UidDia=ejercicio.UidDia,
            UidEjercicios=ejercicio.EjercicioUid,
            Orden=ejercicio.Orden,
            Series=ejercicio.Datos.Series,
            ObjetivoReps=ejercicio.Datos.RangoRepsObjetivo,
            ObjetivoRIR=ejercicio.Datos.RangoRIR,
            TiempoDescanso=ejercicio.Datos.Descanso
        };
        if(_UOW.Transaction is not null)
        {
           await _UOW.Connection.ExecuteAsync(new CommandDefinition(sql,parametros,_UOW.Transaction,cancellationToken:cancellationToken) );
        }else{
        using var connection= await _connectionFactory.CrearConexion();
        int result= await connection.ExecuteAsync(sql, parametros);
        }
        
    }

    public async Task AddVariosAsync(List<EjercicioDiaRutina> ejercicios, CancellationToken cancellationToken)
    {
        string sql=@"insert into ""EjerciciosDiaRutina""
                    (""Uid"",""UidDia"",""UidEjercicios"",""Orden"",""Series"",""ObjetivoReps"",""ObjetivoRIR"",""TiempoDescanso"")
                    values(@Id,@UidDia,@EjercicioUid,@Orden,@Series,@RangoRepsObjetivo,@RangoRIR,@Descanso)";
        var parametros=ejercicios.Select(e=> new
        {
            Id=e.Id,
            UidDia=e.UidDia,
            EjercicioUid=e.EjercicioUid,
            Orden=e.Orden,
            Series=e.Datos.Series,
            RangoRepsObjetivo=e.Datos.RangoRepsObjetivo,
            RangoRIR=e.Datos.RangoRIR,
            Descanso=e.Datos.Descanso
        });
        if(_UOW.Transaction is not null)
        {
            await _UOW.Connection.ExecuteAsync(new CommandDefinition(sql,parametros,_UOW.Transaction,cancellationToken:cancellationToken));
        }
        else
        {
            var connection=await _connectionFactory.CrearConexion();
            await connection.ExecuteAsync(sql,ejercicios);
        }
    }

    public async Task DeleteAsync(Guid uid, CancellationToken cancellationToken)
    {
        string sql=@"delete from ""EjerciciosDiaRutina"" where ""Uid""=@uid";
        using var connection = await _connectionFactory.CrearConexion();
        await connection.ExecuteAsync(sql,new{uid});
    }
    public async Task DeleteVariosAsync(List<Guid> UidEjerciciosDia, CancellationToken cancellationToken)
    {
        string sql=@"delete from ""EjerciciosDiaRutina"" where ""Uid"" = ANY(@Uids)";//por algun motivo in no le vale cuando paso la consulta por dapper
        using var connection=await _connectionFactory.CrearConexion();
        await connection.ExecuteAsync(sql,new {Uids=UidEjerciciosDia});
    }

    public async Task<bool> EsEjercicioRepetido(Guid Uid,Guid UidDia, Guid UidEjercicio,string Repeticiones,string Rir)
    {
       string sql=@"select count(1) from ""EjerciciosDiaRutina"" where 
                    ""UidDia""=@UidDia and 
                    ""UidEjercicios""=@UidEjercicio and
                    ""ObjetivoReps""=@Repeticiones and
                    ""ObjetivoRIR""=@Rir
                    and ""Uid""!=@Uid";
       using var connection=await _connectionFactory.CrearConexion();
       int resultado=await connection.ExecuteScalarAsync<int>(sql,new{Uid,UidDia,UidEjercicio,Repeticiones,Rir});
       return resultado>0;

    }

    // public async Task<bool> EsOrdenRepetido(Guid UidDia, int orden)
    // {
    //     string sql=@"select ""Orden"" from ""EjerciciosDiaRutina"" where ""UidDia""=@UidDia";
    //     using var connection=await _connectionFactory.CrearConexion();
    //     var ordenes=await connection.QueryAsync<int>(sql,new {UidDia});
    //     foreach(var ord in ordenes)
    //     {
    //         if (ord == orden)
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    public async Task<IEnumerable<EjercicioDiaRutinaDTO>> GetByIdDia(Guid id, CancellationToken cancellationToken)
    {
        string sql=@"Select 
                    ""Uid"" UidEjercicioDiaRutina,
                    ""UidDia"",
                    ""UidEjercicios"",
                    ""Series"",
                    ""ObjetivoReps"",
                    ""ObjetivoRIR"",
                    ""TiempoDescanso"",
                    ""Orden""
                     from ""EjerciciosDiaRutina"" 
                      where ""UidDia""=@Uid order by ""Orden""";
            var connection=await _connectionFactory.CrearConexion();
            var resultado=await connection.QueryAsync<EjercicioDiaRutinaDTO>(sql,new {Uid=id});
            return resultado;
        }
    public async Task<IEnumerable<string>> GetNombreEjercicioByIdDia(Guid idDia)
    {
        string sql=@"Select 
                    eb.""Nombre""
                     from ""EjerciciosDiaRutina""edr join ""EjerciciosBase"" eb on edr.""UidEjercicios""=eb.""IdEjercicio"" 
                      where ""UidDia""=@Uid order by ""Orden""";
            var connection=await _connectionFactory.CrearConexion();
            var resultado=await connection.QueryAsync<string>(sql,new {Uid=idDia});
            return resultado;
    }
    // Esta query solo funciona si no permito crear dos ejercicios iguales el mismo dia
    public async Task<int> GetSeriesPlanificadas(Guid UidDia,Guid UidEjercicio)
    {
        string sql=@"select ""Series"" from ""EjerciciosDiaRutina"" where ""UidDia""=@UidDia and ""UidEjercicios""=@UidEjercicio";
        using var connection =await _connectionFactory.CrearConexion();
        var resultado=await connection.QueryFirstOrDefaultAsync<int>(sql,new {UidDia,UidEjercicio});
        return resultado;
    }

    public async Task ModificarAsync(EjercicioDiaRutina ejercicio, CancellationToken cancellationToken)
    {
        string sql=@"update ""EjerciciosDiaRutina"" 
                    set ""Orden""=@Orden,
                     ""Series""=@Series,
                     ""ObjetivoReps""=@ObjetivoReps,
                     ""ObjetivoRIR""=@ObjetivoRIR,
                     ""TiempoDescanso""=@TiempoDeDescanso
                     where ""Uid""=@Uid";
        var parametros = new
        {
         Orden=ejercicio.Orden,
         Series=ejercicio.Datos.Series,
         ObjetivoReps=ejercicio.Datos.RangoRepsObjetivo,
         ObjetivoRIR=ejercicio.Datos.RangoRIR,
         TiempoDeDescanso=ejercicio.Datos.Descanso, 
         Uid=ejercicio.Id  
        };
        using var connection=await _connectionFactory.CrearConexion();
        await connection.ExecuteAsync(sql,parametros);
    }

    public async Task<IEnumerable<EjercicioDiaRutinaDTO>> ObtenerEjerciciosDiaConNombre(Guid UidDia)
    {
        string sql=@"Select 
                ""Uid"" UidEjercicioDiaRutina,
                ""UidDia"",
                ""UidEjercicios"",
                eb.""Nombre"",
                ""Series"",
                ""ObjetivoReps"",
                ""ObjetivoRIR"",
                ""TiempoDescanso"",
                ""Orden""
                from ""EjerciciosDiaRutina"" edr join ""EjerciciosBase"" eb on edr.""UidEjercicios""=eb.""IdEjercicio"" 
                    where ""UidDia""=@UidDIa order by ""Orden""";
        using var connection=await _connectionFactory.CrearConexion();
        return await connection.QueryAsync<EjercicioDiaRutinaDTO>(sql,new {UidDia});
    }

    public async Task<IEnumerable<DatosGraficaGruposMuscularesDto>> ObtenerEjerciciosRutina(Guid UidRutina)
    {
        string sql=@"select edr.""UidEjercicios"", edr.""Series"",es.""idSubGrupoMuscular""
        from ""DiaRutina"" dr join ""EjerciciosDiaRutina"" edr on
         dr.""Uid""=edr.""UidDia"" join ""EjercicioBase-SubgrupoMuscular"" es on edr.""UidEjercicios""=es.""idEjercicioBase""
         where dr.""UidRutina""=@UidRutina";
         using var connection=await _connectionFactory.CrearConexion();
         return await connection.QueryAsync<DatosGraficaGruposMuscularesDto>(sql, new {UidRutina});


    }

    public async Task<IEnumerable<Guid>> ObtenerUidEjerciciosDeDiaRutina(Guid UidDia)
    {
        string sql=@"select ""UidEjercicios"" from ""EjerciciosDiaRutina"" where ""UidDia""=@UidDia";
        using var connection= await _connectionFactory.CrearConexion();
        return await connection.QueryAsync<Guid>(sql,new{UidDia});

    }
}