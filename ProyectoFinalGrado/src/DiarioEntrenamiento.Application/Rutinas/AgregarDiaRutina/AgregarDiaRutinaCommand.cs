using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Rutinas.Entidad;

namespace DiarioEntrenamiento.Application.Rutinas.AgregarDiaRutina;

public record AgregarDiaRutinaCommand(
    Guid UidRutina,
    string Nombre,
    string DiaDeLaSemana
) : ICommand<IReadOnlyCollection<DiaRutina>>;