using System;

namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

internal class PerimetrosDTO
{
    public Guid Uid{ get; set; }
        public Guid UidUsuario { get; set; }

    public decimal Cuello { get; private set; }
    public decimal BrazoDchoRelajado { get; set; }
    public decimal BrazoDchoTension { get; set; }
    public decimal BrazoIzqRelajado { get;  set; }
    public decimal BrazoIzqTension { get;  set; }

    public decimal Pecho { get;  set; }
    public decimal Hombro { get;  set; }
    public decimal Cintura { get;  set; }
    public decimal Cadera { get;  set; }
    public decimal Abdomen { get;  set; }

    public decimal MusloDcho { get;  set; }
    public decimal MusloIzq { get;  set; }
    public decimal PantorrillaDcha { get;  set; }
    public decimal PantorrillaIzq { get;  set; }

    public DateTime FechaTomaDePerimetros { get;  set; }

}
