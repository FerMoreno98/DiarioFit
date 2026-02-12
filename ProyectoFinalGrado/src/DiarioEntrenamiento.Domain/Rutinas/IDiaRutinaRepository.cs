using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Domain.Rutinas;

public interface IDiaRutinaRepository
{
    Task AddAsync(DiaRutina rutina, CancellationToken cancellationToken);
    Task DeleteAsync(Guid uid, CancellationToken cancellationToken);
    Task ModificarAsync(Guid uid, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<DiaRutina>> GetAllAsync(Guid uid, CancellationToken cancellationToken=default);
    Task<DiaRutina> GetByIdAsync(Guid id);
}