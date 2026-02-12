namespace DiarioEntrenamiento.Domain.Sesiones.DTOs;

public sealed record UltimaSesionDto
{
    public string Ejercicio{get;set;}
    public Guid IdEjercicio{get;set;}
    public decimal Peso{get;set;}
    public int Repeticiones{get;set;}
    public string Rir{get;set;}
    public int Serie{get;set;}
    public DateTime FechaSesion{get;set;}
}