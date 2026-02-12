using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.EliminarEjercicioDiaRutina;

internal sealed class EliminarEjercicioCommandHandler : ICommandHandler<EliminarEjercicioCommand, Unit>
{
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutinaRepository;

    public EliminarEjercicioCommandHandler(IEjercicioDiaRutinaRepository ejercicioDiaRutinaRepository)
    {
        _ejercicioDiaRutinaRepository = ejercicioDiaRutinaRepository;
    }

    public async Task<Result<Unit>> Handle(EliminarEjercicioCommand request, CancellationToken cancellationToken)
    {
        await _ejercicioDiaRutinaRepository.DeleteAsync(request.UidEjercicioDiaRutina,cancellationToken);
        return Result.Success(Unit.Value);
    }
}