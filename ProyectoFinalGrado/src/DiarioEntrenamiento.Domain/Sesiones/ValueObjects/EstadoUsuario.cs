using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Sesiones.Errors;

namespace DiarioEntrenamiento.Domain.Sesiones.ValueObjects;

public record EstadoUsuario 
{
    private EstadoUsuario(int? sueno, int? motivacion, int? eRP)
    {
        Sueno = sueno;
        Motivacion = motivacion;
        ERP = eRP;
    }


    public int? Sueno{get;init;}
    public int? Motivacion{get;init;}
    public int? ERP{get;init;}

    public static Result<EstadoUsuario> Crear(int? sueno, int? motivacion, int? eRP)
    {

        if (sueno is not null && (sueno < 0 || sueno > 10))
            return Result.Failure<EstadoUsuario>(SesionErrors.SuenoFueraDeRango);

        if (motivacion is not null && (motivacion < 0 || motivacion > 10))
            return Result.Failure<EstadoUsuario>(SesionErrors.MotivacionFueraDeRango);

        if (eRP is not null && (eRP < 0 || eRP > 10))
            return Result.Failure<EstadoUsuario>(SesionErrors.ERPFueraDeRango);

        return Result.Success(new EstadoUsuario(sueno, motivacion, eRP));
    }
}