using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using MediatR;

namespace DiarioEntrenamiento.Application.Usuarios.PasswordResetRequest;

public record PasswordResetRequestCommand(string Email) : ICommand<Unit>;