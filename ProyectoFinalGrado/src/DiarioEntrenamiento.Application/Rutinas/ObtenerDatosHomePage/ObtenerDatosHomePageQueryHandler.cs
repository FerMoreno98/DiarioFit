using DiarioEntrenamiento.Application.Abstractions.Clock;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Sesiones;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDatosHomePage;

internal sealed class ObtenerDatosHomePageQueryHandler : IQueryHandler<ObtenerDatosHomePageQuery, HomePageDTO>
{
    private readonly IRutinaRepository _rutinaRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IClock _clock;

    public ObtenerDatosHomePageQueryHandler(IRutinaRepository rutinaRepository, ISesionRepository sesionRepository, IClock clock)
    {
        _rutinaRepository = rutinaRepository;
        _sesionRepository = sesionRepository;
        _clock = clock;
    }

    public async Task<Result<HomePageDTO>> Handle(ObtenerDatosHomePageQuery request, CancellationToken cancellationToken)
    {
        RutinaHomeDto datos1=await _rutinaRepository.ObtenerDatosHomePageRutina(request.UidUsuario);
        IEnumerable<DiaRutinaHomeDto> datos2=await _rutinaRepository.ObtenerDatosHomePageDiaRutina(datos1.UidRutina);
        IEnumerable<EjercicioDiaRutinaHomeDto> ejercicios=new List<EjercicioDiaRutinaHomeDto>();
        
        
        foreach(var dias in datos2)
        {
            ejercicios = await _rutinaRepository.ObtenerDatosHomePageEjercicioDiaRutina(dias.UidDia);
            dias.datosEjercicios = ejercicios.ToList();
            if(await _sesionRepository.ExisteSesion(datos1.UidRutina,dias.UidDia,_clock.Now) is not null)
            {
                dias.RutinaHecha=true;
            }
            else
            {
                dias.RutinaHecha=false;
            }
        }
        HomePageDTO ret=new HomePageDTO(
        datos1.UidRutina,
        datos1.NombreRutina,
        datos2
        );
        return ret;

        // return Result.Success(await _rutinaRepository.ObtenerDatosHomePage(request.UidUsuario));
    }
}