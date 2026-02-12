using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.ModificarRutina;

public sealed record ModificarRutinaCommand(Guid UidUsuario,Guid UidRutina,string Nombre,DateOnly FechaInicio,DateOnly FechaFin): ICommand<Unit>;