
namespace DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros.Repository;

public interface IPerimetrosRepository
{
    Task Add(Perimetro perimetro);
    Task<List<Perimetro>> GetAllFromUserAsync(Guid UidUsuario);
}
