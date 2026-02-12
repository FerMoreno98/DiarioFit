using DiarioEntrenamiento.Application.Abstractions.Email;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.Events;
using MediatR;

namespace DiarioEntrenamiento.Application.Usuarios.CrearUsuario;

internal sealed class CrearUsuarioDomainEventHandler : INotificationHandler<UsuarioCreadoDomainEvent>
{

    private readonly IEmailService _emailService;

    public CrearUsuarioDomainEventHandler( IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UsuarioCreadoDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = notification.Email;
   
        await _emailService.SendAsync(email, "Usuario creado", "Tu usuario ha sido creado correctamente");
    }
}