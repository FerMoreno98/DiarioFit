namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

internal class RutinaDto{
    public Guid Uid{get; set;}
    public Guid UidUsuario{get; set;}
    public string Nombre{get; set;}
    public DateTime FechaInicio{get; set;}
    public DateTime FechaFin{get; set;}
}