using System.Data;
using FluentValidation;

namespace DiarioEntrenamiento.Application.Usuarios.PasswordResetRequest;

public class PasswordResetCommandValidator : AbstractValidator<PasswordResetRequestCommand>
{
    public PasswordResetCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty();
    }
}