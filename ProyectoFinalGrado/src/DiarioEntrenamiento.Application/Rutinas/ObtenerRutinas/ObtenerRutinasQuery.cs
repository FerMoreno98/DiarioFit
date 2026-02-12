using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerRutinas;

public sealed record ObtenerRutinasQuery : IQuery<Rutina>;