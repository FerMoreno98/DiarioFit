using DiarioEntrenamiento.Application.Usuarios.CrearUsuario;
using FluentValidation;

namespace DiarioEntrenamiento.Application.Rutinas.CrearRutina;

public class CrearRutinaCommandValidator : AbstractValidator<CrearRutinaCommand>
{
    public CrearRutinaCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty();
        RuleFor(c => c.FechaIncio).LessThan(c => c.FechaFin);
    }
}