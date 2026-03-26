using DiarioEntrenamiento.Domain.Ejercicios.Entidad;

namespace DiarioEntrenamiento.Domain.Ejercicios;

public interface IEjercicioRepository
{
    Task<List<Ejercicio>> GetByIds(List<Guid> id);
    Task<List<Ejercicio>> GetbySubGrupoMuscular(int idSubgrupo);
    Task<string> GetNombreById(Guid IdEjercicio);
    Task<Guid> GetUidByNombre(string Nombre);
    Task<IEnumerable<(string, string)>> ObtenerRelacionEjercioGrupoMuscular();
}