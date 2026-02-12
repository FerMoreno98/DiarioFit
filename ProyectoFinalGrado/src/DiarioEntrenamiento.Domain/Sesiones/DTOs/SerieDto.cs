namespace DiarioEntrenamiento.Domain.Sesiones.DTOs;

public sealed record SerieDto
{
    public Guid UidEjercicio{get;set;}
    public string Ejercicio{get;set;}
    public decimal Peso{get;set;}
    public int Repeticiones{get;set;}
    public string Rir{get;set;}
    public DateTime FechaSesion{get;set;}
    public decimal RMCalculado{get;set;}=0;
}