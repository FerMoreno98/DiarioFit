using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros;

namespace DiarioEntrenamiento.Application.MedidasCorporales.ObtenerPerimetros;

public sealed record ObtenerPerimetrosQuery(Guid UidUsuario) : IQuery<List<Perimetro>>;

