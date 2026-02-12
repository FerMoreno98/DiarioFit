namespace DiarioEntrenamiento.Domain.Rutinas.DTOs;
public sealed record EjercicioDiaRutinaDTO
{
   public Guid UidEjercicioDiaRutina{ get; set; }
    public Guid UidDia{ get; set; }
    public Guid UidEjercicios{ get; set; }
    public string Nombre{get; set;}
    public int Orden { get; set; }
    public int Series { get; set; }
    public string ObjetivoReps{ get; set; }
    public string ObjetivoRIR{ get; set; }
    public int TiempoDescanso{ get; set; }
}