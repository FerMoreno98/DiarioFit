using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

public sealed class DiaBuilder
{
    public Guid Uid { get; set; }
    public Guid UidRutina { get; set; }
    public string Nombre { get; set; } = default!;
    public string DiaDeLaSemana { get; set; }
    public List<EjercicioDiaRutina> Ejercicios { get; set; } = default!;
}