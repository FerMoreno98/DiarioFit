using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;
using MediatR;

namespace DiarioEntrenamiento.Application.Sesiones.IniciarSesionEntrenamiento;

public record IniciarSesionCommand(
    Guid UidUsuario,
    Guid UidDia,
    int? Sueno,
    int? Motivacion,
    int? ERP
) : ICommand<SesionDto>;