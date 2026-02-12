namespace DiarioEntrenamiento.Api.Controllers.Rutinas;

public sealed record DatosModificarEjercicioDiaRutinaRequest(
Guid UidEjercicioDiaRutina,
Guid UidEjercicio,
Guid UidDiaRutina,
int orden,
int Series,
string RangoReps,
string RangoRIR,
int TiempoDeDescanso
);