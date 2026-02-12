using System.Text.RegularExpressions;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas.Errors;

namespace DiarioEntrenamiento.Domain.Rutinas.ValueObjects;

public record DatosEjercicio
{
    public DatosEjercicio(int series, string rangoRepsObjetivo, string rangoRIR, int descanso)
    {
        Series = series;
        RangoRepsObjetivo = rangoRepsObjetivo;
        RangoRIR = rangoRIR;
        Descanso = descanso;
    }

    public int Series { get; init; }
    public string RangoRepsObjetivo { get; init; }
    public string RangoRIR { get; init; }
    public int Descanso{ get; init; }

public static Result<DatosEjercicio> Crear(
    int series,
    string rangoRepsObjetivo,
    string rangoRIR,
    int descanso)
{
    // Regex: "numero" o "numero-numero"
    const string patron = @"^\d+(-\d+)?$";

    if (!Regex.IsMatch(rangoRepsObjetivo, patron))
        return Result.Failure<DatosEjercicio>(RutinaErrors.FormatoInvalidoReps);

    if (!EsRangoNumericoValido(rangoRepsObjetivo))
        return Result.Failure<DatosEjercicio>(RutinaErrors.FormatoInvalidoReps);

    if (!Regex.IsMatch(rangoRIR, patron))
        return Result.Failure<DatosEjercicio>(RutinaErrors.FormatoInvalidoRir);

    if (!EsRangoNumericoValido(rangoRIR))
        return Result.Failure<DatosEjercicio>(RutinaErrors.FormatoInvalidoRir);


    var datos = new DatosEjercicio(series, rangoRepsObjetivo, rangoRIR, descanso);
    return Result.Success(datos);
}

private static bool EsRangoNumericoValido(string valor)
{
    var parts = valor.Split('-');

    if (parts.Length == 1)
    {
        return int.TryParse(parts[0], out var n) && n >= 0;
    }

    if (parts.Length == 2)
    {
        if (!int.TryParse(parts[0], out var a)) return false;
        if (!int.TryParse(parts[1], out var b)) return false;
        if (a < 0 || b < 0) return false;

        return a <= b;
    }

    return false;
}
}