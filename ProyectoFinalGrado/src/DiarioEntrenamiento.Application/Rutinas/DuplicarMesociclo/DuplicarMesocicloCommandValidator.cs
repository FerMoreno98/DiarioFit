using FluentValidation;

namespace DiarioEntrenamiento.Application.Rutinas.DuplicarMesociclo;

public class DuplicarMesocicloCommandValidator : AbstractValidator<DuplicarMesocicloCommand>
{
    public DuplicarMesocicloCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty();
        RuleFor(c => c.FechaInicio).LessThan(c => c.FechaFin);
    }
}