using DiarioEntrenamiento.Application.Abstractions.Messaging;

namespace DiarioEntrenamiento.Application.Rutinas.CrearRutina;

public record CrearRutinaCommand(
Guid UidUsuario,
string Nombre,
DateOnly FechaIncio,
DateOnly FechaFin
) : ICommand<Guid>;