using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas;


namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDatosEditarMesociclo;

internal sealed class ObtenerDatosEditarMesocicloQueryHandler : IQueryHandler<ObtenerDatosEditarMesocicloQuery, List<DatosEditarMesocicloDto>>
{
    private readonly IRutinaRepository _rutinaRepository;
    private readonly IDiaRutinaRepository _diaRutinaRepository;

    public ObtenerDatosEditarMesocicloQueryHandler(IRutinaRepository rutinaRepository, IDiaRutinaRepository diaRutinaRepository)
    {
        _rutinaRepository = rutinaRepository;
        _diaRutinaRepository = diaRutinaRepository;
    }

    public async Task<Result<List<DatosEditarMesocicloDto>>> Handle(ObtenerDatosEditarMesocicloQuery request, CancellationToken cancellationToken)
    {
        List<DatosEditarMesocicloDto> ret=new List<DatosEditarMesocicloDto>();
        List<Rutina> rutinas=await _rutinaRepository.GetAllWithDias(request.UidUsuario);
        foreach(var rutina in rutinas)
        {
            DatosEditarMesocicloDto dato =new DatosEditarMesocicloDto
            {
                Uid=rutina.Id,
                UidUsuario=rutina.UidUsuario,
                Nombre=rutina.Nombre,
                FechaInicio=rutina.FechaInicio.ToDateTime(TimeOnly.MinValue),
                FechaFin=rutina.FechaFin.ToDateTime(TimeOnly.MinValue),
                diasEntrenamiento=rutina.Dias
            };
            ret.Add(dato);
        }
        return Result.Success(ret);

    }
}