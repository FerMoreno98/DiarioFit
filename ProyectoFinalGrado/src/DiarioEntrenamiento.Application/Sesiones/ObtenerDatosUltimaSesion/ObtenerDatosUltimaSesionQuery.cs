using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Sesiones.DTOs;

namespace DiarioEntrenamiento.Application.Sesiones.ObtenerDatosUltimaSesion;

public sealed record ObtenerDatosUltimaSesionQuery
(
Guid UidDia
) : IQuery<Dictionary<string,List<UltimaSesionDto>>>;