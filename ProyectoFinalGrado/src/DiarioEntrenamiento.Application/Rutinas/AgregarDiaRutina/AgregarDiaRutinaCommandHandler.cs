
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;

namespace DiarioEntrenamiento.Application.Rutinas.AgregarDiaRutina;

internal sealed class AgregarDiaCommandHandler : ICommandHandler<AgregarDiaRutinaCommand, IReadOnlyCollection<DiaRutina>?>
{
    private readonly IRutinaRepository _rutinaRepository;
    private readonly IUnitOfWork _UOW;
    private readonly IDiaRutinaRepository _diaRutinaRepository;

    public AgregarDiaCommandHandler(IRutinaRepository rutinaRepository, IUnitOfWork uOW, IDiaRutinaRepository diaRutinaRepository)
    {
        _rutinaRepository = rutinaRepository;
        _UOW = uOW;
        _diaRutinaRepository = diaRutinaRepository;
    }
// Aunque no sea necesario usar el unitofwork explicito aqui lo uso para probarlo
    public async Task<Result<IReadOnlyCollection<DiaRutina>?>> Handle(AgregarDiaRutinaCommand request, CancellationToken cancellationToken)
    {
        await _UOW.BeginAsync(cancellationToken);
        try{
            Rutina? rutina=await _rutinaRepository.GetByIdWithDiasAsync(request.UidRutina,cancellationToken);
            if(rutina is null)
            {
                return Result.Failure<IReadOnlyCollection<DiaRutina>>(RutinaErrors.RutinaActualNoSeleccionada);
            }
            Result<DiaRutina> dia=rutina.AgregarDia(request.UidRutina,request.Nombre,request.DiaDeLaSemana);
            if (dia.IsFailure)
            {
                return Result.Failure<IReadOnlyCollection<DiaRutina>>(dia.Error);
            }
            await _diaRutinaRepository.AddAsync(dia.Value,cancellationToken);
            await _UOW.CommitAsync(cancellationToken);
            return Result.Success(rutina.Dias);
        }
        catch (Exception e)
        {
            await _UOW.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _UOW.DisposeAsync();
        }
    }
}