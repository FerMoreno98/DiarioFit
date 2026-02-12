using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using DiarioEntrenamiento.Domain.Usuarios.ValueObjects;

namespace DiarioEntrenamiento.Domain.Usuarios;
public interface IUsuarioRepository
{
    Task<Usuario> GetByIdAsync(Guid uid, CancellationToken cancellationToken = default);
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task<bool> esEmailUnico(string email, CancellationToken cancellationToken = default);
    Task<Usuario> GetByEmailAsync(string Email);
    Task ModificarUsuario(Guid uid, Nombre nombre, Apellidos apellidos, FechaNacimiento fechaNacimiento, CancellationToken cancellationToken = default);
}