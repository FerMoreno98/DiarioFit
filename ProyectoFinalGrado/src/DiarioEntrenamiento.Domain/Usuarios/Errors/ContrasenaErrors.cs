using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Usuarios.Errors;

public static class ContrasenaErrors
{
    public static readonly Error ComplejidadInvalida = new Error("Contrasena.Invalida", "La contraseña tiene que tener por lo menos 8 caracteres, una mayuscula, una minuscula y un numero");
    public static readonly Error Empty = new Error("Contrasena.Empty", "La contrasena no puede estar vacía");
}