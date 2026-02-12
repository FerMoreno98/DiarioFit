using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios.Errors;

namespace DiarioEntrenamiento.Domain.Usuarios.ValueObjects;

public record FechaNacimiento(DateOnly Value);
