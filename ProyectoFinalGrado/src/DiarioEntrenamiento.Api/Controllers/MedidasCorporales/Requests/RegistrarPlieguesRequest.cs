namespace DiarioEntrenamiento.Api.Controllers.MedidasCorporales.Requests;

public sealed record RegistrarPlieguesRequest
(
        Guid uidUsuario,
        decimal? abdominal,
        decimal? suprailiaco,
        decimal? tricipital,
        decimal? subescapular,
        decimal? muslo,
        decimal? pantorrilla
);
