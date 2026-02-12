using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerRutinas;

internal sealed class ObtenerRutinasQueryHandler : IQueryHandler<ObtenerRutinasQuery, Rutina>
{
    public Task<Result<Rutina>> Handle(ObtenerRutinasQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}