using DiarioEntrenamiento.Application.Abstractions.Clock;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Sesiones;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDatosHomePage;

internal sealed class ObtenerDatosHomePageQueryHandler : IQueryHandler<ObtenerDatosHomePageQuery, HomePageDTO>
{
    private readonly IRutinaRepository _rutinaRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IClock _clock;
    private readonly IDiaRutinaRepository _diaRutinaRepository;
    private readonly IEjercicioRepository _ejercicioRepository;

    public ObtenerDatosHomePageQueryHandler(IRutinaRepository rutinaRepository, ISesionRepository sesionRepository, IClock clock, IDiaRutinaRepository diaRutinaRepository, IEjercicioRepository ejercicioRepository)
    {
        _rutinaRepository = rutinaRepository;
        _sesionRepository = sesionRepository;
        _clock = clock;
        _diaRutinaRepository = diaRutinaRepository;
        _ejercicioRepository = ejercicioRepository;
    }

    public async Task<Result<HomePageDTO>> Handle(ObtenerDatosHomePageQuery request, CancellationToken cancellationToken)
    {

        List<DiaRutinaHomeDto> DiasRutina=new List<DiaRutinaHomeDto>();
        Rutina Current=await _rutinaRepository.GetCurrentAysnc(request.UidUsuario);
        Rutina rutinaCompleta=await _rutinaRepository.ObtenerRutinaCompleta(Current.Id);
        foreach(var dias in rutinaCompleta.Dias)
        {
            bool Hecha=false;
         
            if(await _sesionRepository.ExisteSesion(dias.Uid_rutina,dias.Id,_clock.Now) is not null)
            {
                Hecha=true;
            }
            List<EjercicioDiaRutinaHomeDto> ejercicios=new List<EjercicioDiaRutinaHomeDto>();
            List<Guid> ids = dias.EjerciciosDiaRutinas.Select(e => e.Id).Distinct().ToList();
            List<Ejercicio> nombresById = await _ejercicioRepository.GetByIds(ids);
            foreach(var ejercicio in dias.EjerciciosDiaRutinas)
            {
                EjercicioDiaRutinaHomeDto ejer=new EjercicioDiaRutinaHomeDto
                {
                    Ejercicio=nombresById
                                .FirstOrDefault(e => e.Id == ejercicio.Id)?.Nombre,
                    Series=ejercicio.Datos.Series,
                    ObjetivoReps=ejercicio.Datos.RangoRepsObjetivo,
     
                };
                ejercicios.Add(ejer);
            }
            DiaRutinaHomeDto diaRutina= new DiaRutinaHomeDto
            {
                UidDia=dias.Id,
                NombreDiaRutina=dias.Nombre,
                DiaDeLaSemana=dias.DiaDeLaSemana,
                datosEjercicios=ejercicios,
                RutinaHecha=Hecha
            };
            DiasRutina.Add(diaRutina);

        }
        HomePageDTO ret=new HomePageDTO(
        rutinaCompleta.Id,
        rutinaCompleta.Nombre,
        DiasRutina
        );
        return ret;

        // return Result.Success(await _rutinaRepository.ObtenerDatosHomePage(request.UidUsuario));
    }
}