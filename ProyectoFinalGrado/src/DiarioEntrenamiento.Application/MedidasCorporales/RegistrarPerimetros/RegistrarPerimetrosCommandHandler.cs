using System;
using DiarioEntrenamiento.Application.Abstractions.Messaging;
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros;
using DiarioEntrenamiento.Domain.RegistrosCorporales.Perimetros.Repository;
using MediatR;

namespace DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPerimetros;

internal sealed class RegistrarPerimetrosCommandHandler : ICommandHandler<RegistrarPerimetrosCommand, Unit>
{
    private readonly IPerimetrosRepository _perimetroRepository;

    public RegistrarPerimetrosCommandHandler(IPerimetrosRepository perimetroRepository)
    {
        _perimetroRepository = perimetroRepository;
    }

    public async Task<Result<Unit>> Handle(RegistrarPerimetrosCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Perimetro perimetro = Perimetro.Crear(
            request.UidUsuario,
            request.Cuello,
            request.BrazoDchoRelajado,
            request.BrazoDchoTension,
            request.BrazoIzqRelajado,
            request.BrazoIzqTension,
            request.Pecho,
            request.Hombro,
            request.Cintura,
            request.Cadera,
            request.Abdomen,
            request.MusloDcho,
            request.MusloIzq,
            request.PantorrillaDcha,
            request.PantorrillaIzq
        );
            await _perimetroRepository.Add(perimetro);
            return Result.Success(Unit.Value);

        }catch(Exception e)
        {
             Console.Write(e.Message);
             return Result.Failure<Unit>(new Error("Error.Inesperado",e.Message));
        }
    }
}
