using System.Data;
using FluentValidation;

namespace DiarioEntrenamiento.Application.Usuarios.CrearUsuario;

public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
{
    public CrearUsuarioCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Contrasena).NotEmpty();
        RuleFor(c => c.FechaNacimiento).LessThan(DateOnly.FromDateTime(DateTime.Now));
    }
}