using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.DTOs;

namespace DiarioEntrenamiento.Application.Sesiones.ObtenerDatosUltimaSesion;

internal sealed class ObtenerDatosUltimaSesionQueryHandler 
: IQueryHandler<ObtenerDatosUltimaSesionQuery, Dictionary<string,List<UltimaSesionDto>>>
{
   private readonly ISesionRepository _sesionRepository;
   private readonly IEjercicioDiaRutinaRepository _ejerciciosDiaRutinaRepository;

    public ObtenerDatosUltimaSesionQueryHandler(ISesionRepository sesionRepository, IEjercicioDiaRutinaRepository ejerciciosDiaRutinaRepository)
    {
        _sesionRepository = sesionRepository;
        _ejerciciosDiaRutinaRepository = ejerciciosDiaRutinaRepository;
    }

    public async Task<Result<Dictionary<string,List<UltimaSesionDto>>>> Handle(ObtenerDatosUltimaSesionQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<UltimaSesionDto> SesionRecienHecha=await _sesionRepository.ObtenerSesionRecienHecha(request.UidDia);

        IEnumerable<UltimaSesionDto>ultimasesion=await _sesionRepository.ObtenerUltimaSesion(request.UidDia);
        // En el futuro, lo ideal seria coger el los ids de los ejercicios tanto de la rutina como de la sesión, de manera
        // que si el usuario en una sesion añade un ejercicio puntual, que aparezca tambien por pantalla
        IEnumerable<string>NombreEjercicios=await _ejerciciosDiaRutinaRepository.GetNombreEjercicioByIdDia(request.UidDia);
        Dictionary<string,List<UltimaSesionDto>>ultimaSesionDic=new Dictionary<string, List<UltimaSesionDto>>();
        foreach(var Nombre in NombreEjercicios)
        {
            ultimaSesionDic[Nombre]=new List<UltimaSesionDto>();
            foreach(var sesion in SesionRecienHecha)
            {
                if (sesion.Ejercicio == Nombre)
                {
                    ultimaSesionDic[Nombre].Add(sesion);
                }
            }
            foreach(var datos in ultimasesion)
            {
                if (Nombre == datos.Ejercicio)
                {
           
                   if (ultimaSesionDic.TryGetValue(Nombre, out var lista) &&
                        (lista==null || !lista.Any() || lista.First().FechaSesion<DateTime.Today))
                    {
                        ultimaSesionDic[Nombre].Add(datos);
                    } 
                        
                    
                }
            }

        }
        return ultimaSesionDic;

    }
}
