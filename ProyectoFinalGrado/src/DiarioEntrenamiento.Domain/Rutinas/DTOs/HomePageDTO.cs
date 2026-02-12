namespace DiarioEntrenamiento.Domain.Rutinas.DTOs;

public sealed record RutinaHomeDto
{
    public Guid UidRutina{get;set;}
    public string NombreRutina{get;set;}


}
public sealed record DiaRutinaHomeDto
{
    public Guid UidDia{get; set;}
    public string NombreDiaRutina{get;set;}
    public string DiaDeLaSemana{get;set;}
    public List<EjercicioDiaRutinaHomeDto> datosEjercicios {get;set;}
    public bool RutinaHecha{get;set;}

}
public sealed record EjercicioDiaRutinaHomeDto
{
    public string Ejercicio{get;set;}
    public string Series{get;set;}
    public string ObjetivoReps{get;set;}
}