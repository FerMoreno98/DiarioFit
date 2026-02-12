using FluentValidation;

namespace DiarioEntrenamiento.Application.Usuarios.ModificarUsuario;

public class ModificarUsuarioCommandValidator : AbstractValidator<ModificarUsuarioCommand>
{
    public ModificarUsuarioCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty();
        RuleFor(c => c.Apellidos).NotEmpty();
        RuleFor(c => c.FechaNacimiento).NotEmpty();
        RuleFor(c => c.FechaNacimiento).LessThan(DateOnly.FromDateTime(DateTime.Now));
    }
}