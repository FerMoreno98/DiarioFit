namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

public sealed record SerieDto
(
     Guid Uid,
     Guid UidEjercicio ,
     Guid UidSesion,
     decimal? Peso,
     int? Repeticiones,
     string Rir

);