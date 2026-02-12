using DiarioEntrenamiento.Domain.Abstractions;
using MediatR;

namespace DiarioEntrenamiento.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}
public interface IBaseCommand{}