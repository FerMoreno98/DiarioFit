using FluentValidation;

namespace DiarioEntrenamiento.Application.Rutinas.AgregarDiaRutina;

public class AgregarDiaRutinaCommandValidator : AbstractValidator<AgregarDiaRutinaCommand>
{
    public AgregarDiaRutinaCommandValidator()
    {
        RuleFor(c=>c.Nombre).NotEmpty();
        RuleFor(c=> c.DiaDeLaSemana).NotEmpty();
    }
}