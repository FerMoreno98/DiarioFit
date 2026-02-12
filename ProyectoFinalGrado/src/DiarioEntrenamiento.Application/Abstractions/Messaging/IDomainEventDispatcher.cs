using DiarioEntrenamiento.Domain.Abstractions;
using MediatR;

namespace DiarioEntrenamiento.Application.Abstractions.Messaging;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvents> domainEvents, CancellationToken cancellationToken=default);
}