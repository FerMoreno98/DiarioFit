using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

namespace DiarioEntrenamiento.Domain.GruposMusculares;

public interface ISubGrupoMuscularRepository
{
    Task<IEnumerable<SubGrupoMuscular>> GetSubGruposBy(int idGrupo);
}