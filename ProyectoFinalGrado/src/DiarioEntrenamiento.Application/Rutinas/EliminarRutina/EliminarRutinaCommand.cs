using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.ELiminarRutina;

public sealed record EliminarRutinaCommand(Guid UidRutina) : ICommand<Unit>;