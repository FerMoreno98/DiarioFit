
using FluentValidation;

namespace DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPerimetros;

public class RegistrarPerimetrosCommandValidator : AbstractValidator<RegistrarPerimetrosCommand>
{
    public RegistrarPerimetrosCommandValidator()
    {
 RuleFor(x => x.UidUsuario)
            .NotEmpty().WithMessage("El usuario es obligatorio.");

        RuleFor(x => x.Cuello)
            .NotEmpty().WithMessage("El cuello es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El cuello no puede ser menor que 0.");

        RuleFor(x => x.BrazoDchoRelajado)
            .NotEmpty().WithMessage("El brazo derecho relajado es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El brazo derecho relajado no puede ser menor que 0.");

        RuleFor(x => x.BrazoDchoTension)
            .NotEmpty().WithMessage("El brazo derecho en tensión es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El brazo derecho en tensión no puede ser menor que 0.");

        RuleFor(x => x.BrazoIzqRelajado)
            .NotEmpty().WithMessage("El brazo izquierdo relajado es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El brazo izquierdo relajado no puede ser menor que 0.");

        RuleFor(x => x.BrazoIzqTension)
            .NotEmpty().WithMessage("El brazo izquierdo en tensión es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El brazo izquierdo en tensión no puede ser menor que 0.");

        RuleFor(x => x.Pecho)
            .NotEmpty().WithMessage("El pecho es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El pecho no puede ser menor que 0.");

        RuleFor(x => x.Hombro)
            .NotEmpty().WithMessage("El hombro es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El hombro no puede ser menor que 0.");

        RuleFor(x => x.Cintura)
            .NotEmpty().WithMessage("La cintura es obligatoria.")
            .GreaterThanOrEqualTo(0).WithMessage("La cintura no puede ser menor que 0.");

        RuleFor(x => x.Cadera)
            .NotEmpty().WithMessage("La cadera es obligatoria.")
            .GreaterThanOrEqualTo(0).WithMessage("La cadera no puede ser menor que 0.");

        RuleFor(x => x.Abdomen)
            .NotEmpty().WithMessage("El abdomen es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El abdomen no puede ser menor que 0.");

        RuleFor(x => x.MusloDcho)
            .NotEmpty().WithMessage("El muslo derecho es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El muslo derecho no puede ser menor que 0.");

        RuleFor(x => x.MusloIzq)
            .NotEmpty().WithMessage("El muslo izquierdo es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El muslo izquierdo no puede ser menor que 0.");

        RuleFor(x => x.PantorrillaDcha)
            .NotEmpty().WithMessage("La pantorrilla derecha es obligatoria.")
            .GreaterThanOrEqualTo(0).WithMessage("La pantorrilla derecha no puede ser menor que 0.");

        RuleFor(x => x.PantorrillaIzq)
            .NotEmpty().WithMessage("La pantorrilla izquierda es obligatoria.")
            .GreaterThanOrEqualTo(0).WithMessage("La pantorrilla izquierda no puede ser menor que 0.");
    }
}
