namespace DiarioEntrenamiento.Api.Controllers.Usuarios;

public sealed record UsuarioModificarRequest(
string Nombre,
string Apellidos,
DateOnly FechaNacimiento

);