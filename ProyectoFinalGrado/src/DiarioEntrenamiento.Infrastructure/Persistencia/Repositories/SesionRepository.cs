using System.Data.SqlTypes;
using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Sesiones.IniciarSesionEntrenamiento;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.DTOs;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

public class SesionRepository : ISesionRepository
{

    private readonly ISqlConnectionFactory _connectionFactory;
    private IUnitOfWork _uow;

    public SesionRepository(ISqlConnectionFactory connectionFactory, IUnitOfWork uow)
    {
        _connectionFactory = connectionFactory;
        _uow = uow;
    }

    public async Task<Guid?> ExisteSesion(Guid UidRutina,Guid UidDia,DateTime fecha)
    {
        string sql=@"select ""Uid"" from ""RegistroDatosSesion"" where 
                    ""UidRutina""=@UidRutina and 
                    ""UidDia""=@UidDia and
                    ""FechaSesion""=@Fecha";
        var parametros=new
        {
            UidRutina=UidRutina,
            UidDia=UidDia,
            Fecha=fecha.Date
            
        };
        using var connection=await _connectionFactory.CrearConexion();
        return await connection.QueryFirstOrDefaultAsync<Guid?>(sql,parametros);
        

    }

    public async Task<Guid> GetUidSesionPor(Guid UidDia)
    {
        string sql=@"select ""Uid""
                            from ""RegistroDatosSesion""
                            where 
                            ""UidDia"" = @UidDia
                            and (
                            ""FechaSesion"" = CURRENT_DATE
                            or ""FechaSesion"" = CURRENT_DATE + INTERVAL '1 day')";// por si alguien inicia un entrenamiento a las 23:30 y hace registros mas tarde de las 00:00
        using var connection=await _connectionFactory.CrearConexion();
        return await connection.QueryFirstOrDefaultAsync<Guid>(sql,new{UidDia});
    }

    public async Task InsertSesion(Sesion sesion, CancellationToken cancellationToken = default)
    {
        string sql1=@"insert into ""RegistroDatosSesion""
                    (
                    ""Uid"",
                    ""UidUsuario"",
                    ""UidRutina"",
                    ""UidDia"",
                    ""FechaSesion""
                    )values
                    (
                    @Uid,
                    @UidUsuario,
                    @UidRutina,
                    @UidDia,
                    @FechaSesion
                    )";
        string sql2=@"insert into ""RegistroEstadoUsuarioSesion""
                    (
                    ""UidRegistroDatosSesion"",
                    ""Sueno"",
                    ""Motivacion"",
                    ""ERP""
                    )values
                    (
                    @Uid,
                    @Sueno,
                    @Motivacion,
                    @ERP
                    )";
        var parametros1 =new
        {
         Uid=sesion.Uid,
         UidUsuario=sesion.UidUsuario,
         UidRutina=sesion.UidRutina,
         UidDia=sesion.UidDia,
         FechaSesion=sesion.FechaSesion   
        };
        var parametros2=new
        {
            Uid=sesion.Uid,
            Sueno=sesion.Estado.Sueno,
            Motivacion=sesion.Estado.Motivacion,
            ERP=sesion.Estado.ERP

        };
        using var connection=await _connectionFactory.CrearConexion();
        await connection.ExecuteAsync(sql1,parametros1);
        await connection.ExecuteAsync(sql2,parametros2);
    }

