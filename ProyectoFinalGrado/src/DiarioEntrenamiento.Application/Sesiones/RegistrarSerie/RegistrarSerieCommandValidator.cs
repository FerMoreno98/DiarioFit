using FluentValidation;

namespace DiarioEntrenamiento.Application.Sesiones.RegistrarSerie;

public class RegistrarSerieCommandValidator : AbstractValidator<RegistrarSerieCommand>
{
    public RegistrarSerieCommandValidator()
    {
        RuleFor(c=>c.Peso).GreaterThan(-1);
        RuleFor(c=>c.Repeticiones).GreaterThan(-1);
    }
}