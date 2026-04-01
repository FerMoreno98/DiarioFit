using System;

namespace DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues.Repository;

public interface IPliegueRepository
{
    Task Add(Pliegue pliegue);
    Task<List<Pliegue>> GetAllFromUserAsync(Guid UidUsuario);
}
