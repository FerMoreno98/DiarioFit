namespace DiarioEntrenamiento.Application.Abstractions.Clock;

public interface IClock
{
    DateTime Now { get; }
}