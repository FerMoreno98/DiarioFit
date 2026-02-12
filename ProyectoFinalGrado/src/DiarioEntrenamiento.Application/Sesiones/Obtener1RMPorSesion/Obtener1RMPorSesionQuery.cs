using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Sesiones.DTOs;

namespace DiarioEntrenamiento.Application.Sesiones.Obtener1RMPorSesion;


public sealed record Obtener1RMPorSesionQuery(Guid UidUsuario) : IQuery<List<SortedDictionary<DateTime,SerieDto>>>;