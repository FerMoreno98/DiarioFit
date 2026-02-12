using DiarioEntrenamiento.Domain.Abstractions;
using MediatR;

namespace DiarioEntrenamiento.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>//al enviar un IMediator.send le estoy diciendo a mediatR que me devuelva un Result con una respuesta generica
{
    
}