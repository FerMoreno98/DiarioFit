using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerEjerciciosDia;

public sealed record ObtenerEjerciciosDiaQuery(Guid UidDia) : IQuery<IEnumerable<EjercicioDiaRutinaDTO>>;