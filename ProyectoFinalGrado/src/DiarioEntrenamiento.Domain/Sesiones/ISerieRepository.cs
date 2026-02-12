using DiarioEntrenamiento.Domain.Sesiones.DTOs;

namespace DiarioEntrenamiento.Domain.Sesiones;

public interface ISerieRepository
{
    Task RegistrarSerie(Guid UidSerie,Guid UidSesion,Guid uidEjercicio,decimal? peso,int? Repeticiones,string? Rir,int Serie);
    Task UpdateSerie(Guid UidSesion,Guid UidEjercicio,decimal? peso,int? Repeticiones,string? Rir,int Serie);
    Task<bool> EsSerieRepetida(Guid UidSesion,Guid UidEjercicio,int Serie);
    Task<IEnumerable<SerieDto>> ObtenerHistoricoSeriesDeUnEjercicio(Guid UidEjercicio);
}