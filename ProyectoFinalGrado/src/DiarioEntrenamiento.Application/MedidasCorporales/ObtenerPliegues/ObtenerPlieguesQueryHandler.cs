using System;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues.Repository;

namespace DiarioEntrenamiento.Application.MedidasCorporales.ObtenerPliegues;

internal sealed class ObtenerPlieguesQueryHandler : IQueryHandler<ObtenerPlieguesQuery, List<Pliegue>>
{
    private readonly IPliegueRepository _pliegueRepository;

    public ObtenerPlieguesQueryHandler(IPliegueRepository pliegueRepository)
    {
        _pliegueRepository = pliegueRepository;
    }

    public async Task<Result<List<Pliegue>>> Handle(ObtenerPlieguesQuery request, CancellationToken cancellationToken)
    {
        List<Pliegue> pliegues=await _pliegueRepository.GetAllFromUserAsync(request.UidUsuario);
        return Result.Success(pliegues);
    }
}
