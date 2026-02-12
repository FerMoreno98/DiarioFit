using DiarioEntrenamiento.Application.Abstractions.Messaging;

namespace DiarioEntrenamiento.Application.Usuarios.Login;

public sealed record LoginQuery(string Email, string Contrasena) : IQuery<string>;