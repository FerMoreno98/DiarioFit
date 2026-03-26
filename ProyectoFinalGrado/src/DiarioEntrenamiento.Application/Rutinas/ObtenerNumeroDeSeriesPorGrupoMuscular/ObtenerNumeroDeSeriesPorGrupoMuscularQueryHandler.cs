using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.GruposMusculares;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Usuarios.ValueObjects;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerNumeroDeSeriesPorGrupoMuscular;

internal sealed class ObtenerNumeroDeSeriesPorGrupoMuscularQueryHandler
: IQueryHandler<ObtenerNumeroDeSeriesPorGrupoMuscularQuery, Dictionary<string, int>>
{
    private readonly IEjercicioDiaRutinaRepository _ejercicioDiaRutinaRepository;
    private readonly IRutinaRepository _rutinaRepository;
    private readonly IEjercicioRepository _ejercicioRepository;


    public ObtenerNumeroDeSeriesPorGrupoMuscularQueryHandler(IEjercicioDiaRutinaRepository ejercicioDiaRutinaRepository, IRutinaRepository rutinaRepository, IDiaRutinaRepository diaRutinaRepository, IEjercicioRepository ejercicioRepository)
    {
        _ejercicioDiaRutinaRepository = ejercicioDiaRutinaRepository;
        _rutinaRepository = rutinaRepository;
        _ejercicioRepository = ejercicioRepository;
    }

    public async Task<Result<Dictionary<string, int>>> Handle(ObtenerNumeroDeSeriesPorGrupoMuscularQuery request, CancellationToken cancellationToken)
    {
        Rutina CurrentRutine=await _rutinaRepository.GetCurrentAysnc(request.UidUsuario,cancellationToken);
        IEnumerable<EjercicioDiaRutina> ejercicioDiarutina = await
            _ejercicioDiaRutinaRepository.ObtenerEjerciciosDiaRutinaDeRutina(CurrentRutine.Id);
        IEnumerable<(string ejercicio, string grupo)> RelacionEjerciciosGrupos =
            await _ejercicioRepository.ObtenerRelacionEjercioGrupoMuscular();
        var diccionario = RelacionEjerciciosGrupos
    .GroupBy(x => x.ejercicio)
    .ToDictionary(
        g => g.Key,
        g => g.Select(x => x.grupo).ToList()
    );
        Dictionary<string, int> ret = new Dictionary<string, int>();
        foreach (var ejercicio in ejercicioDiarutina)
        {
            string NombreEjercicio = await _ejercicioRepository.GetNombreById(ejercicio.EjercicioUid);
            List<string> GrupoMuscular = diccionario[NombreEjercicio];
            foreach (var grupom in GrupoMuscular)
            {
                if (!ret.ContainsKey(grupom))
                {
                    ret.TryAdd(grupom, ejercicio.Datos.Series);
                }
                else
                {
                    ret[grupom] += ejercicio.Datos.Series;
                }
            }
        }
        return ret;

    }
}