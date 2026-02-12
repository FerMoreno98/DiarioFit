using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDias;

public sealed record ObtenerDiasQuery(Guid Uid) : IQuery<IReadOnlyCollection<DiaRutina>>;