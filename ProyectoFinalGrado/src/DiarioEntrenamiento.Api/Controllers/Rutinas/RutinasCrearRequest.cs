namespace DiarioEntrenamiento.Api.Controllers.Rutinas;

public sealed record RutinasCrearRequest
(
    Guid Uid,
string nombre,
DateOnly FechaInicio,
DateOnly FechaFin

);