    public async Task<List<Sesion>> ObtenerSesionesCompletasPorUidSerie(Guid UidUsuario, List<Guid> UidEjercicio)
    {
        string sql = @"SELECT 
    rds.""Uid"",                    
    rds.""UidUsuario"",             
    rds.""UidRutina"",              
    rds.""UidDia"",                 
    rds.""FechaSesion"",            
    rdse.""Uid"" AS UidSerie,       
    rdse.""UidEjercicio"",        
    rdse.""UidRegistroDatosSesion"" AS UidSesion, 
    rdse.""Peso"",                  
    rdse.""Repeticiones"",          
    rdse.""Rir"",                   
    rdse.""Serie"" 
FROM
    ""RegistroDatosSesion"" rds
JOIN
    ""RegistroDatosSesionEjercicio"" rdse
    ON rds.""Uid"" = rdse.""UidRegistroDatosSesion""
WHERE
    rds.""UidUsuario"" = @UidUsuario
    AND rdse.""Serie"" = 1
    AND rdse.""UidEjercicio"" = ANY(@UidEjercicio); ";
        var connection = await _connectionFactory.CrearConexion();
        IEnumerable<DTOs.SesionDto> Sesiones = await connection.QueryAsync<DTOs.SesionDto>(sql, new { UidUsuario, UidEjercicio });
        Dictionary<Guid, SesionBuilder> dict = new Dictionary<Guid, SesionBuilder>();
        foreach (var s in Sesiones)
        {
            if (!dict.TryGetValue(s.Uid, out var builder))
            {
                builder = new SesionBuilder
                {
                    Uid = s.UidSesion,
                    UidUsuario = s.UidUsuario,
                    UidRutina = s.UidRutina,
                    UidDia = s.UidDia,
                    FechaSesion = s.FechaSesion,
                    serie = new List<SerieRealizada>()

                };
                dict.Add(s.Uid, builder);
            }
            SerieRealizada serie = SerieRealizada.CrearFromDataBase
            (
                s.UidSerie,
                s.UidEjercicio,
                s.UidSesion,
                s.Peso,
                s.Repeticiones,
                s.Rir,
                s.Serie
            );
            builder.serie.Add(serie);
        }
        List<Sesion> ret = new List<Sesion>(dict.Count);
        foreach (var s in dict.Values)
        {
            Sesion sesion = Sesion.CrearFromDataBaseConSeries(
                s.Uid,
                s.UidUsuario,
                s.UidRutina,
                s.UidDia,
                s.FechaSesion,
                s.serie
            );
            ret.Add(sesion);
        }
        return ret;
        
    }

    public async Task<IEnumerable<UltimaSesionDto>> ObtenerSesionRecienHecha(Guid UidDia)
    {
                string sql=@"select e.""Nombre"" Ejercicio,
                            e.""IdEjercicio"",
                            rdse.""Peso"",
                            rdse.""Repeticiones"",
                            rdse.""Rir"",
                            rdse.""Serie"",
                            rds.""FechaSesion"" from 
        ""EjerciciosBase"" e join ""RegistroDatosSesionEjercicio"" rdse 
        on e.""IdEjercicio""=rdse.""UidEjercicio"" join ""RegistroDatosSesion"" rds
        on rdse.""UidRegistroDatosSesion""=rds.""Uid""
        Where
        rds.""FechaSesion""=(select max(""FechaSesion"") from ""RegistroDatosSesion""
        where ""UidDia""=@UidDia) and rds.""UidDia""=@UidDia
        and rdse.""Peso"" is not null
        and rdse.""Repeticiones"" is not null
        and rdse.""Rir"" is not null";
        using var connection= await _connectionFactory.CrearConexion();
        return await connection.QueryAsync<UltimaSesionDto>(sql,new {UidDia});
    }

    public async Task<IEnumerable<UltimaSesionDto>> ObtenerUltimaSesion(Guid UidDia)
    {
        string sql=@"select e.""Nombre"" Ejercicio,
                            e.""IdEjercicio"",
                            rdse.""Peso"",
                            rdse.""Repeticiones"",
                            rdse.""Rir"",
                            rdse.""Serie"",
                            rds.""FechaSesion"" from 
        ""EjerciciosBase"" e join ""RegistroDatosSesionEjercicio"" rdse 
        on e.""IdEjercicio""=rdse.""UidEjercicio"" join ""RegistroDatosSesion"" rds
        on rdse.""UidRegistroDatosSesion""=rds.""Uid""
        Where
        rds.""FechaSesion""=(select max(""FechaSesion"") from ""RegistroDatosSesion""
        where ""UidDia""=@UidDia and ""FechaSesion""< CURRENT_DATE) and rds.""UidDia""=@UidDia
        and rdse.""Peso"" is not null
        and rdse.""Repeticiones"" is not null
        and rdse.""Rir"" is not null";
        using var connection= await _connectionFactory.CrearConexion();
        return await connection.QueryAsync<UltimaSesionDto>(sql,new {UidDia});
    }


    public async Task Update(Guid? UidSesion,Sesion sesion ,CancellationToken cancellationToken = default)
    {
        string sql=@"update ""RegistroEstadoUsuarioSesion"" set ""Sueno""=@Sueno,""Motivacion""=@Motivacion,""ERP""=@ERP
        where ""UidRegistroDatosSesion""=@UidSesion";
        using var connection=await _connectionFactory.CrearConexion();
        var parametros = new
        {
            UidSesion=UidSesion,
            Sueno=sesion.Estado.Sueno,
            Motivacion=sesion.Estado.Motivacion,
            ERP=sesion.Estado.ERP
        };
        await connection.ExecuteAsync(sql,parametros);
    }
}