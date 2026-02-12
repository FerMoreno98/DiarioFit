using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Sesiones.RegistrarSerie;

public sealed record RegistrarSerieCommand
(
    Guid UidDia,
    string Ejercicio, // creo que lo puedo pasar por parametro
    decimal? Peso,
    int? Repeticiones,
    string? Rir,
    int Serie // Sumar en frontend y comprobar en backend que numero de series hay
) : ICommand<RegistrarSesionResponse>;