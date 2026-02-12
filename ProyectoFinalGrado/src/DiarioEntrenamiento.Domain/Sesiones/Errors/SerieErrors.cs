using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Sesiones.Errors;

public static class SerieErrors
{
  public static readonly Error FormatoInvalidoRir=new Error("Serie.FormatoInvalidoRir","El rango de RIR debe ser 'n' o 'n-n' (ej: 2 o 1-3).");
}