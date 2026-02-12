using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.ModificarDatosEjercicio;

public sealed record ModificarDatosEjercicioCommand
(
Guid UidEjercicioDiaRutina,
Guid UidDiaRutina,
Guid UidEjercicio,
int orden,
int Series,
string RangoReps,
string RangoRIR,
int TiempoDeDescanso
) : ICommand<Unit>;