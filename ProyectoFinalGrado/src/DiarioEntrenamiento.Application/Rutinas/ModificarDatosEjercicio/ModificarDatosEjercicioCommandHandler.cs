using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.ModificarDatosEjercicio;

internal sealed class ModificarDatosEjercicioCommandHandler : ICommandHandler<ModificarDatosEjercicioCommand, Unit>
{
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutinaRepository;

    public ModificarDatosEjercicioCommandHandler(IEjercicioDiaRutinaRepository ejercicioDiaRutinaRepository)
    {
        _ejercicioDiaRutinaRepository = ejercicioDiaRutinaRepository;
    }

    public async Task<Result<Unit>> Handle(ModificarDatosEjercicioCommand request, CancellationToken cancellationToken)
    {
        EjercicioDiaRutina ejercicio=EjercicioDiaRutina.CrearFromDataBase
        (
            request.UidEjercicioDiaRutina,
            request.UidDiaRutina,
            request.UidEjercicio,
            request.orden,
            request.Series,
            request.RangoReps,
            request.RangoRIR,
            request.TiempoDeDescanso
        ).Value;
        if(await _ejercicioDiaRutinaRepository.EsEjercicioRepetido(request.UidEjercicioDiaRutina,request.UidDiaRutina, request.UidEjercicio,request.RangoReps,request.RangoRIR))
        {
            return Result.Failure<Unit>(RutinaErrors.EjercicioRepetido);
        }
        await _ejercicioDiaRutinaRepository.ModificarAsync(ejercicio,cancellationToken);
        return Result.Success<Unit>(Unit.Value);
    }
}