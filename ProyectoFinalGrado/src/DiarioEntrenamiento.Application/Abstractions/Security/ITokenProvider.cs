using DiarioEntrenamiento.Domain.Usuarios.Entidad;

namespace DiarioEntrenamiento.Application.Abstractions.Security;

public interface ITokenProvider
{
    string Crear(Usuario usuario);
}