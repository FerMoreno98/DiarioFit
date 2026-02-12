using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.GruposMusculares;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerGruposMusculares;

internal sealed class ObtenerGruposMuscularesQueryHandler : IQueryHandler<ObtenerGruposMuscularesQuery, IEnumerable<GrupoMuscular>>
{
    private readonly IGrupoMuscularRepository _grupoMuscularRepository;

    public ObtenerGruposMuscularesQueryHandler(IGrupoMuscularRepository grupoMuscularRepository)
    {
        _grupoMuscularRepository = grupoMuscularRepository;
    }

    public async Task<Result<IEnumerable<GrupoMuscular>>> Handle(ObtenerGruposMuscularesQuery request, CancellationToken cancellationToken)
    {
        return Result.Success(await _grupoMuscularRepository.GetAll());
    }
}