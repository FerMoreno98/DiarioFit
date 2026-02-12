using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.GruposMusculares;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerNumeroDeSeriesPorGrupoMuscular;

internal sealed class ObtenerNumeroDeSeriesPorGrupoMuscularQueryHandler
: IQueryHandler<ObtenerNumeroDeSeriesPorGrupoMuscularQuery, Dictionary<string, int>>
{
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutinaRepository;
    private readonly IRutinaRepository _rutinaRepository;
    private readonly IGrupoMuscularRepository _grupoMuscularRepository;

    public ObtenerNumeroDeSeriesPorGrupoMuscularQueryHandler(IEjercicioDiaRutinaRepository ejercicioDiaRutinaRepository, IRutinaRepository rutinaRepository, IGrupoMuscularRepository grupoMuscularRepository)
    {
        _ejercicioDiaRutinaRepository = ejercicioDiaRutinaRepository;
        _rutinaRepository = rutinaRepository;
        _grupoMuscularRepository = grupoMuscularRepository;
    }

    public async Task<Result<Dictionary<string, int>>> Handle(ObtenerNumeroDeSeriesPorGrupoMuscularQuery request, CancellationToken cancellationToken)
    {
        Rutina currentRutine=await _rutinaRepository.GetCurrentAysnc(request.UidUsuario);
        // Obtengo el IdEjercicio, el subgrupo muscular al que pertenece ese ejercicio y las series de una rutina
        IEnumerable<DatosGraficaGruposMuscularesDto> ejerciciosRutina=await _ejercicioDiaRutinaRepository.ObtenerEjerciciosRutina(currentRutine.Id);
        Dictionary<string,int> ret=new Dictionary<string,int>();
        IEnumerable<GrupoMuscular>GruposMusculares=await _grupoMuscularRepository.GetAll();
        foreach(var ejercicios in ejerciciosRutina)
        {
            GrupoMuscular grupoDelEjercicio=await _grupoMuscularRepository.ObtenerGrupoPorSubGrupo(ejercicios.IdSubGrupoMuscular);
            if (!ret.ContainsKey(grupoDelEjercicio.Nombre))
            {
                ret.Add(grupoDelEjercicio.Nombre,ejercicios.Series);
            }
            else
            {
                ret[grupoDelEjercicio.Nombre]+=ejercicios.Series;
            }
        }
        return ret;

    }
}