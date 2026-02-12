using DiarioEntrenamiento.Application.Abstractions.Messaging;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerNumeroDeSeriesPorGrupoMuscular;

public sealed record ObtenerNumeroDeSeriesPorGrupoMuscularQuery(Guid UidUsuario) : IQuery<Dictionary<string,int>>;