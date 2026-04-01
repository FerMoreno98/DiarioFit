using System;

namespace DiarioEntrenamiento.Api.Controllers.MedidasCorporales.Requests;

public sealed record RegistrarPerimetrosRequest
(
    Guid uidUsuario,
    decimal? cuello,
    decimal? brazoDchoRelajado,
    decimal? brazoDchoTension,
    decimal? brazoIzqRelajado,
    decimal? brazoIzqTension,
    decimal? pecho,
    decimal? hombro,
    decimal? cintura,
    decimal? cadera,
    decimal? abdomen,
    decimal? musloDcho,
    decimal? musloIzq,
    decimal? pantorrillaDcha,
    decimal? pantorrillaIzq
);
