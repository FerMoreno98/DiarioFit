namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;
public sealed class DiaEjercicioRow
{
    public Guid UidDia { get; set; }
    public Guid UidRutina { get; set; }
    public string Nombre { get; set; } = default!;
    public string DiaDeLaSemana { get; set; }

    public Guid UidEjercicioDiaRutina { get; set; }
    public Guid UidDiaEjercicio { get; set; }
    public Guid UidEjercicios { get; set; }
    public int Series { get; set; }
    public string ObjetivoReps { get; set; }
    public string ObjetivoRIR { get; set; }
    public int TiempoDescanso { get; set; }
    public int Orden { get; set; }
}
