using DiarioEntrenamiento.Application.Abstractions.Messaging;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerUidRutina;

public sealed record ObtenerUidRutinaQuery(Guid UidDia) : IQuery<Guid>;