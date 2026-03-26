using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Domain.Rutinas;

public interface IRutinaRepository
{
    Task AddAsync(Rutina rutina, CancellationToken cancellationToken);
    Task DeleteAsync(Guid uid, CancellationToken cancellationToken);
    Task ModificarAsync(Rutina rutina, CancellationToken cancellationToken);
    Task<Rutina> GetCurrentAysnc(Guid uid,CancellationToken cancellationToken= default);
    Task<Rutina> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);
    Task<Guid> GetUidRutinaPorUidDia(Guid UidDia);
    Task<bool> ExisteRutinaEnEsaFecha(Guid UidUsuario,DateTime FechaInicio, DateTime FechaFin);
    Task<Guid> ObtenerUidRutinaPorUidDia(Guid UidDia);
    Task<Rutina?> GetByIdWithDiasAsync(Guid UidRutina, CancellationToken cancellationToken);
    Task<List<Rutina>> GetAllWithDias(Guid UidUsuario);
    Task<Rutina> ObtenerRutinaCompleta(Guid UidRutina);
}