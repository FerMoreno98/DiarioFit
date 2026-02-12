namespace DiarioEntrenamiento.Domain.Rutinas.DTOs;

public sealed record DatosGraficaGruposMuscularesDto
{
    public Guid UidEjercicio{get;set;}
    public int Series{get;set;}
    public int IdSubGrupoMuscular{get;set;}
}