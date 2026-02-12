using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDatosEditarMesociclo;

public sealed record ObtenerDatosEditarMesocicloQuery(Guid UidUsuario) : IQuery<List<DatosEditarMesocicloDto>>;