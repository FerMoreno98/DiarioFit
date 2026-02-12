using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerGruposMusculares;

public sealed record ObtenerGruposMuscularesQuery : IQuery<IEnumerable<GrupoMuscular>>;