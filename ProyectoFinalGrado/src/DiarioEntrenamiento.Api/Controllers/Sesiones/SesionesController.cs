
using DiarioEntrenamiento.Application.Exceptions;
using DiarioEntrenamiento.Application.Sesiones.IniciarSesionEntrenamiento;
using DiarioEntrenamiento.Application.Sesiones.Obtener1RMPorSesion;
using DiarioEntrenamiento.Application.Sesiones.ObtenerDatosUltimaSesion;
using DiarioEntrenamiento.Application.Sesiones.RegistrarSerie;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiarioEntrenamiento.Api.Controllers.Sesiones;

[ApiController]
[Route("/api/sesion")]
public class SesionesController : ControllerBase
{
    private ISender sender;

    public SesionesController(ISender sender)
    {
        this.sender = sender;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> IniciarSesionEntrenamiento(DatosInicioSesionRequest req,CancellationToken cancellationToken)
    {
        try
        {
            var command=new IniciarSesionCommand(req.UidUsuario,req.UidDia,req.Sueno,req.Motivacion,req.ERP);
            var resultado=await sender.Send(command,cancellationToken);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Value);
            }
            return BadRequest(resultado.Error);
        }catch(Exception e)
        {
            return StatusCode(500, new { message = e.Message });
        }
    }
    [Authorize]
    [HttpGet("datosultimasesion")]
    public async Task<IActionResult> ObtenerDatosUltimaSesion(Guid UidDia)
    {
        try{
        var query=new ObtenerDatosUltimaSesionQuery(UidDia);
        var resultado=await sender.Send(query);
        if (resultado.IsSuccess)
        {
            return Ok(resultado.Value);
        }
        return BadRequest(resultado.Error);
        }catch(Exception e)
        {
            return StatusCode(500, new {  message = e.Message });
        }
    }
    [Authorize]
    [HttpPost("registrarserie")]
    public async Task<IActionResult> RegistrarSerie(DatosRegistroSerieRequest req,CancellationToken cancellationToken)
    {
        try
        {
            var command=new RegistrarSerieCommand(req.UidDia,req.Ejercicio,req.Peso,req.Repeticiones,req.Rir,req.Serie);
            var resultado=await sender.Send(command,cancellationToken);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Value);
            }
            return BadRequest(resultado.Error);

        }catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [Authorize]
    [HttpGet("obtener1rmporejercicio")]
    public async Task<IActionResult> ObtenerRMPorEjercicio(Guid UidUsuario)
    {
        try
        {
            var query = new Obtener1RMPorSesionQuery(UidUsuario);
            var resultado=await sender.Send(query);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Value);
            }
            return BadRequest(resultado.Error);
        }catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
