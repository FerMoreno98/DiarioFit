using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
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
        DiaRutina dia=await _diaRutinaRepository.GetDiaByIdWithEjerciciosAsync(request.UidDiaRutina);
        Result<DatosEjercicio> datos=DatosEjercicio.Crear(request.Series,request.RangoReps,request.RangoRIR,request.TiempoDeDescanso);
        if (datos.IsFailure)
        {
            return Result.Failure<Unit>(datos.Error);
        }
        Result<EjercicioDiaRutina> ejerciciodia=EjercicioDiaRutina.Crear(request.UidEjercicio,dia.Id,request.orden,datos.Value);
        Result agregarEjercicioResultado=dia.AgregarEjercicio(ejerciciodia.Value);
        if (agregarEjercicioResultado.IsFailure)
        {
            return Result.Failure<Unit>(agregarEjercicioResultado.Error);
        }
       await  _ejercicioDiaRutinaRepository.AddAsync(ejerciciodia.Value,cancellationToken);
       return Result.Success(Unit.Value);
    }
}