using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues;

namespace DiarioEntrenamiento.Application.MedidasCorporales.ObtenerPliegues;

public sealed record ObtenerPlieguesQuery(Guid UidUsuario) : IQuery<List<Pliegue>>;

