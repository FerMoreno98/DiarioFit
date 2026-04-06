using DiarioEntrenamiento.Domain.Sesiones.Entidad;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

public record class SesionBuilder
{
    public Guid Uid { get; set; }
    public Guid UidUsuario { get; set; }
    public Guid UidRutina { get; set; }
    public Guid UidDia { get; set; }
    public DateTime FechaSesion { get; set; }
    public List<SerieRealizada> serie{ get; set; }
}
