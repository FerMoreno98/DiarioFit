namespace DiarioEntrenamiento.Application.Sesiones.RegistrarSerie;

public record RegistrarSesionResponse
(
    int SeriesHechas,
    int SeriesPlanificadas,
    bool EjercicioTerminado
);