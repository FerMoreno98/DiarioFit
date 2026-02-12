using DiarioEntrenamiento.Application.Abstractions.Email;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Application.Abstractions.Services;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using MediatR;

namespace DiarioEntrenamiento.Application.Usuarios.PasswordResetRequest;

// internal sealed class PasswordResetRequestCommandHandler : ICommandHandler<PasswordResetRequestCommand, Unit>
// {
//     private readonly IUsuarioRepository _usuarioRepository;
//     private readonly IEmailService _emailService;
//     private readonly ICodeGenerator _codeGenerator;

//     public async Task<Result<Unit>> Handle(PasswordResetRequestCommand request, CancellationToken cancellationToken)
//     {
//         Usuario? usuario = await _usuarioRepository.GetByEmailAsync(request.Email);
//         if (usuario is not null)
//         {
//             var (codigoGenerado,codigoGeneradoHash) = _codeGenerator.GenerateCode();
//             string cuerpo=""
//             _emailService.SendAsync(usuario.Email.Value,"Recuperacion de contraseña", );
//         }
        
//     }
// }