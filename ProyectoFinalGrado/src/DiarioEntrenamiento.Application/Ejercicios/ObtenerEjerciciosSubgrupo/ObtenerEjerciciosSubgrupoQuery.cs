using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;

namespace DiarioEntrenamiento.Application.Ejercicios.ObtenerEjerciciosSubgrupo;

public sealed record ObtenerEjerciciosSubgrupoQuery(int idSubgrupo) : IQuery<List<Ejercicio>>;