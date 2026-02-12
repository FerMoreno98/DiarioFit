using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Usuarios.Events;

public sealed record UsuarioCreadoDomainEvent(string Email) : IDomainEvents;