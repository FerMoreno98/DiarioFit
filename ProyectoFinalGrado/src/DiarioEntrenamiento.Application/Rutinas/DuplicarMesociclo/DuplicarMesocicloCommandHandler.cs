using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.DuplicarMesociclo;

internal sealed class DuplicarMesocicloCommandHandler : ICommandHandler<DuplicarMesocicloCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    private readonly IRutinaRepository _rutinaRepository;
    private readonly IDiaRutinaRepository _diaRutinaRepository;
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutina;

    public DuplicarMesocicloCommandHandler(IUnitOfWork uow, IRutinaRepository rutinaRepository, IDiaRutinaRepository diaRutinaRepository, IEjercicioDiaRutinaRepository ejercicioDiaRutina)
    {
        _uow = uow;
        _rutinaRepository = rutinaRepository;
        _diaRutinaRepository = diaRutinaRepository;
        _ejercicioDiaRutina = ejercicioDiaRutina;
    }

    public async Task<Result<Unit>> Handle(DuplicarMesocicloCommand request, CancellationToken cancellationToken)
    {
        await _uow.BeginAsync(cancellationToken);
        try
        {
            if(await _rutinaRepository.ExisteRutinaEnEsaFecha(request.UidUsuario,request.FechaInicio, request.FechaFin))
            {
                return Result.Failure<Unit>(RutinaErrors.FechasDuplicadas);
            }
            Rutina nuevaRutina=Rutina.Crear(
                request.UidUsuario,
                request.Nombre,
                DateOnly.FromDateTime(request.FechaInicio),
                DateOnly.FromDateTime(request.FechaFin));
            await _rutinaRepository.AddAsync(nuevaRutina,cancellationToken);
            IReadOnlyCollection<DiaRutina> dias=await _diaRutinaRepository.GetAllAsync(request.UidRutina,cancellationToken);
            List<DiaRutina> DiasDuplicados= new List<DiaRutina>();
            foreach(var dia in dias)
            {
                DiaRutina diaObtenidoConEjercicios=await _diaRutinaRepository.GetDiaByIdWithEjerciciosAsync(dia.Id);
                // List<EjercicioDiaRutina> ejercicios=new List<EjercicioDiaRutina>();
                // foreach(var day in diaObtenidoConEjercicios.EjerciciosDiaRutinas)
                // {
                //     EjercicioDiaRutina ejercicio=EjercicioDiaRutina.Crear(day.EjercicioUid,day.UidDia,day.Orden,day.Datos);
                //     ejercicios.Add(ejercicio);

                // }
                DiaRutina diarutina=DiaRutina.DuplicarDiaRutinaWithEjercicio
                (
                    nuevaRutina.Id,
                    dia.Nombre,
                    dia.DiaDeLaSemana,
                    diaObtenidoConEjercicios.EjerciciosDiaRutinas.ToList()
                ).Value;
                DiasDuplicados.Add(diarutina);
                
                // await _diaRutinaRepository.AddAsync(diarutina,cancellationToken);
                // IEnumerable<EjercicioDiaRutinaDTO> datosDiaRutina=await _ejercicioDiaRutina.GetByIdDia(dia.Id,cancellationToken);
              
                // foreach(var datos in datosDiaRutina)
                // {
                //     DatosEjercicio nuevosDatos=new DatosEjercicio(datos.Series,datos.ObjetivoReps,datos.ObjetivoRIR,datos.TiempoDescanso);
                //     EjercicioDiaRutina ejercicioDiaRutina=EjercicioDiaRutina.Crear(datos.UidEjercicios,datos.Orden,nuevosDatos);
                //     await _ejercicioDiaRutina.AddAsync(ejercicioDiaRutina,diarutina.Id,cancellationToken);
                // }
            }
            await _diaRutinaRepository.AddVariosAsync(DiasDuplicados,cancellationToken);
            foreach (var days in DiasDuplicados)
            {
                await _ejercicioDiaRutina.AddVariosAsync(days.EjerciciosDiaRutinas.ToList(),cancellationToken);
            }

            await _uow.CommitAsync();
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