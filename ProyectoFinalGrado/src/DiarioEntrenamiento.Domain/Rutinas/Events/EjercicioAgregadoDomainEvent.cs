using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Rutinas.Events;

public sealed record EjercicioAgregadoDomainEvent(Guid IdRutina,Guid IdDia,Guid EjercicioId,int orden) : IDomainEvents;