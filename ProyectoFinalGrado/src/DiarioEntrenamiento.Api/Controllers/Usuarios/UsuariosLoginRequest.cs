namespace DiarioEntrenamiento.Api.Controllers.Usuarios;

public sealed record UsuariosLoginRequest(
string Email,
string Contrasena
);