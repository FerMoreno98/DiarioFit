using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.DTOs;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;

namespace DiarioEntrenamiento.Application.Sesiones.Obtener1RMPorSesion;

internal sealed class Obtener1RMPorSesionQueryHandler : IQueryHandler<Obtener1RMPorSesionQuery, List<SortedDictionary<DateTime,SerieDto>>>
{
    private readonly ISerieRepository _serieRepository;
    private readonly IRutinaRepository _rutinaRepository;

    private readonly ISesionRepository _sesionRepository;
    private readonly IEjercicioRepository _ejercicioRepository;

    public Obtener1RMPorSesionQueryHandler(ISerieRepository serieRepository, IRutinaRepository rutinaRepository, IDiaRutinaRepository diaRutinaRepository, IEjercicioDiaRutinaRepository ejercicioDiaRutinaRepository, ISesionRepository sesionRepository, IEjercicioRepository ejercicioRepository)
    {
        _serieRepository = serieRepository;
        _rutinaRepository = rutinaRepository;
        _sesionRepository = sesionRepository;
        _ejercicioRepository = ejercicioRepository;
    }

    public async Task<Result<List<SortedDictionary<DateTime,SerieDto>>>> Handle(Obtener1RMPorSesionQuery request, CancellationToken cancellationToken)
    {
        Rutina currentRutina = await _rutinaRepository.GetCurrentAysnc(request.UidUsuario);
        Rutina RutinaConDiasYEjercicios = await _rutinaRepository.ObtenerRutinaCompleta(currentRutina.Id);
    
        List<SortedDictionary<DateTime, SerieDto>> ret = new List<SortedDictionary<DateTime, SerieDto>>();
        List<Guid> UidsEjerciciosRutinaActual = RutinaConDiasYEjercicios.Dias
                                                    .SelectMany(dia => dia.EjerciciosDiaRutinas)
                                                    .Select(ejercicio => ejercicio.EjercicioUid)
                                                    .ToList();

        List<Sesion> Sesiones = await _sesionRepository.ObtenerSesionesCompletasPorUidSerie(request.UidUsuario, UidsEjerciciosRutinaActual);
        List<Ejercicio> ejercicios = await _ejercicioRepository.GetByIds(UidsEjerciciosRutinaActual);
        var seriesDto = Sesiones
            .SelectMany(s => s.series)  
            .Join(ejercicios,           
                serie => serie.UidEjercicio,
                ejercicio => ejercicio.Id,
                (serie, ejercicio) => new SerieDto(
                    serie.Uid,
                    serie.UidEjercicio,
                    serie.UidSesion,
                    ejercicio.Nombre,
                    serie.Peso,
                    serie.Repeticiones,
                    serie.RIR))
            .ToList();
        
        var sesionesDict = Sesiones.ToDictionary(s => s.Uid, s => s.FechaSesion);

    
ret = seriesDto
    .GroupBy(dto => new { dto.UidEjercicio, Fecha = sesionesDict[dto.UidSesion] })
    .Select(grupo => new SortedDictionary<DateTime, SerieDto>(
        new Dictionary<DateTime, SerieDto>
        {
            { grupo.Key.Fecha, grupo.First() } // Tomas la primera serie o la que necesites
        }
    ))
    .ToList();
        
        return ret;
      
        
    }


}