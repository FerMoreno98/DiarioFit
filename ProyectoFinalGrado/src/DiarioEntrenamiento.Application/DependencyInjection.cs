using DiarioEntrenamiento.Application.Abstractions.Behaviours;
using DiarioEntrenamiento.Application.Rutinas.CrearRutina;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DiarioEntrenamiento.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Configuration =>
        {
            Configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            Configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssemblyContaining<CrearRutinaCommand>();
        return services;
    }
}