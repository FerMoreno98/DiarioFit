using DiarioEntrenamiento.Application.Abstractions.Clock;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Email;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Application.Abstractions.Security;
using DiarioEntrenamiento.Domain.Ejercicios;
using DiarioEntrenamiento.Domain.GruposMusculares;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Sesiones;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Infrastructure.Clock;
using DiarioEntrenamiento.Infrastructure.Data;
using DiarioEntrenamiento.Infrastructure.Email;
using DiarioEntrenamiento.Infrastructure.Messaging;
using DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;
using DiarioEntrenamiento.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiarioEntrenamiento.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRutinaRepository, RutinaRepository>();
        services.AddScoped<IDiaRutinaRepository,DiaRutinaRepository>();
        services.AddScoped<IEjercicioDiaRutinaRepository,EjercicioDiaRutinaRepository>();
        services.AddScoped<IGrupoMuscularRepository,GrupoMuscularRepository>();
        services.AddScoped<IEjercicioRepository,EjercicioRepository>();
        services.AddScoped<ISesionRepository,SesionRepository>();
        services.AddScoped<ISubGrupoMuscularRepository,SubGrupoMuscularRepository> ();
        services.AddScoped<ISerieRepository,SerieRepository>();
        var connectionString = configuration.GetConnectionString("Postgres")
        ?? throw new ArgumentNullException(nameof(configuration));
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        services.AddScoped<IClock,SystemClock>();
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        return services;
    }
}