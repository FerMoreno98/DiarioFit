
using DiarioEntrenamiento.Application.Abstractions.Messaging;

namespace DiarioEntrenamiento.Application.Usuarios.CrearUsuario;

public record CrearUsuarioCommand
(Guid Id,
string Nombre,
string Apellidos,
DateOnly FechaNacimiento,
string Email,
string Contrasena) : ICommand<Guid>;