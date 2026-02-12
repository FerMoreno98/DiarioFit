using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;
using MediatR;

namespace DiarioEntrenamiento.Application.Sesiones.RegistrarSerie;

internal sealed class RegistrarSerieCommandHandler : ICommandHandler<RegistrarSerieCommand, RegistrarSesionResponse>
{
    private readonly ISerieRepository _serieRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IEjercicioRepository _ejercicioRepository;
    private readonly IEjercicioDiaRutinaRepository _ejercicioRutinaRepository;

    public RegistrarSerieCommandHandler(ISerieRepository serieRepository, ISesionRepository sesionRepository, IEjercicioRepository ejercicioRepository, IEjercicioDiaRutinaRepository ejercicioRutinaRepository)
    {
        _serieRepository = serieRepository;
        _sesionRepository = sesionRepository;
        _ejercicioRepository = ejercicioRepository;
        _ejercicioRutinaRepository = ejercicioRutinaRepository;
    }

    public async Task<Result<RegistrarSesionResponse>> Handle(RegistrarSerieCommand request, CancellationToken cancellationToken)
    {
        Guid uidSesion=await _sesionRepository.GetUidSesionPor(request.UidDia);
        Guid UidEjercicio=await _ejercicioRepository.GetUidByNombre(request.Ejercicio);
        int SeriesPlanificadas=await _ejercicioRutinaRepository.GetSeriesPlanificadas(request.UidDia,UidEjercicio);
        int SeriesHechas=request.Serie;
        if (await _serieRepository.EsSerieRepetida(uidSesion,UidEjercicio,request.Serie))
        {
            // Estaria bien crear un metodo update en el dominio para pasar las validaciones del dominio tambien aqui
            await _serieRepository.UpdateSerie(
                uidSesion,
                UidEjercicio,
                request.Peso,
                request.Repeticiones,
                request.Rir,
                request.Serie
            );
        }else{
        Result<SerieRealizada> serie=SerieRealizada.Crear(
            UidEjercicio,uidSesion,request.Peso,request.Repeticiones,request.Rir,request.Serie);
        if (serie.IsFailure)
        {
            return Result.Failure<RegistrarSesionResponse>(serie.Error);
        }
       
        var serieValue=serie.Value;
       await _serieRepository.RegistrarSerie
       (
       serieValue.Uid,
       uidSesion,
       serieValue.UidEjercicio,
       serieValue.Peso,
       serieValue.Repeticiones,
       serieValue.RIR,
       serieValue.Serie
       );
        }
       RegistrarSesionResponse result=new RegistrarSesionResponse(SeriesHechas,SeriesPlanificadas,SeriesHechas>=SeriesPlanificadas);
       return Result.Success(result);
    }
}