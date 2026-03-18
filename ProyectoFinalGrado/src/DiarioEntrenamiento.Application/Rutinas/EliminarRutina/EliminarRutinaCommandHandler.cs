using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.ELiminarRutina;

internal sealed class EliminarRutinaCommandHandler : ICommandHandler<EliminarRutinaCommand, Unit>
{
    private readonly IRutinaRepository _rutinaRepository;
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutinaReporitory;
    private readonly IDiaRutinaRepository _diaRutinaRepository;
    private readonly IUnitOfWork _uow;

    public EliminarRutinaCommandHandler(IRutinaRepository rutinaRepository, IEjercicioDiaRutinaRepository ejercicioDiaRutinaReporitory, IDiaRutinaRepository diaRutinaRepository, IUnitOfWork uow)
    {
        _rutinaRepository = rutinaRepository;
        _ejercicioDiaRutinaReporitory = ejercicioDiaRutinaReporitory;
        _diaRutinaRepository = diaRutinaRepository;
        _uow = uow;
    }

    public async Task<Result<Unit>> Handle(EliminarRutinaCommand request, CancellationToken cancellationToken)
    {
        await _uow.BeginAsync(cancellationToken);
        try{
        List<DiaRutina> diasRutinaYEjercicios=await _diaRutinaRepository.GetDiasDeRutinaWithEjercicios(request.UidRutina);
        foreach(var dia in diasRutinaYEjercicios)
        {
            IEnumerable<EjercicioDiaRutinaDTO> ejerciciosdeldia=await _ejercicioDiaRutinaReporitory.GetByIdDia(dia.Id,cancellationToken);
            List<Guid> UidsEjercicio=dia.EjerciciosDiaRutinas.Select(e=>e.Id).ToList();
            await _ejercicioDiaRutinaReporitory.DeleteVariosAsync(UidsEjercicio, cancellationToken);            
            await _diaRutinaRepository.DeleteAsync(dia.Id,cancellationToken);
        }
        await _rutinaRepository.DeleteAsync(request.UidRutina,cancellationToken);
        await _uow.CommitAsync(cancellationToken);
        return Result.Success(Unit.Value);

        }catch
        {
            await _uow.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _uow.DisposeAsync();
        }


    }
}