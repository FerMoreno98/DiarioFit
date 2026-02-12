using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Domain.Rutinas.DTOs;

public sealed record DatosEditarMesocicloDto
{
    public Guid Uid{get;set;}
    public Guid UidUsuario{get;set;}
    public string Nombre{get;set;}
    public DateTime FechaInicio{get;set;}
    public DateTime FechaFin{get;set;}

    public IReadOnlyCollection<DiaRutina> diasEntrenamiento{get;set;}

}

