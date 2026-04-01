

using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.RegistrosCorporales.Pliegues;

public class Pliegue : Entity<Guid>
{

    public Guid UidUsuario { get; private set; }

    public decimal? Abdominal { get; private set; }
    public decimal? Suprailiaco{get;private set;}
    public decimal? Tricipital { get; private set; }
    public decimal? Subescapular { get; private set; }
    public decimal? Muslo { get; private set; }
    public decimal? Pantorrilla { get; private set; }
    public decimal SumaDePliegues =>
        (Suprailiaco ?? 0) +
        (Abdominal ?? 0) +
        (Tricipital ?? 0) +
        (Subescapular ?? 0) +
        (Muslo ?? 0) +
        (Pantorrilla ?? 0);
    public decimal PorcentajeGrasoEstimado =>
    0.105m * SumaDePliegues + 2.05m;

    public DateTime FechaTomaPliegues { get; private set; }

    private Pliegue(
        Guid id,
        Guid uidUsuario,
        decimal? abdominal,
        decimal? suprailiaco,
        decimal? tricipital,
        decimal? subescapular,
        decimal? muslo,
        decimal? pantorrilla,
        DateTime fechaTomaPliegues)
        : base(id)
    {
        UidUsuario = uidUsuario;
        Suprailiaco=suprailiaco;
        Abdominal = abdominal;
        Tricipital = tricipital;
        Subescapular = subescapular;
        Muslo = muslo;
        Pantorrilla = pantorrilla;

        FechaTomaPliegues = fechaTomaPliegues;
    }
    public static Pliegue Crear(
        Guid uidUsuario,
        decimal? abdominal,
        decimal? suprailiaco,
        decimal? tricipital,
        decimal? subescapular,
        decimal? muslo,
        decimal? pantorrilla)
    {
        return new Pliegue(
            Guid.NewGuid(),
            uidUsuario,
            abdominal,
            suprailiaco,
            tricipital,
            subescapular,
            muslo,
            pantorrilla,
            DateTime.Now
        );
    }
        public static Pliegue CrearFromDataBase(
        Guid Uid,
        Guid uidUsuario,
        decimal? abdominal,
        decimal? suprailiaco,
        decimal? tricipital,
        decimal? subescapular,
        decimal? muslo,
        decimal? pantorrilla,
        DateTime Fecha)
    {
        return new Pliegue(
            Uid,
            uidUsuario,
            abdominal,
            suprailiaco,
            tricipital,
            subescapular,
            muslo,
            pantorrilla,
            Fecha
        );
    }
}

