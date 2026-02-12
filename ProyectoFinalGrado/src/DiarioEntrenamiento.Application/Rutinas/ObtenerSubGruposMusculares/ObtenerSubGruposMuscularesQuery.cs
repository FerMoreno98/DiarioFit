using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerSubGruposMusculares;

public sealed record ObtenerSubGruposMuscularesQuery(int IdGrupoMuscular) : IQuery<IEnumerable<SubGrupoMuscular>>;