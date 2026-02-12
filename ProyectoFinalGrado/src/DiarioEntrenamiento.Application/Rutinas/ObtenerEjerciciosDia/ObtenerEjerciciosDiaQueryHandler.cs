using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerEjerciciosDia;

internal sealed class ObtenerEjerciciosDiaQueryHandler : IQueryHandler<ObtenerEjerciciosDiaQuery, IEnumerable<EjercicioDiaRutinaDTO>>
{
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutina;
    private readonly IEjercicioRepository _ejercicioRepository;

    public ObtenerEjerciciosDiaQueryHandler(IEjercicioDiaRutinaRepository ejercicioDiaRutina, IEjercicioRepository ejercicioRepository)
    {
        _ejercicioDiaRutina = ejercicioDiaRutina;
        _ejercicioRepository = ejercicioRepository;
    }
// Aqui en lugar de devolver una entidad de dominio, como es para mostrar datos por pantalla, por lo visto esta bien el usar DTO y devolver todos los datos a capon
    public async Task<Result<IEnumerable<EjercicioDiaRutinaDTO>>> Handle(ObtenerEjerciciosDiaQuery request, CancellationToken cancellationToken)
    {
        // Con esta solucion estoy permitiendo que se muestren ejercicios repetidos para que si alguien quiere hacer el mismo
        //ejercicio con distintos datos pueda hacerlo sin problema, la idea es validar que los datos sean distinos
        IEnumerable<EjercicioDiaRutinaDTO> ejercicios=await _ejercicioDiaRutina.ObtenerEjerciciosDiaConNombre(request.UidDia);
        return Result.Success<IEnumerable<EjercicioDiaRutinaDTO>>(ejercicios);


        // Esta solucion no permitia mostrar datos duplicados ya que al meter los ids en el diccionario, si habia dos iguales, petaba

        // List<EjercicioDiaConDetalleDto> ret=new List<EjercicioDiaConDetalleDto>();
        // if(ejercicios is null || ejercicios.Count()==0)
        // {
        //     return Result.Success<IReadOnlyCollection<EjercicioDiaConDetalleDto>>(ret);
        // }
        // List<Guid> IdsEjercicios=ejercicios.Select(e=>e.UidEjercicios).ToList();
        // List<Ejercicio> ejerciciosDetalle= await _ejercicioRepository.GetByIds(IdsEjercicios);
        // // revisar aqui por que no se pueden poner ejercicios repetidos por el diccionario la idea es manejarlo
        // Dictionary<Guid,Ejercicio>ejerciciosDetalleDictionary=ejerciciosDetalle.ToDictionary(e=>e.Id);
        
        // foreach(var key in ejerciciosDetalleDictionary.Keys)
        // {
        //     foreach(var element in ejercicios)
        //     {
        //         if (key == element.UidEjercicios)
        //         {
        //             var ejercicioDTO = new EjercicioDiaConDetalleDto(
                
        //                 element.UidEjercicioDiaRutina,
        //                 element.UidEjercicios,
        //                 ejerciciosDetalleDictionary[key].Nombre,
        //                 // ejerciciosDetalleDictionary[key].GruposMuscularesPrincipales,
        //                 // ejerciciosDetalleDictionary[key].GruposMuscularesSecundarios,
        //                 element.Series,
        //                 element.ObjetivoReps,
        //                 element.ObjetivoRIR,
        //                 element.TiempoDescanso
        //             );
        //             ret.Add(ejercicioDTO);              
        //         }
        //     }
            
        // }
        
   
    }
}

