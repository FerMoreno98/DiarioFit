

using DiarioEntrenamiento.Application.Abstractions.Clock;

namespace DiarioEntrenamiento.Infrastructure.Clock;

public sealed class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}
