
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.Usuarios.ModificarUsuario;

public record ModificarUsuarioCommand(
Guid Uid,
string Nombre,
string Apellidos,
DateOnly FechaNacimiento
) : ICommand<Unit>;