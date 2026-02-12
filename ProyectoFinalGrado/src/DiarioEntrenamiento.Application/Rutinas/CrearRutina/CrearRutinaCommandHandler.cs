using System.Security.Claims;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;

namespace DiarioEntrenamiento.Application.Rutinas.CrearRutina;

internal sealed class CrearRutinaCommandHandler : ICommandHandler<CrearRutinaCommand, Guid>
{

    private readonly IRutinaRepository _rutinaRepository;

    public CrearRutinaCommandHandler(IRutinaRepository rutinaRepository)
    {
        _rutinaRepository = rutinaRepository;
    }

    public async Task<Result<Guid>> Handle(CrearRutinaCommand request, CancellationToken cancellationToken)
    {
        if(await _rutinaRepository.ExisteRutinaEnEsaFecha(request.UidUsuario,request.FechaIncio.ToDateTime(TimeOnly.MinValue), request.FechaFin.ToDateTime(TimeOnly.MinValue)))
        {
            return Result.Failure<Guid>(RutinaErrors.FechasDuplicadas);
        }
        var rutina = Rutina.Crear(request.UidUsuario, request.Nombre, request.FechaIncio, request.FechaFin);
        await _rutinaRepository.AddAsync(rutina, cancellationToken);
       
        return rutina.Id;

    }
}