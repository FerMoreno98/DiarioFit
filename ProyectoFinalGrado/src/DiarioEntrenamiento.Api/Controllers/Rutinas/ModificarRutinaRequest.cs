namespace DiarioEntrenamiento.Api.Controllers.Rutinas;

public sealed record ModificarRutinaRequest
(
    Guid UidUsuario,
    Guid UidRutina,
    string Nombre,
    DateOnly FechaInicio,
    DateOnly FechaFin

);