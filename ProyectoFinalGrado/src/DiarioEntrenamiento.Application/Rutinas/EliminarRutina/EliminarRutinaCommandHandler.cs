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
        //pillo los dias de la rutina
        IReadOnlyCollection<DiaRutina> DiasRutina=await _diaRutinaRepository.GetAllAsync(request.UidRutina);
        //borro los datos de esos dias
        foreach(var dia in DiasRutina)
        {
            // pillo de cada dia sus ejercicios y borro los datos, cuando termina, borro el dia
            IEnumerable<EjercicioDiaRutinaDTO> ejerciciosdeldia=await _ejercicioDiaRutinaReporitory.GetByIdDia(dia.Id,cancellationToken);
            foreach(var ejercicio in ejerciciosdeldia)
            {
            await _ejercicioDiaRutinaReporitory.DeleteAsync(ejercicio.UidEjercicioDiaRutina, cancellationToken);
            }
            await _diaRutinaRepository.DeleteAsync(dia.Id,cancellationToken);
        }
        //Borro la rutina
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