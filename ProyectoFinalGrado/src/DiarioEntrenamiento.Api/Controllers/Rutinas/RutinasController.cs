
using System.Runtime.CompilerServices;
using System.Security.Claims;
using DiarioEntrenamiento.Application.Ejercicios.ObtenerEjerciciosSubgrupo;
using DiarioEntrenamiento.Application.Exceptions;
using DiarioEntrenamiento.Application.Rutinas.AgregarDiaRutina;
using DiarioEntrenamiento.Application.Rutinas.CompletarDatosEjercicioDiaRutina;
using DiarioEntrenamiento.Application.Rutinas.CrearRutina;
using DiarioEntrenamiento.Application.Rutinas.DuplicarMesociclo;
using DiarioEntrenamiento.Application.Rutinas.EliminarEjercicioDiaRutina;
using DiarioEntrenamiento.Application.Rutinas.ELiminarRutina;
using DiarioEntrenamiento.Application.Rutinas.ModificarDatosEjercicio;
using DiarioEntrenamiento.Application.Rutinas.ModificarRutina;
using DiarioEntrenamiento.Application.Rutinas.ObtenerDatosEditarMesociclo;
using DiarioEntrenamiento.Application.Rutinas.ObtenerDatosHomePage;
using DiarioEntrenamiento.Application.Rutinas.ObtenerDias;
using DiarioEntrenamiento.Application.Rutinas.ObtenerEjerciciosDia;
using DiarioEntrenamiento.Application.Rutinas.ObtenerGruposMusculares;
using DiarioEntrenamiento.Application.Rutinas.ObtenerNumeroDeSeriesPorGrupoMuscular;
using DiarioEntrenamiento.Application.Rutinas.ObtenerSubGruposMusculares;
using DiarioEntrenamiento.Application.Rutinas.ObtenerUidRutina;
using DiarioEntrenamiento.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DiarioEntrenamiento.Api.Controllers.Rutinas;
[ApiController]
[Route("/api/rutinas")]
public class RutinasController : ControllerBase
{
    private ISender sender;

