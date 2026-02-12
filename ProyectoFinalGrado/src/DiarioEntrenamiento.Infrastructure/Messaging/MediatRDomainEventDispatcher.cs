using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using MediatR;

namespace DiarioEntrenamiento.Infrastructure.Messaging;

public sealed class MediatRDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublisher _publisher;

    public MediatRDomainEventDispatcher(IPublisher publisher)
    { 
        _publisher = publisher;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvents> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach(var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
    }
}