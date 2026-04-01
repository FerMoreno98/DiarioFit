namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

public record class PliegueDTO
{
    public Guid Uid { get; set; }

    public Guid UidUsuario { get; set; }

    public decimal? Abdominal { get; set; }

    public decimal? Suprailiaco { get; set; }

    public decimal? Tricipital { get; set; }

    public decimal? Subescapular { get; set; }

    public decimal? Muslo { get; set; }

    public decimal? Pantorrilla { get; set; }
    public DateTime FechaTomaPliegues{get;set;}
}
