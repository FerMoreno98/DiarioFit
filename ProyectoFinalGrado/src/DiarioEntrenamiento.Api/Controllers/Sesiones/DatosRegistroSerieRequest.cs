namespace DiarioEntrenamiento.Api.Controllers.Sesiones;

public sealed record DatosRegistroSerieRequest
(
    Guid UidDia,
    string Ejercicio,
    decimal? Peso,
    int? Repeticiones,
    string? Rir,
    int Serie
);