namespace DiarioEntrenamiento.Application.Sesiones.IniciarSesionEntrenamiento;

public record SesionDto
(
    Guid UidUsuario,
    Guid UidRutina,
    Guid UidDia,
    int? Sueno,
    int? Motivacion,
    int? ERP
);