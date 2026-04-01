
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using MediatR;

namespace DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPerimetros;

public sealed record RegistrarPerimetrosCommand(
    Guid UidUsuario,

    decimal? Cuello,
    decimal? BrazoDchoRelajado,
    decimal? BrazoDchoTension,
    decimal? BrazoIzqRelajado,
    decimal? BrazoIzqTension,

    decimal? Pecho,
    decimal? Hombro,
    decimal? Cintura,
    decimal? Cadera,
    decimal? Abdomen,

    decimal? MusloDcho,
    decimal? MusloIzq,
    decimal? PantorrillaDcha,
    decimal? PantorrillaIzq
) : ICommand<Unit>;
