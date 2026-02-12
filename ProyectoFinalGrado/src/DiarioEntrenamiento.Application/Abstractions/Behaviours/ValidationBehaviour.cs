using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace DiarioEntrenamiento.Application.Abstractions.Behaviours;

public class ValidationBehaviour<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);
        var validationErrors =
        _validators.Select(validators => validators.Validate(context))
        .Where(validationResult => validationResult.Errors.Any())
        .SelectMany(validationResult => validationResult.Errors)
        .Select(validateFailure => new ValidationError(validateFailure.PropertyName, validateFailure.ErrorMessage)).ToList();
        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}