using DiarioEntrenamiento.Application.Abstractions.Clock;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;
using DiarioEntrenamiento.Domain.Sesiones.Errors;
using DiarioEntrenamiento.Domain.Sesiones.ValueObjects;

namespace DiarioEntrenamiento.Application.Sesiones.IniciarSesionEntrenamiento;

internal sealed class IniciarSesionCommandHandler : ICommandHandler<IniciarSesionCommand, SesionDto>
{
    private readonly IClock _clock;
    private readonly ISesionRepository _sesionRepository;
    private readonly IRutinaRepository _rutinaRepository;

    public IniciarSesionCommandHandler(IClock clock, ISesionRepository sesionRepository, IRutinaRepository rutinaRepository)
    {
        _clock = clock;
        _sesionRepository = sesionRepository;
        _rutinaRepository = rutinaRepository;
    }

    public async Task<Result<SesionDto>> Handle(IniciarSesionCommand request, CancellationToken cancellationToken)
    {
        Guid UidRutina=await _rutinaRepository.ObtenerUidRutinaPorUidDia(request.UidDia);
        Result<EstadoUsuario> estado= EstadoUsuario.Crear(request.Sueno,request.Motivacion,request.ERP);
        if (estado.IsFailure)
        {
            return Result.Failure<SesionDto>(estado.Error);   
        }
        Sesion sesion=Sesion.Crear(request.UidUsuario,UidRutina,request.UidDia,_clock.Now,estado.Value);
        Guid? ExisteSesion=await _sesionRepository.ExisteSesion(sesion.UidRutina,sesion.UidDia,sesion.FechaSesion);
        if(ExisteSesion is not null)
        {
            await _sesionRepository.Update(ExisteSesion,sesion,cancellationToken);
        }else
        {
            await _sesionRepository.InsertSesion(sesion,cancellationToken);
        }
        SesionDto sesionDto=new SesionDto(
            sesion.UidUsuario,sesion.UidRutina,sesion.UidDia,estado.Value.Sueno,sesion.Estado.Motivacion,sesion.Estado.ERP);
        return Result.Success(sesionDto);

    }
}