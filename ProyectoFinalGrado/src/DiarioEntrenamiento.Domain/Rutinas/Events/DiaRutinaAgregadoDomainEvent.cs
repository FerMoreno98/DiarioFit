using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Rutinas.Events;

public sealed record DiaRutinaAgregadoDomainEvent(Guid IdRutina,Guid IdDia,string DiaDeLaSemana) : IDomainEvents;