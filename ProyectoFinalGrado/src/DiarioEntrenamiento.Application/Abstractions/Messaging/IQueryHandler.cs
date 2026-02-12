using DiarioEntrenamiento.Domain.Abstractions;
using MediatR;

namespace DiarioEntrenamiento.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery,TResponse> : IRequestHandler<TQuery,Result<TResponse>>where TQuery : IQuery<TResponse>
{
    
}