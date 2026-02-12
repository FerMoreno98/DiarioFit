using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;

namespace DiarioEntrenamiento.Application.Ejercicios.ObtenerEjerciciosSubgrupo;

internal sealed class ObtenerEjerciciosSubgrupoQueryHandler : IQueryHandler<ObtenerEjerciciosSubgrupoQuery, List<Ejercicio>>
{
    private readonly IEjercicioRepository _ejercicioRepository;

    public ObtenerEjerciciosSubgrupoQueryHandler(IEjercicioRepository ejercicioRepository)
    {
        _ejercicioRepository = ejercicioRepository;
    }

    public async Task<Result<List<Ejercicio>>> Handle(ObtenerEjerciciosSubgrupoQuery request, CancellationToken cancellationToken)
    {
        var result=await _ejercicioRepository.GetbySubGrupoMuscular(request.idSubgrupo);
         return Result.Success(result);
    }
}