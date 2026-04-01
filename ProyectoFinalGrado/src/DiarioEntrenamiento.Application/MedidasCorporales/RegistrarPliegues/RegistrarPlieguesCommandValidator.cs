
using FluentValidation;

namespace DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPliegues;

public class RegistrarPlieguesCommandValidator : AbstractValidator<RegistrarPlieguesCommand>
{
    public RegistrarPlieguesCommandValidator()
    {
        RuleFor(x => x.UidUsuario)
            .NotEmpty().WithMessage("El usuario es obligatorio.");

        RuleFor(x => x.Abdominal)
            .NotNull().WithMessage("El pliegue abdominal es obligatorio.")
            .GreaterThan(0).WithMessage("El pliegue abdominal debe ser mayor que 0.");
            
        RuleFor(x => x.Suprailiaco)
            .NotNull().WithMessage("El pliegue suprailiaco es obligatorio.")
            .GreaterThan(0).WithMessage("El pliegue suprailiaco debe ser mayor que 0.");

        RuleFor(x => x.Tricipital)
            .NotNull().WithMessage("El pliegue tricipital es obligatorio.")
            .GreaterThan(0).WithMessage("El pliegue tricipital debe ser mayor que 0.");

        RuleFor(x => x.Subescapular)
            .NotNull().WithMessage("El pliegue subescapular es obligatorio.")
            .GreaterThan(0).WithMessage("El pliegue subescapular debe ser mayor que 0.");

        RuleFor(x => x.Muslo)
            .NotNull().WithMessage("El pliegue del muslo es obligatorio.")
            .GreaterThan(0).WithMessage("El pliegue del muslo debe ser mayor que 0.");

        RuleFor(x => x.Pantorrilla)
            .NotNull().WithMessage("El pliegue de la pantorrilla es obligatorio.")
            .GreaterThan(0).WithMessage("El pliegue de la pantorrilla debe ser mayor que 0.");
    }

}
