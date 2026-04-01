
using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros;

public sealed class Perimetro : Entity<Guid>
{
private Perimetro(
    Guid id,
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
    decimal? pantorrillaIzq,
    DateTime fechaTomaDePerimetros)
    : base(id)
{
    UidUsuario = uidUsuario;

    Cuello = cuello;
    BrazoDchoRelajado = brazoDchoRelajado;
    BrazoDchoTension = brazoDchoTension;
    BrazoIzqRelajado = brazoIzqRelajado;
    BrazoIzqTension = brazoIzqTension;

    Pecho = pecho;
    Hombro = hombro;
    Cintura = cintura;
    Cadera = cadera;
    Abdomen = abdomen;

    MusloDcho = musloDcho;
    MusloIzq = musloIzq;
    PantorrillaDcha = pantorrillaDcha;
    PantorrillaIzq = pantorrillaIzq;

    FechaTomaDePerimetros = fechaTomaDePerimetros;
}

    public Guid UidUsuario { get; private set; }

    public decimal? Cuello { get; private set; }
    public decimal? BrazoDchoRelajado { get; private set; }
    public decimal? BrazoDchoTension { get; private set; }
    public decimal? BrazoIzqRelajado { get; private set; }
    public decimal? BrazoIzqTension { get; private set; }

    public decimal? Pecho { get; private set; }
    public decimal? Hombro { get; private set; }
    public decimal? Cintura { get; private set; }
    public decimal? Cadera { get; private set; }
    public decimal? Abdomen { get; private set; }

    public decimal? MusloDcho { get; private set; }
    public decimal? MusloIzq { get; private set; }
    public decimal? PantorrillaDcha { get; private set; }
    public decimal? PantorrillaIzq { get; private set; }

    public DateTime FechaTomaDePerimetros { get; private set; }

    public static Perimetro Crear(
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
    decimal? pantorrillaIzq)
{
    return new Perimetro(
        Guid.NewGuid(),
        uidUsuario,
        cuello,
        brazoDchoRelajado,
        brazoDchoTension,
        brazoIzqRelajado,
        brazoIzqTension,
        pecho,
        hombro,
        cintura,
        cadera,
        abdomen,
        musloDcho,
        musloIzq,
        pantorrillaDcha,
        pantorrillaIzq,
        DateTime.Now
    );
}

}
