namespace DiarioEntrenamiento.Api.Controllers.Sesiones;

public sealed record DatosInicioSesionRequest(
    Guid UidUsuario,
    Guid UidDia,
    int? Sueno,
    int? Motivacion,
    int? ERP
);