using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.ModificarRutina;

internal sealed class ModificarRutinaCommandHandler : ICommandHandler<ModificarRutinaCommand, Unit>
{
    private readonly IRutinaRepository _rutinaRepository;

    public ModificarRutinaCommandHandler(IRutinaRepository rutinaRepository)
    {
        _rutinaRepository = rutinaRepository;
    }

    public async Task<Result<Unit>> Handle(ModificarRutinaCommand request, CancellationToken cancellationToken)
    {
        if(await _rutinaRepository.ExisteRutinaEnEsaFecha
        (request.UidRutina,
        request.FechaInicio.ToDateTime(TimeOnly.MinValue),
        request.FechaFin.ToDateTime(TimeOnly.MinValue)))
        {
            return Result.Failure<Unit>(RutinaErrors.FechasDuplicadas);
        }
        Rutina rutina=Rutina.CrearFromDataBase(
            request.UidRutina,
            request.UidUsuario,
            request.Nombre,
            request.FechaInicio,
            request.FechaFin
            );
        await _rutinaRepository.ModificarAsync(rutina,cancellationToken);
        return Result.Success(Unit.Value);
    }
}