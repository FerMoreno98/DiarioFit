using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.ValueObjects;
using MediatR;

namespace DiarioEntrenamiento.Application.Usuarios.ModificarUsuario;

internal sealed class ModificarUsuarioCommandHandler : ICommandHandler<ModificarUsuarioCommand, Unit>
{

    private readonly IUsuarioRepository _usuarioRepository;

    public ModificarUsuarioCommandHandler(IUnitOfWork uow, IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<Unit>> Handle(ModificarUsuarioCommand request, CancellationToken cancellationToken)
    {
  
            Nombre nombre = new Nombre(request.Nombre);
            Apellidos apellido = new Apellidos(request.Apellidos);
            FechaNacimiento fecha = new FechaNacimiento(request.FechaNacimiento);
            await _usuarioRepository.ModificarUsuario(request.Uid, nombre, apellido, fecha);
            return Result.Success<Unit>(Unit.Value);
            
    }

}