namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

internal class EjercicioDiaRutinaResponseDTO
{
    public Guid uidejerciciodiarutina{ get; set; }
    public Guid UidDia{ get; set; }
    public Guid UidEjercicios{ get; set; }
    public int Orden{ get; set; }
    public int ObjetivoSeries{ get; set; }
    public string ObjetivoReps{ get; set; }
    public string ObjetivoRIR{ get; set; }
    public int TiempoDeDescanso{ get; set; }
}