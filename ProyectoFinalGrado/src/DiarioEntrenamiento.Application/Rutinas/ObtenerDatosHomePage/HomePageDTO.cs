using System.Dynamic;
using DiarioEntrenamiento.Domain.Rutinas.DTOs;

namespace DiarioEntrenamiento.Application.Rutinas.ObtenerDatosHomePage;

public record HomePageDTO(
    Guid UidRutina,
    string NombreRutina,
    IEnumerable<DiaRutinaHomeDto> DatosDias
    // public IEnumerable<EjercicioDiaRutinaHomeDto> datosEjerciciosDias{get;set;}
);