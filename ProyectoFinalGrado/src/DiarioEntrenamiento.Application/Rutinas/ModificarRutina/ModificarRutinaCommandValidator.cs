using FluentValidation;

namespace DiarioEntrenamiento.Application.Rutinas.ModificarRutina;

public class ModificarRutinaCommandValidator : AbstractValidator<ModificarRutinaCommand>
{
    public ModificarRutinaCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty();
        RuleFor(c => c.FechaInicio).LessThan(c => c.FechaFin);
    }
}