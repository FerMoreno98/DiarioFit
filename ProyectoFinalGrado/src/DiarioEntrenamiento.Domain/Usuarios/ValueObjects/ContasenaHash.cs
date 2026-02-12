using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios.Errors;

namespace DiarioEntrenamiento.Domain.Usuarios.ValueObjects;

public record ContrasenaHash
{
    private ContrasenaHash(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<ContrasenaHash> Crear(string hashedValue)
    {
        return Result.Success(new ContrasenaHash(hashedValue));
    }
    
    public static Result ValidarComplejidad(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            return Result.Failure(ContrasenaErrors.Empty);

        bool ok = plainPassword.Length >= 8
                  && plainPassword.Any(char.IsUpper)
                  && plainPassword.Any(char.IsLower)
                  && plainPassword.Any(char.IsDigit);

        return ok
            ? Result.Success()
            : Result.Failure(ContrasenaErrors.ComplejidadInvalida);
    }
}