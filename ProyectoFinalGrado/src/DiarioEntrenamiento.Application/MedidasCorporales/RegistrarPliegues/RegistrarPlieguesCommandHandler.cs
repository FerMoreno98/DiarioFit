using System;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues.Repository;
using MediatR;

namespace DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPliegues;

internal sealed class RegistrarPlieguesCommandHandler : ICommandHandler<RegistrarPlieguesCommand, Unit>
{
    private readonly IPliegueRepository _pliegueRepository;

    public RegistrarPlieguesCommandHandler(IPliegueRepository pliegueRepository)
    {
        _pliegueRepository = pliegueRepository;
    }

    public async Task<Result<Unit>> Handle(RegistrarPlieguesCommand request, CancellationToken cancellationToken)
    {
        try{
        Pliegue pliegue=Pliegue.Crear(
            request.UidUsuario,
            request.Abdominal,
            request.Suprailiaco,
            request.Tricipital,
            request.Subescapular,
            request.Muslo,
            request.Pantorrilla
        );
        await _pliegueRepository.Add(pliegue);
        return Result.Success(Unit.Value);
        }catch(Exception e)
        {
            return Result.Failure<Unit>(new Error("Error.Inesperado",e.Message));
        }

    }
}
