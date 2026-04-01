
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPliegues;

public sealed record RegistrarPlieguesCommand
(
    Guid UidUsuario,
    decimal? Abdominal,
    decimal? Suprailiaco,
    decimal? Tricipital,
    decimal? Subescapular,
    decimal? Muslo,
    decimal? Pantorrilla
    
) : ICommand<Unit>;
