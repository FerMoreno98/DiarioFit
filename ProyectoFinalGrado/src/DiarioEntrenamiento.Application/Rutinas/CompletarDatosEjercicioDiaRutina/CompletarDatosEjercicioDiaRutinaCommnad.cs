using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.CompletarDatosEjercicioDiaRutina;

public record CompletarDatosEjercicioDiaRutinaCommand
(
Guid UidDiaRutina,
Guid UidEjercicio,
int orden,
int Series,
string RangoReps,
string RangoRIR,
int TiempoDeDescanso
) : ICommand<Unit>;