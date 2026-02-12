using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerUidRutina;

internal sealed class ObtenerUidRutinaQueryHandler : IQueryHandler<ObtenerUidRutinaQuery, Guid>
{
    private readonly IRutinaRepository _rutinaRepository;

    public ObtenerUidRutinaQueryHandler(IRutinaRepository rutinaRepository)
    {
        _rutinaRepository = rutinaRepository;
    }

    public async Task<Result<Guid>> Handle(ObtenerUidRutinaQuery request, CancellationToken cancellationToken)
    {
        return await _rutinaRepository.GetUidRutinaPorUidDia(request.UidDia);
    }
}