    public RutinasController(ISender sender)
    {
        this.sender = sender;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CrearRutina(RutinasCrearRequest req,CancellationToken cancellationToken)
    {
        try{
        var command = new CrearRutinaCommand(
            req.Uid,
            req.nombre,
            req.FechaInicio,
            req.FechaFin
        );
        var resultado = await sender.Send(command, cancellationToken);
        if (resultado.IsSuccess)
        {
            return Ok(resultado.Value);
        }
        return BadRequest(resultado.Error);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    [Authorize]
    [HttpPost("creardia")]
    public async Task<IActionResult> CrearDiaRutina(DiaRutinaRequest req,CancellationToken cancellationToken)
    {
        try
        {
            // var uidSTR = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Guid uid = Guid.Parse(uidSTR);
            var command = new AgregarDiaRutinaCommand(req.uidRutina,req.Nombre,req.DiaDeLaSemana);
            var resultado = await sender.Send(command, cancellationToken);
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
    [HttpGet("obtenerdias")]
    public async Task<IActionResult> ObtenerDiasRutina(Guid uid,CancellationToken cancellationToken)
    {
        try{
        var query = new ObtenerDiasQuery(uid);
        var resultado = await sender.Send(query, cancellationToken);
        return Ok(resultado.Value);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
        
    }
    [Authorize]
    [HttpGet("obtenerejerciciosdia")]
    public async Task<IActionResult> ObtenerEjerciciosDiaRutina(Guid uidDiaRutina)
    {
        try
        {
            var query=new ObtenerEjerciciosDiaQuery(uidDiaRutina);
            var resultado=await sender.Send(query);
            return Ok(resultado.Value);

        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpGet("obtenergruposmusculares")]
    public async Task<IActionResult> ObtenerGruposMusculares()
    {
        try
        {
            var query=new ObtenerGruposMuscularesQuery();
            var resultado=await sender.Send(query);
            return Ok(resultado.Value);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpGet("subgruposmusculares")]
    public async Task<IActionResult> ObtenerSubGruposMusculares(int idGrupo)
    {
        try{
        var query=new ObtenerSubGruposMuscularesQuery(idGrupo);
        var resultado=await sender.Send(query);
        return Ok(resultado.Value);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpGet("ejerciciossubgrupo")]
    public async Task<IActionResult> ObtenerEjerciciosSubGrupo(int idSubgrupo)
    {
        try{
        var query=new ObtenerEjerciciosSubgrupoQuery(idSubgrupo);
        var resultado=await sender.Send(query);
        return Ok(resultado.Value);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpPost("completardatosejerciciodia")]
    public async Task<IActionResult> CompletarDatosDia(DatosDiaRequest datosDia)
    {
        try
        {
            var command=new CompletarDatosEjercicioDiaRutinaCommand
            (
                datosDia.UidDiaRutina,
                datosDia.UidEjercicio,
                datosDia.orden,
                datosDia.Series,
                datosDia.RangoReps,
                datosDia.RangoRIR,
                datosDia.TiempoDeDescanso
            );
            var resultado=await sender.Send(command);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Value);
            }
            return BadRequest(resultado.Error);
        }catch(ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    [Authorize]
    [HttpGet("obteneruidrutina")]
    public async Task<IActionResult> ObtenerUidRutinaPorUidDia(Guid UidDia)
    {
        try{
        var query=new ObtenerUidRutinaQuery(UidDia);
        var resultado=await sender.Send(query);
        return Ok(resultado.Value);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpGet("obtenerdatoshomepage")]
    public async Task<IActionResult> ObtenerDatosHomePage(Guid UidUsuario)
    {
        try
        {
            var query=new ObtenerDatosHomePageQuery(UidUsuario);
            var resultado=await sender.Send(query);
            // var json = System.Text.Json.JsonSerializer.Serialize(resultado.Value);
            // Console.WriteLine(json);

            return Ok(resultado.Value);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpPut("modificardatosejerciciodiarutina")]
    public async Task<IActionResult> ModificarDatosEjercicioDiaRutina(DatosModificarEjercicioDiaRutinaRequest datos)
    {
        try
        {
            var command=new ModificarDatosEjercicioCommand
            (
                datos.UidEjercicioDiaRutina,
                datos.UidDiaRutina,
                datos.UidEjercicio,
                datos.orden,
                datos.Series,
                datos.RangoReps,
                datos.RangoRIR,
                datos.TiempoDeDescanso
            );
            var resultado=await sender.Send(command);
            if(resultado.IsSuccess){
            return Ok(resultado);
            }
            return BadRequest(resultado.Error);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpDelete("eliminarejerciciodiarutina")]
    public async Task<IActionResult> EliminarEjercicioDiaRutina(Guid UidEjercicioDiaRutina)
    {
        try
        {
            var command=new EliminarEjercicioCommand(UidEjercicioDiaRutina);
            var resultado=await sender.Send(command);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Value);
            }
            return BadRequest(resultado.Error);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpGet("obtenerdatoseditarmesocilo")]
    public async Task<IActionResult> ObtenerDatosEditarMesociclo(Guid UidUsuario)
    {
        try
        {
            var query=new ObtenerDatosEditarMesocicloQuery(UidUsuario);
            var result=await sender.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    [Authorize]
    [HttpDelete("eliminarrutina")]
    public async Task<IActionResult> EliminarRutina(Guid UidRutina,CancellationToken cancellationToken)
    {
        try
        {
            var command=new EliminarRutinaCommand(UidRutina);
            var resultado=await sender.Send(command,cancellationToken);
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
    [Authorize]
    [HttpPost("duplicarrutina")]
    public async Task<IActionResult> DuplicarRutina(DuplicarRutinaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command=new DuplicarMesocicloCommand
            (
            request.UidUsuario,
            request.UidRutina,
            request.nombre,
            request.FechaInicio,
            request.FechaFin
            );
            var resultado=await sender.Send(command,cancellationToken);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Value);
            }
            return BadRequest(resultado.Error);
            
        }  catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    [Authorize]
    [HttpPut("modificarrutina")]
    public async Task<IActionResult> ModificarRutina(ModificarRutinaRequest request,CancellationToken cancellationToken)
    {
        try
        {
            var command=new ModificarRutinaCommand(
                request.UidUsuario,
                request.UidRutina,
                request.Nombre,
                request.FechaInicio,
                request.FechaFin
                );
            var resultado=await sender.Send(command,cancellationToken);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Value);
            }
            return BadRequest(resultado.Error);
            
        }catch(ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [Authorize]
    [HttpGet("obtenerdatosgraficagruposmusculares")]
    public async Task<IActionResult> ObtenerDatosGraficaGruposMusculares(Guid UidUsuario)
    {
        try
        {
            var query=new ObtenerNumeroDeSeriesPorGrupoMuscularQuery(UidUsuario);
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