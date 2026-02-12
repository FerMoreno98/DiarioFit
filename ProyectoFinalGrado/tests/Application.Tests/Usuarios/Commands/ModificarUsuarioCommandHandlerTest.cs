using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Application.Usuarios.ModificarUsuario;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;

namespace Application.Tests.Usuarios.Commands;

public class ModificarUsuarioCommandHandlerTest
{
    private readonly IUnitOfWork _uowMock;
    private readonly IUsuarioRepository _usuarioRepositoryMock;

    public ModificarUsuarioCommandHandlerTest()
    {
        _uowMock = Substitute.For<IUnitOfWork>();
        _usuarioRepositoryMock = Substitute.For<IUsuarioRepository>();
    }
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenHappyPath()
    {
        // arrange
        string nombre = "nombre";
        string apellidos = "apellido1 apellido2";
        DateOnly FechaNacimiento = DateOnly.Parse("1998-03-12");
        var command = new ModificarUsuarioCommand(Guid.NewGuid(), nombre, apellidos, FechaNacimiento);
        var handler = new ModificarUsuarioCommandHandler(_uowMock,_usuarioRepositoryMock);
        // act
        Result<Unit> resultado = await handler.Handle(command, default);
        // assert
        resultado.IsSuccess.Should().BeTrue();
    }
}