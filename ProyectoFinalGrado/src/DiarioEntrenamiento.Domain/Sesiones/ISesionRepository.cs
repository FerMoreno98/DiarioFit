using DiarioEntrenamiento.Domain.Sesiones.DTOs;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;

namespace DiarioEntrenamiento.Domain.Sesiones;

public interface ISesionRepository
{
    Task InsertSesion(Sesion sesion, CancellationToken cancellationToken=default);
    Task<Guid?> ExisteSesion(Guid UidRutina,Guid UidDia,DateTime Fecha);
    Task Update(Guid? UidSesion,Sesion sesion, CancellationToken cancellationToken=default);
    Task<IEnumerable<UltimaSesionDto>> ObtenerUltimaSesion(Guid UidDia);
    Task<Guid> GetUidSesionPor(Guid UidDia);
    Task<IEnumerable<UltimaSesionDto>> ObtenerSesionRecienHecha(Guid UidDia);
    Task<List<Sesion>> ObtenerSesionesCompletasPorUidSerie(Guid UidUsuario,List<Guid> UidSerie);

}