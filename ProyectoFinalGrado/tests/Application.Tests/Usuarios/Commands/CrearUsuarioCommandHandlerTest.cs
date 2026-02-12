using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Application.Abstractions.Security;
using DiarioEntrenamiento.Application.Usuarios.CrearUsuario;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using DiarioEntrenamiento.Domain.Usuarios.Errors;
using FluentAssertions;
using NSubstitute;

namespace Application.Tests.Usuarios.Commands;
// https://www.youtube.com/watch?v=a6Qab5l-VLo&t=487s en vez de moq uso NSubstitute
public class CrearUsuarioCommandHandlerTests
{
    private readonly IUsuarioRepository _userRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IPasswordHasher _passwordHasherMock;
    private readonly IDomainEventDispatcher _eventDispatcherMock;

    public CrearUsuarioCommandHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IUsuarioRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _passwordHasherMock = Substitute.For<IPasswordHasher>();
        _eventDispatcherMock = Substitute.For<IDomainEventDispatcher>();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
    {
        //arrange
        var command = new CrearUsuarioCommand(Guid.NewGuid(), "Pablo", "Moreno Barja", DateOnly.Parse("1999-03-12"), "fmorenomarzal@gmail.com", "Kaisser221");
        _userRepositoryMock.esEmailUnico("fmorenomarzal@gmail.com")
        .Returns(Task.FromResult(false));
        var handler = new CrearUsuarioCommandHandler(_userRepositoryMock, _unitOfWorkMock, _passwordHasherMock, _eventDispatcherMock);
        //Act
        Result<Guid> result = await handler.Handle(command, default);
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmailErrors.EmailEnUso);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenEmailIsUnique()
    {
        //arrange
        var command = new CrearUsuarioCommand(Guid.NewGuid(), "Pablo", "Moreno Barja", DateOnly.Parse("1999-03-12"), "fmorenomarzal@gmail.com", "Kaisser221");
        _userRepositoryMock.esEmailUnico("fmorenomarzal@gmail.com")
        .Returns(Task.FromResult(true));
        var handler = new CrearUsuarioCommandHandler(_userRepositoryMock, _unitOfWorkMock, _passwordHasherMock, _eventDispatcherMock);
        //Act
        Result<Guid> result = await handler.Handle(command, default);
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
    [Fact]
    public async Task Handle_Should_CallAddAsyncOnRepository_WhenEmailIsUnique()
    {
        //arrange
        var command = new CrearUsuarioCommand(Guid.NewGuid(), "Pablo", "Moreno Barja", DateOnly.Parse("1999-03-12"), "fmorenomarzal@gmail.com", "Kaisser221");
        _userRepositoryMock.esEmailUnico("fmorenomarzal@gmail.com")
        .Returns(Task.FromResult(true));
        var handler = new CrearUsuarioCommandHandler(_userRepositoryMock, _unitOfWorkMock, _passwordHasherMock, _eventDispatcherMock);
        //Act
        Result<Guid> result = await handler.Handle(command, default);
        // Assert
        await _userRepositoryMock.Received(1).AddAsync(Arg.Is<Usuario>(u =>
        u.Nombre.Value == "Pablo" &&
        u.Apellidos.Value == "Moreno Barja" &&
        u.Email.Value == "fmorenomarzal@gmail.com"));
    }
    [Fact]
    public async Task Handle_Should_NotCallCommitAsyncFromUOW_WhenEmailIsNotUnique()
    {
        //arrange
        var command = new CrearUsuarioCommand(Guid.NewGuid(), "Pablo", "Moreno Barja", DateOnly.Parse("1999-03-12"), "fmorenomarzal@gmail.com", "Kaisser221");
        _userRepositoryMock.esEmailUnico("fmorenomarzal@gmail.com")
        .Returns(Task.FromResult(false));
        var handler = new CrearUsuarioCommandHandler(_userRepositoryMock, _unitOfWorkMock, _passwordHasherMock, _eventDispatcherMock);
        //Act
        await handler.Handle(command, default);
        // Assert
        await _unitOfWorkMock.Received(0).CommitAsync();
    }
    [Fact]
    public async Task Handle_Should_ResturnFailureResult_WhenPasswordIsInvalid()
    {
        // arrange
        string contrasenaInvalida="12345";
        var command = new CrearUsuarioCommand(Guid.NewGuid(), "Pablo", "Moreno Barja", DateOnly.Parse("1999-03-12"), "fmorenomarzal@gmail.com", contrasenaInvalida);
        _userRepositoryMock.esEmailUnico("fmorenomarzal@gmail.com").Returns(Task.FromResult(true));
        var handler = new CrearUsuarioCommandHandler(_userRepositoryMock, _unitOfWorkMock, _passwordHasherMock, _eventDispatcherMock);
         // Act
        Result<Guid> result=await handler.Handle(command, default);
        // Assert
        result.IsFailure.Should().BeTrue();
    }
    [Fact]
    public async Task Handle_Should_ResturnFailureResult_WhenEmailIsInvalid()
    {
        // arrange
        string EmailInvalido="emailInvalido";
        var command = new CrearUsuarioCommand(Guid.NewGuid(), "Pablo", "Moreno Barja", DateOnly.Parse("1999-03-12"), EmailInvalido, "contrasenaValida");
        _userRepositoryMock.esEmailUnico("fmorenomarzal@gmail.com").Returns(Task.FromResult(true));
        var handler = new CrearUsuarioCommandHandler(_userRepositoryMock, _unitOfWorkMock, _passwordHasherMock, _eventDispatcherMock);
         // Act
        Result<Guid> result=await handler.Handle(command, default);
        // Assert
        result.IsFailure.Should().BeTrue();
    }
    [Fact]
    public async Task Handle_Should_ResturnFailureResult_WhenVariosCamposInvalid()
    {
        // arrange
        string EmailInvalido="emailInvalido";
        string contrasenaInvalida="12345";
        var command = new CrearUsuarioCommand(Guid.NewGuid(), "Pablo", "Moreno Barja", DateOnly.Parse("1999-03-12"), EmailInvalido, contrasenaInvalida);
        _userRepositoryMock.esEmailUnico("fmorenomarzal@gmail.com").Returns(Task.FromResult(true));
        var handler = new CrearUsuarioCommandHandler(_userRepositoryMock, _unitOfWorkMock, _passwordHasherMock, _eventDispatcherMock);
         // Act
        Result<Guid> result=await handler.Handle(command, default);
        // Assert
        result.IsFailure.Should().BeTrue();
    }
    
    
}

