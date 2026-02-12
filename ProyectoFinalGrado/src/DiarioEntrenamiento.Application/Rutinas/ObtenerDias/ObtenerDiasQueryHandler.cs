using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDias;

internal sealed class ObtenerDiasQueryHandler : IQueryHandler<ObtenerDiasQuery, IReadOnlyCollection<DiaRutina>>
{
    private readonly IDiaRutinaRepository _diaRutinaRepository;

    public ObtenerDiasQueryHandler(IDiaRutinaRepository diaRutinaRepository)
    {
        _diaRutinaRepository = diaRutinaRepository;
    }

    public async Task<Result<IReadOnlyCollection<DiaRutina>>> Handle(ObtenerDiasQuery request, CancellationToken cancellationToken)
    {
       IReadOnlyCollection<DiaRutina> rutina=  await _diaRutinaRepository.GetAllAsync(request.Uid,cancellationToken);
       return Result.Success(rutina);
    }
}