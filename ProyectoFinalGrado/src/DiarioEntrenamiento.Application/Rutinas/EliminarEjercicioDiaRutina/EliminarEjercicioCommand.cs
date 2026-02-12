
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.EliminarEjercicioDiaRutina;

public sealed record EliminarEjercicioCommand(Guid UidEjercicioDiaRutina) : ICommand<Unit>;