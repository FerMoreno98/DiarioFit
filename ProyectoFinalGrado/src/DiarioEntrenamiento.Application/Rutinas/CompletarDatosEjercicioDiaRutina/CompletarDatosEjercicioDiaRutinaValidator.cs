using FluentValidation;

namespace DiarioEntrenamiento.Application.Rutinas.CompletarDatosEjercicioDiaRutina;

public class CompletarDatosEjercicioDiaRutinaValidator : AbstractValidator<CompletarDatosEjercicioDiaRutinaCommand>
{
    public CompletarDatosEjercicioDiaRutinaValidator()
    {
        RuleFor(c => c.Series).NotEqual(0);
        RuleFor(c => c.RangoReps).NotEmpty();
        RuleFor(c=>c.RangoRIR).NotEmpty();
        RuleFor(c=>c.TiempoDeDescanso).NotEmpty();
        RuleFor(c=>c.orden).NotEqual(0);
    }
}