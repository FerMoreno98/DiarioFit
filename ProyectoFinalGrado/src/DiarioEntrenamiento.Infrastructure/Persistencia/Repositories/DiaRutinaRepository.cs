using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;
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

    public Task ModificarAsync(Guid uid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public async Task<DiaRutina> GetDiaByIdWithEjerciciosAsync(Guid UidDia)
    {
        string sql1=@"Select 
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
        string sql2=@"Select ""Uid"", ""UidRutina"", ""Nombre"", ""DiaDeLaSemana"" from ""DiaRutina"" where ""Uid""=@Uid";
        
            using (var connection=await _sqlConnectionFactory.CrearConexion())
            {
                var resultado=await connection.QueryAsync<EjercicioDiaRutinaDTO>(sql1,new {Uid=UidDia});
                var resultadoDia=await connection.QueryFirstOrDefaultAsync<DiaRutinaDto>(sql2,new{Uid=UidDia});
            
           
            List<EjercicioDiaRutina> ret=new List<EjercicioDiaRutina>();
            foreach (var dato in resultado)
            {
                EjercicioDiaRutina ejercicio=EjercicioDiaRutina.CrearFromDataBase
                (
                    dato.UidEjercicioDiaRutina,
                    dato.UidEjercicios,
                    dato.UidDia,
                    dato.Orden,
                    dato.Series,
                    dato.ObjetivoReps,
                    dato.ObjetivoRIR,
                    dato.TiempoDescanso
                ).Value;
                ret.Add(ejercicio);
            }
            DiaRutina result=DiaRutina.CargarDiaRutinaWithEjercicio(resultadoDia.Uid,resultadoDia.UidRutina,resultadoDia.Nombre,resultadoDia.DiaDeLaSemana,ret).Value;
            return result;
            }
    }


    public async Task<List<DiaRutina>> GetDiasDeRutinaWithEjercicios(Guid uidRutina)
    {
        const string sql = @"
            SELECT
                d.""Uid""              AS UidDia,
                d.""UidRutina"",
                d.""Nombre"",
                d.""DiaDeLaSemana"",
                e.""Uid""              AS UidEjercicioDiaRutina,
                e.""UidDia""           AS UidDiaEjercicio,
                e.""UidEjercicios"",
                e.""Series"",
                e.""ObjetivoReps"",
                e.""ObjetivoRIR"",
                e.""TiempoDescanso"",
                e.""Orden""
            FROM ""DiaRutina"" d
            LEFT JOIN ""EjerciciosDiaRutina"" e
                ON e.""UidDia"" = d.""Uid""
            WHERE d.""UidRutina"" = @UidRutina
            ORDER BY d.""DiaDeLaSemana"", d.""Nombre"", e.""Orden"";";

        using var connection = await _sqlConnectionFactory.CrearConexion();

        var rows = await connection.QueryAsync<DiaEjercicioRow>(sql, new { UidRutina = uidRutina });

        var dict = new Dictionary<Guid, DiaBuilder>();

        foreach (var r in rows)
        {
            if (!dict.TryGetValue(r.UidDia, out var builder))
            {
                builder = new DiaBuilder
                {
                    Uid = r.UidDia,
                    UidRutina = r.UidRutina,
                    Nombre = r.Nombre,
                    DiaDeLaSemana = r.DiaDeLaSemana,
                    Ejercicios = new List<EjercicioDiaRutina>()
                };
                dict.Add(r.UidDia, builder);
            }

    
            var ejer = EjercicioDiaRutina.CrearFromDataBase(
                r.UidEjercicioDiaRutina,
                r.UidEjercicios,
                r.UidDiaEjercicio,
                r.Orden,
                r.Series,
                r.ObjetivoReps,
                r.ObjetivoRIR,
                r.TiempoDescanso
            ).Value;

                builder.Ejercicios.Add(ejer);
        }

        var ret = new List<DiaRutina>(dict.Count);

        foreach (var b in dict.Values)
        {
            var dia = DiaRutina.CargarDiaRutinaWithEjercicio(
                b.Uid,
                b.UidRutina,
                b.Nombre,
                b.DiaDeLaSemana,
                b.Ejercicios
            ).Value;

            ret.Add(dia);
        }

        return ret;
    }

    public async Task AddVariosAsync(List<DiaRutina> diasRutina, CancellationToken cancellationToken)
    {
        string sql=@"insert into ""DiaRutina""(""Uid"",""UidRutina"",""Nombre"",""DiaDeLaSemana"")
                    values(@Id,@Uid_Rutina,@Nombre,@DiaDeLaSemana)";
        if(_uow.Transaction is not null)
        {
            await _uow.Connection.ExecuteAsync(new CommandDefinition(sql,diasRutina,_uow.Transaction,cancellationToken:cancellationToken));
        }
        else
        {
            var connection=await _sqlConnectionFactory.CrearConexion();
            await connection.ExecuteAsync(sql,diasRutina);
        }
    }
}