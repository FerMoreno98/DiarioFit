namespace DiarioEntrenamiento.Api.Controllers.Usuarios;

public sealed record UsuariosCrearRequest(
Guid Uid,
string Nombre,
string Apellidos,
DateOnly FechaNacimiento,
string Email,
string Contrasena
);