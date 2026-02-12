using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Application.Abstractions.Security;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using DiarioEntrenamiento.Domain.Usuarios.Errors;

namespace DiarioEntrenamiento.Application.Usuarios.Login;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, string>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _uow;
    private readonly IRutinaRepository _rutinaRepository;

    public LoginQueryHandler(
    IUsuarioRepository usuarioRepository,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider,
    IUnitOfWork uow,
    IRutinaRepository rutinaRepository)
    {
     
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _uow = uow;
        _rutinaRepository=rutinaRepository;
    }

    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
       
            Usuario? usuario = await _usuarioRepository.GetByEmailAsync(request.Email);
            if (usuario is null)
            {
                return Result.Failure<string>(UsuarioErrors.EmailInexistente);
            }
            bool Verificada = _passwordHasher.Verify(request.Contrasena, usuario.Contrasena.Value);
            if (!Verificada)
            {
                return Result.Failure<string>(UsuarioErrors.ContrasenaIncorrecta);
            }
            string token = _tokenProvider.Crear(usuario);
            return token;

    }
}