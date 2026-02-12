using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Security;
using DiarioEntrenamiento.Application.Usuarios.Login;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using DiarioEntrenamiento.Domain.Usuarios.Errors;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace Application.Tests.Usuarios.Queries;

public class LoginQueryHandlerTests
{
     private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _uow;

    public LoginQueryHandlerTests()
    {
        _connectionFactory = Substitute.For<ISqlConnectionFactory>();
        _usuarioRepository = Substitute.For<IUsuarioRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _tokenProvider = Substitute.For<ITokenProvider>();
        _uow = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenPasswordIsIncorrect()
    {
        // Arrange
        var email = "fmoreno@gmail.com";
        var badPassword = "WrongPass";
        var storedHash = "Hashed_Correct_Password";
        var query = new LoginQuery(email, badPassword);
        Usuario usuario = Usuario.Reconstruir(Guid.NewGuid(), "Pedro", "Moreno Marzal", DateOnly.Parse("1998-02-12"), email, storedHash);
        _usuarioRepository.GetByEmailAsync(email).Returns(usuario);
        _passwordHasher.Verify(badPassword, storedHash).Returns(false);
        var handler = new LoginQueryHandler(_usuarioRepository, _passwordHasher, _tokenProvider, _uow);
        // Act
        Result<string> result = await handler.Handle(query, default);
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UsuarioErrors.ContrasenaIncorrecta);
        _tokenProvider.DidNotReceiveWithAnyArgs().Crear(default!);
        await _uow.Received(0).CommitAsync();
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenEmailAndPasswordCorrect()
    {
        // arrange
        string email = "fmorenomarzal@gmail.com";
        string contrasenaCorrecta = "Correct_password";
        string storedHash = "Hash_Correcto";
        string expectedToken = "jwt-token-de-prueba";
        var query = new LoginQuery(email, contrasenaCorrecta);
        Usuario usuario = Usuario.Reconstruir(Guid.NewGuid(), "Pedro", "Moreno Marzal", DateOnly.Parse("1998-02-12"), email, storedHash);
        _usuarioRepository.GetByEmailAsync(email).Returns(usuario);
        _passwordHasher.Verify(contrasenaCorrecta, storedHash).Returns(true);
        _tokenProvider.Crear(usuario).Returns(expectedToken);
        var handler = new LoginQueryHandler(_usuarioRepository, _passwordHasher, _tokenProvider, _uow);
        // act
        Result<string> result = await handler.Handle(query, default);
        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedToken);
        await _usuarioRepository.Received(1).GetByEmailAsync(email);
        _passwordHasher.Received(1).Verify(contrasenaCorrecta, storedHash);
        _tokenProvider.Received(1).Crear(Arg.Is<Usuario>(u => u.Id == usuario.Id));
        // await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        //arrange
        var email = "fmoreno@gmail.com";
        var password = "una_contrasena";
        var query = new LoginQuery(email, password);
        _usuarioRepository.GetByEmailAsync(email).Returns(Task.FromResult<Usuario?>(null));
        var handler = new LoginQueryHandler(_usuarioRepository, _passwordHasher, _tokenProvider, _uow);
        //act
        Result<string> result = await handler.Handle(query, default);
        //assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UsuarioErrors.EmailInexistente);
        _passwordHasher.DidNotReceiveWithAnyArgs().Verify(default!, default!);
        _tokenProvider.DidNotReceiveWithAnyArgs().Crear(default!);
        await _uow.Received(0).CommitAsync();

        await _usuarioRepository.Received(1).GetByEmailAsync(email);
    }
}