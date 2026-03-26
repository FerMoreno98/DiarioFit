namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

internal class RutinaWithDiasYEjercicios
{
    public Guid Uid{get; set;}
    public Guid UidUsuario{get; set;}
    public string Nombre{get; set;}
    public DateTime FechaInicio{get; set;}
    public DateTime FechaFin{get; set;}

    public Guid UidDia{ get; set; }
    public Guid UidRutina{ get; set; }
    public string NombreDia{ get; set; }
    public string DiaDeLaSemana{ get; set; }

    public Guid UidEjercicioDiaRutina{ get; set; }
    public Guid UidDiaEjercicio{ get; set; }
    public Guid UidEjercicios{ get; set; }
    public int Orden{ get; set; }
    public int Series{ get; set; }
    public string ObjetivoReps{ get; set; }
    public string ObjetivoRIR{ get; set; }
    public int TiempoDescanso{ get; set; }
}