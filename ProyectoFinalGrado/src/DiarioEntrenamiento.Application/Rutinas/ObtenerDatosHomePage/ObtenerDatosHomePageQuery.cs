using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDatosHomePage;

public sealed record ObtenerDatosHomePageQuery(Guid UidUsuario) : IQuery<HomePageDTO>;