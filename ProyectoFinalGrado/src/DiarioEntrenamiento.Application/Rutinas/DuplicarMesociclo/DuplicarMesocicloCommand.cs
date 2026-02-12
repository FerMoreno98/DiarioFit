using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Rutinas.DuplicarMesociclo;

public sealed record DuplicarMesocicloCommand(Guid UidUsuario,Guid UidRutina,string Nombre,DateTime FechaInicio,DateTime FechaFin):ICommand<Unit>;