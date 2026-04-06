using System.Text.RegularExpressions;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Sesiones.Errors;

namespace DiarioEntrenamiento.Domain.Sesiones.Entidad;

public sealed class SerieRealizada : Entity<Guid>
{
    private SerieRealizada(Guid uid, Guid uidEjercicio, Guid uidSesion, decimal? peso, int? repeticiones, string? rIR, int serie) : base(uid)
    {
        Uid = uid;
        UidEjercicio = uidEjercicio;
        UidSesion = uidSesion;
        Peso = peso;
        Repeticiones = repeticiones;
        RIR = rIR;
        Serie = serie;
    }

    public Guid Uid { get; private set; }
    public Guid UidEjercicio { get; private set; }
    public Guid UidSesion { get; private set; }
    public decimal? Peso { get; private set; }
    public int? Repeticiones { get; private set; }
    public string? RIR { get; private set; }
    public int Serie { get; private set; }
    public decimal RMCalculado => Math.Round((decimal)(Peso *
                    (1 + (Repeticiones + rirMedio) / 30m)), 2);
    public decimal? rirMedio =>
    !string.IsNullOrEmpty(RIR)
        ? (decimal)RIR.Split('-')
            .Select(v => int.Parse(v.Trim()))
            .Average()
        : 0;


    public static Result<SerieRealizada> Crear(Guid uidEjercicio, Guid uidSesion, decimal? peso, int? repeticiones, string? rIR, int serie)
    {
        const string patron = @"^\d+(-\d+)?$";
        if (rIR is not null)
        {
            if (!Regex.IsMatch(rIR, patron))
                return Result.Failure<SerieRealizada>(SerieErrors.FormatoInvalidoRir);
        }

        return new SerieRealizada(Guid.NewGuid(), uidEjercicio, uidSesion, peso, repeticiones, rIR, serie);
    }
    public static SerieRealizada CrearFromDataBase(Guid Uid,Guid uidEjercicio, Guid uidSesion, decimal? peso, int? repeticiones, string? rIR, int serie)
    {
        return new SerieRealizada(Uid, uidEjercicio, uidSesion, peso, repeticiones, rIR, serie);
    }

}