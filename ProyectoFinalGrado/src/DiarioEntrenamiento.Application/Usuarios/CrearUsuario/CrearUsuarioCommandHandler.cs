using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Application.Abstractions.Security;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.Errors;
using DiarioEntrenamiento.Domain.Usuarios.ValueObjects;


namespace DiarioEntrenamiento.Application.Usuarios.CrearUsuario;

internal sealed class CrearUsuarioCommandHandler : ICommandHandler<CrearUsuarioCommand, Guid>
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDomainEventDispatcher _events;

    public CrearUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IDomainEventDispatcher events)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _events = events;
    }

    public async Task<Result<Guid>> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginAsync(cancellationToken);
        try
        {
            var nombre = new Nombre(request.Nombre);
            var apellidos = new Apellidos(request.Apellidos);
            if(!await _usuarioRepository.esEmailUnico(request.Email, cancellationToken))
            {
                return Result.Failure<Guid>(EmailErrors.EmailEnUso);
            }
            var email = Email.Crear(request.Email);
            if (email.IsFailure)
            {
                return Result.Failure<Guid>(email.Error);
            }
      
            var complejidad = ContrasenaHash.ValidarComplejidad(request.Contrasena);
            if (complejidad.IsFailure)
            {
                return Result.Failure<Guid>(complejidad.Error);
            }
            var HashPassword = ContrasenaHash.Crear(_passwordHasher.HashPassword(request.Contrasena));
            if (HashPassword.IsFailure)
            {
                return Result.Failure<Guid>(HashPassword.Error);
            }
       
            var fechaNacimiento = new FechaNacimiento(request.FechaNacimiento);
            var usuario = Domain.Usuarios.Entidad.Usuario.Crear(nombre, apellidos, fechaNacimiento, email.Value, HashPassword.Value);
            await _usuarioRepository.AddAsync(usuario,cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _events.DispatchAsync(usuario.GetDomainEvents(), cancellationToken);
            usuario.ClearDomainEvents();
            return usuario.Id;
        }
        catch 
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _unitOfWork.DisposeAsync();
        }
    }
}