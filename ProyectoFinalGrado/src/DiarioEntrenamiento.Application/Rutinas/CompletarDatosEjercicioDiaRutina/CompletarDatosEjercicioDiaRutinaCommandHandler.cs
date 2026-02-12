using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.CompletarDatosEjercicioDiaRutina;

internal sealed class CompletarDatosEjercicioDiaRutinaCommandHandler : ICommandHandler<CompletarDatosEjercicioDiaRutinaCommand, Unit>
{
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutinaRepository;
    private readonly IDiaRutinaRepository _diaRutinaRepository;
    private readonly IRutinaRepository _rutinaRepository;

    public CompletarDatosEjercicioDiaRutinaCommandHandler(IEjercicioDiaRutinaRepository ejercicioDiaRutinaRepository, IDiaRutinaRepository diaRutinaRepository, IRutinaRepository rutinaRepository)
    {
        _ejercicioDiaRutinaRepository = ejercicioDiaRutinaRepository;
        _diaRutinaRepository = diaRutinaRepository;
        _rutinaRepository = rutinaRepository;
    }

    public async Task<Result<Unit>> Handle(CompletarDatosEjercicioDiaRutinaCommand request, CancellationToken cancellationToken)
    {
        if(await _ejercicioDiaRutinaRepository.EsOrdenRepetido(request.UidDiaRutina, request.orden))
        {
            return Result.Failure<Unit>(RutinaErrors.OrdenRepetido);
        }
        DiaRutina dia=await _diaRutinaRepository.GetByIdAsync(request.UidDiaRutina);
        Rutina rutina=await _rutinaRepository.GetByIdAsync(dia.Uid_rutina);
        Result<DatosEjercicio> datos=DatosEjercicio.Crear(request.Series,request.RangoReps,request.RangoRIR,request.TiempoDeDescanso);
        if (datos.IsFailure)
        {
            return Result.Failure<Unit>(datos.Error);
        }
        Result<EjercicioDiaRutina> ejerciciodia=rutina.CrearNuevoEjercicio(request.UidEjercicio,request.orden,datos.Value);
        if(await _ejercicioDiaRutinaRepository.EsEjercicioRepetido(ejerciciodia.Value.Id,request.UidDiaRutina, request.UidEjercicio,request.RangoReps,request.RangoRIR))
        {
            return Result.Failure<Unit>(RutinaErrors.EjercicioRepetido);
        }
       await  _ejercicioDiaRutinaRepository.AddAsync(ejerciciodia.Value,dia.Id,cancellationToken);
       return Result.Success<Unit>(Unit.Value);
    }
}