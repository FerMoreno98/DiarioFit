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
    Task<RutinaHomeDto> ObtenerDatosHomePageRutina(Guid UidUsuario);
    Task<IEnumerable<DiaRutinaHomeDto>> ObtenerDatosHomePageDiaRutina(Guid UidRutina);
    Task<IEnumerable<EjercicioDiaRutinaHomeDto>> ObtenerDatosHomePageEjercicioDiaRutina(Guid UidDiaRutina);
    Task<Guid> ObtenerUidRutinaPorUidDia(Guid UidDia);
    // Antes usaba esta consulta para pillar todas las rutinas sin los dias, he optimizado el caso de uso y ya no hace falta en principio
    // Task<List<Rutina>> ObtenerMesociclosUsuario(Guid UidUsuario);
    Task<Rutina?> GetByIdWithDiasAsync(Guid UidRutina, CancellationToken cancellationToken);
    Task<List<Rutina>> GetAllWithDias(Guid UidUsuario);
    Task<Rutina> ObtenerRutinaCompleta(Guid UidRutina);
}