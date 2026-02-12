using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Usuarios.Errors;

public static class FechaNacimientoErrors
{
    public static readonly Error FechaInvalida=new Error("FechaNacimiento.Invalida","La fecha de nacimiento no puede ser mas alta ni igual que hoy");
}