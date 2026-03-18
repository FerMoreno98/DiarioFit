using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

internal class RutinaWithDiasDto
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
}

internal class RutinaWithDiasResult
{
    public Guid Uid{get; set;}
    public Guid UidUsuario{get; set;}
    public string Nombre{get; set;}
    public DateTime FechaInicio{get; set;}
    public DateTime FechaFin{get; set;}
    public List<DiaRutina> DiasRutina{get;set;}
}