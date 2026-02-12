namespace DiarioEntrenamiento.Application.Rutinas.ObtenerEjerciciosDia;
public record EjercicioDiaConDetalleDto(
    Guid EjercicioDiaUid,
    Guid EjercicioUid,
    string NombreEjercicio,
    // string GruposMuscularesPrincipales,
    // string GruposMuscularesSecundarios,
    int? ObjetivoSeries,
    string? ObjetivoReps,
    string? ObjetivoRir,
    int TiempoDescanso
);

