namespace DiarioEntrenamiento.Api.Controllers.Rutinas;

public sealed record DuplicarRutinaRequest
(
    Guid UidUsuario,
    Guid UidRutina,
string nombre,
DateTime FechaInicio,
DateTime FechaFin

);