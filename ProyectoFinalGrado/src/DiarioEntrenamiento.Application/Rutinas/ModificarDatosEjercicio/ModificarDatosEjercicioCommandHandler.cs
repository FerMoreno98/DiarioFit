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
        Result<DatosEjercicio> datos=DatosEjercicio.Crear
        (request.Series,request.RangoReps,request.RangoRIR,request.TiempoDeDescanso);
        if (datos.IsFailure)
        {
            return Result.Failure<Unit>(datos.Error);
        }
        EjercicioDiaRutina ejercicio=EjercicioDiaRutina.CrearFromDataBase
        (request.UidEjercicioDiaRutina,request.UidEjercicio,request.orden,datos.Value);
        if(await _ejercicioDiaRutinaRepository.EsEjercicioRepetido(request.UidEjercicioDiaRutina,request.UidDiaRutina, request.UidEjercicio,request.RangoReps,request.RangoRIR))
        {
            return Result.Failure<Unit>(RutinaErrors.EjercicioRepetido);
        }
        await _ejercicioDiaRutinaRepository.ModificarAsync(ejercicio,cancellationToken);
        return Result.Success<Unit>(Unit.Value);
    }
}