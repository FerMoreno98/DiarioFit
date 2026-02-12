using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Usuarios.Errors;

public static class EmailErrors
{
    public static readonly Error Empty = new Error("Email.Empty", "El email no puede estar vacío");
    public static readonly Error FormatoInvalido = new Error("Email.Format", "El email no tiene un formato válido");
    public static readonly Error EmailEnUso = new Error("Email.Usado", "El email esta actualmente en uso");
}