namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

public record SesionDto(
    Guid Uid,
    Guid UidUsuario,
    Guid UidRutina,
    Guid UidDia,
    DateTime FechaSesion,
    Guid UidSerie,      // rdse.Uid
    Guid UidEjercicio,  // rdse.UidEjercicio
    Guid UidSesion,     // rdse.UidRegistroDatosSesion
    decimal? Peso,
    int? Repeticiones,
    string? Rir,
    int Serie
);

