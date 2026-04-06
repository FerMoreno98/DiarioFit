using System;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros.Repository;

namespace DiarioEntrenamiento.Application.MedidasCorporales.ObtenerPerimetros;

internal sealed class ObtenerPerimetrosQueryHandler : IQueryHandler<ObtenerPerimetrosQuery, List<Perimetro>>
{
    private readonly IPerimetrosRepository _perimetrosRepository;

    public ObtenerPerimetrosQueryHandler(IPerimetrosRepository perimetrosRepository)
    {
        _perimetrosRepository = perimetrosRepository;
    }

    public async Task<Result<List<Perimetro>>> Handle(ObtenerPerimetrosQuery request, CancellationToken cancellationToken)
    {
        List<Perimetro> perimetros = await _perimetrosRepository.GetAllFromUserAsync(request.UidUsuario);
        return Result.Success(perimetros);
    }
}
