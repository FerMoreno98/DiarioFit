using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Application.Rutinas.ObtenerGruposMusculares;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.GruposMusculares;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerSubGruposMusculares;

internal sealed class ObtenerSubGruposMuscularesQueryHandler : IQueryHandler<ObtenerSubGruposMuscularesQuery, IEnumerable<SubGrupoMuscular>>
{
    private readonly ISubGrupoMuscularRepository _subGrupoMuscularRepository;

    public ObtenerSubGruposMuscularesQueryHandler(ISubGrupoMuscularRepository subGrupoMuscularRepository)
    {
        _subGrupoMuscularRepository = subGrupoMuscularRepository;
    }

    public async Task<Result<IEnumerable<SubGrupoMuscular>>> Handle(ObtenerSubGruposMuscularesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<SubGrupoMuscular> ret= await _subGrupoMuscularRepository.GetSubGruposBy(request.IdGrupoMuscular);
        return Result.Success(ret);
    }
}