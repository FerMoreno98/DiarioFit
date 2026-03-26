using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.DTOs;

namespace DiarioEntrenamiento.Application.Sesiones.Obtener1RMPorSesion;

internal sealed class Obtener1RMPorSesionQueryHandler : IQueryHandler<Obtener1RMPorSesionQuery, List<SortedDictionary<DateTime,SerieDto>>>
{
    private readonly ISerieRepository _serieRepository;
    private readonly IRutinaRepository _rutinaRepository;
    private readonly IDiaRutinaRepository _diaRutinaRepository;
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutinaRepository;

    public Obtener1RMPorSesionQueryHandler(ISerieRepository serieRepository, IRutinaRepository rutinaRepository, IDiaRutinaRepository diaRutinaRepository, IEjercicioDiaRutinaRepository ejercicioDiaRutinaRepository)
    {
        _serieRepository = serieRepository;
        _rutinaRepository = rutinaRepository;
        _diaRutinaRepository = diaRutinaRepository;
        _ejercicioDiaRutinaRepository = ejercicioDiaRutinaRepository;
    }

    public async Task<Result<List<SortedDictionary<DateTime,SerieDto>>>> Handle(Obtener1RMPorSesionQuery request, CancellationToken cancellationToken)
    {
        Rutina currentRutina=await _rutinaRepository.GetCurrentAysnc(request.UidUsuario);
        IEnumerable<DiaRutina> diasDeRutinaCurrent=await _diaRutinaRepository.GetAllAsync(currentRutina.Id);
        List<Guid> listadoEjerciciosRutina=new List<Guid>();
        foreach(var dia in diasDeRutinaCurrent)
        {
            IEnumerable<Guid>uidejercicios=await _ejercicioDiaRutinaRepository.ObtenerUidEjerciciosDeDiaRutina(dia.Id);
            foreach(var uid in uidejercicios)
            {
                listadoEjerciciosRutina.Add(uid);
            }
        }
        
        List<SortedDictionary<DateTime,SerieDto>> ret=new List<SortedDictionary<DateTime,SerieDto>>();
        foreach(var uid in listadoEjerciciosRutina)
        {
            SortedDictionary<DateTime,SerieDto> diccionarioFechaRatio=new SortedDictionary<DateTime, SerieDto>();
            IEnumerable<SerieDto>historicoSeries=await _serieRepository.ObtenerHistoricoSeriesDeUnEjercicio(uid);
            foreach(var serie in historicoSeries)
            {
                if(serie.Rir is not null && serie.Rir!=""){
                decimal rirMedio = CalcularRirMedio(serie.Rir);

                decimal RMCalculado = Math.Round(serie.Peso *
                    (1 + (serie.Repeticiones + rirMedio) / 30m),2);
                serie.RMCalculado=RMCalculado;
                     diccionarioFechaRatio.TryAdd(serie.FechaSesion, serie);    
                }     

            }
            if(diccionarioFechaRatio.Count>1)
            {
                    ret.Add(diccionarioFechaRatio);
             
                
            }
        }
        return ret;
      
        
    }
    private static decimal CalcularRirMedio(string rir)
{
    var valores = rir
        .Split('-')
        .Select(v => int.Parse(v));

    return (decimal)valores.Average();
}

}