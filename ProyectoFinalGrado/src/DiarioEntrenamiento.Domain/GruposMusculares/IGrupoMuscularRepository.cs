using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

namespace DiarioEntrenamiento.Domain.GruposMusculares;

public interface IGrupoMuscularRepository
{
    Task<IEnumerable<GrupoMuscular>> GetAll();
    Task<GrupoMuscular> GetById(int id);
    Task<GrupoMuscular> ObtenerGrupoPorSubGrupo(int IdSubGrupo);
}

