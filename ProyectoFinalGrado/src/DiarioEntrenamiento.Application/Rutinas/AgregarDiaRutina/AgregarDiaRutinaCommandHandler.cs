
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;

namespace DiarioEntrenamiento.Application.Rutinas.AgregarDiaRutina;

internal sealed class AgregarDiaCommandHandler : ICommandHandler<AgregarDiaRutinaCommand, IReadOnlyCollection<DiaRutina>>
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
    public async Task<Result<IReadOnlyCollection<DiaRutina>>> Handle(AgregarDiaRutinaCommand request, CancellationToken cancellationToken)
    {
        await _UOW.BeginAsync(cancellationToken);
        try{
            
            Rutina rutina= await _rutinaRepository.GetByIdAsync(request.UidRutina);
            IReadOnlyCollection<DiaRutina> diasRutina=await _diaRutinaRepository.GetAllAsync(rutina.Id,cancellationToken);
            if (diasRutina.Count() == 7)
            {
                return Result.Failure<IReadOnlyCollection<DiaRutina>>(RutinaErrors.MaximoDiasAlcanzado);
            }
            if(rutina is null)
            {
                return Result.Failure<IReadOnlyCollection<DiaRutina>>(RutinaErrors.RutinaActualNoSeleccionada);
            }
            foreach(var day in diasRutina)
            {
                if (day.DiaDeLaSemana.Equals(request.DiaDeLaSemana))
                {
                    return Result.Failure<IReadOnlyCollection<DiaRutina>>(RutinaErrors.DiaDuplicado);
                }
            }
            rutina.AgregarDia(rutina.Id,request.Nombre,request.DiaDeLaSemana);
            DiaRutina dia=rutina.Dias.FirstOrDefault(x=>x.DiaDeLaSemana==request.DiaDeLaSemana);
            if(dia is null)
            {
                return Result.Failure<IReadOnlyCollection<DiaRutina>>(RutinaErrors.DiaNoEncontrado);    
            }
     

     
            await _diaRutinaRepository.AddAsync(dia,cancellationToken);
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