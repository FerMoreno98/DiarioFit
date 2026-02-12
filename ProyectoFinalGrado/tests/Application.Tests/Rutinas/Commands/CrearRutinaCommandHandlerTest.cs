using DiarioEntrenamiento.Application.Rutinas.CrearRutina;
using DiarioEntrenamiento.Domain.Rutinas;
using FluentAssertions;
using NSubstitute;

namespace Application.Tests.Rutinas.Commands;


public class CrearRutinaCommandHandlerTest
{
    private readonly IRutinaRepository _rutinaRepository;

    public CrearRutinaCommandHandlerTest()
    {
        _rutinaRepository = Substitute.For<IRutinaRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnGuidRutina_WhenHappyPath()
    {
        // Arrange
        string nombre = "nombre";
        DateOnly fechaInicio = DateOnly.Parse("2025-01-09");
        DateOnly fechaFin = DateOnly.Parse("2025-02-12");
        var command = new CrearRutinaCommand(Guid.NewGuid(), nombre, fechaInicio, fechaFin);
        var handler = new CrearRutinaCommandHandler(_rutinaRepository);
        // Act
        var resultado = await handler.Handle(command, default);
        // Assert
        resultado.IsSuccess.Should().BeTrue();
    }
}
