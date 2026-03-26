using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;


namespace DiarioEntrenamiento.Domain.Rutinas;

public interface IEjercicioDiaRutinaRepository
{
    Task AddAsync(EjercicioDiaRutina ejercicio,CancellationToken cancellationToken);
    Task AddVariosAsync(List<EjercicioDiaRutina> ejercicios, CancellationToken cancellationToken);
    Task DeleteAsync(Guid uid, CancellationToken cancellationToken);
    Task DeleteVariosAsync(List<Guid> UidEjerciciosDia, CancellationToken cancellationToken);
    Task ModificarAsync(EjercicioDiaRutina ejericicio,CancellationToken cancellationToken);
    Task<IEnumerable<EjercicioDiaRutinaDTO>> GetByIdDia(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<string>> GetNombreEjercicioByIdDia(Guid idDia);
    // Task<bool> EsOrdenRepetido(Guid UidDia, int orden);
    Task<int> GetSeriesPlanificadas(Guid UidDia,Guid UidEjercicio);
    Task<bool> EsEjercicioRepetido(Guid UidEjercicioDiaRutina,Guid UidDia,Guid UidEjercicio,string reps,string rir);
    Task<IEnumerable<EjercicioDiaRutinaDTO>> ObtenerEjerciciosDiaConNombre(Guid UidDia);
    Task<IEnumerable<Guid>> ObtenerUidEjerciciosDeDiaRutina(Guid UidDia);
    Task<IEnumerable<EjercicioDiaRutina>> ObtenerEjerciciosDiaRutinaDeRutina(Guid UidRutina);
    
}