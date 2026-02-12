using System.Text.RegularExpressions;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios.Errors;

namespace DiarioEntrenamiento.Domain.Usuarios.ValueObjects;

public record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<Email> Crear(string? value)
    {
        string normalizado=value.Trim().ToLowerInvariant();
        if (!esEmailValido(normalizado))
        {
            return Result.Failure<Email>(EmailErrors.FormatoInvalido);
        }

        return Result.Success(new Email(value));
    }
    
    private static bool esEmailValido(string email)
    {
        var pattern = @"^(?=.{1,254}$)(?=.{1,64}@)[\p{L}\p{N}!#$%&'*+/=?^_`{|}~.-]+@(?:[\p{L}\p{N}](?:[\p{L}\p{N}-]{0,61}[\p{L}\p{N}])?\.)+[\p{L}]{2,63}$";
        bool ok = Regex.IsMatch(email.Trim(), pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        return ok;

    }
}