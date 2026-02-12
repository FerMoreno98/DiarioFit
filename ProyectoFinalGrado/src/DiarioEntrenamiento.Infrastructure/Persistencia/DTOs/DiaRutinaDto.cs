namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

internal class DiaRutinaDto
{
    public Guid Uid{ get; set; }
    public Guid UidRutina{ get; set; }
    public string Nombre{ get; set; }
    public string DiaDeLaSemana{ get; set; }
}