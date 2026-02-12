namespace DiarioEntrenamiento.Api.Controllers.Rutinas;

public sealed record DiaRutinaRequest(
    Guid uidRutina,
    string Nombre,
    string DiaDeLaSemana
);