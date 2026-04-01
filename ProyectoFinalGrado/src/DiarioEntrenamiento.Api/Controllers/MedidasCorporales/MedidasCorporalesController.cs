
using DiarioEntrenamiento.Api.Controllers.MedidasCorporales.Requests;
using DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPerimetros;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DiarioEntrenamiento.Application.Exceptions;
using DiarioEntrenamiento.Application.MedidasCorporales.RegistrarPliegues;
using DiarioEntrenamiento.Application.MedidasCorporales.ObtenerPliegues;

namespace DiarioEntrenamiento.Api.Controllers.MedidasCorporales
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidasCorporalesController : ControllerBase
    {
        private ISender sender;
        public MedidasCorporalesController(ISender sender)
        {
            this.sender = sender;
        }
        [Authorize]
        [HttpPost("registrarperimetros")]
        public async Task<IActionResult> RegistrarPerimetros(RegistrarPerimetrosRequest request,CancellationToken cancellationToken)
        {
            try{
            var command=new RegistrarPerimetrosCommand(
                request.uidUsuario,
                request.cuello,
                request.brazoDchoRelajado,
                request.brazoDchoTension,
                request.brazoIzqRelajado,
                request.brazoIzqTension,
                request.pecho,
                request.hombro,
                request.cintura,
                request.cadera,
                request.abdomen,
                request.musloDcho,
                request.musloIzq,
                request.pantorrillaDcha,
                request.pantorrillaIzq
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
        [HttpPost("registrarpliegues")]
        public async Task<IActionResult> RegistrarPliegues(RegistrarPlieguesRequest request,CancellationToken cancellationToken)
        {
            try{
            var command = new RegistrarPlieguesCommand(
                request.uidUsuario,
                request.abdominal,
                request.suprailiaco,
                request.tricipital,
                request.subescapular,
                request.muslo,
                request.pantorrilla
            );
            var resultado=await sender.Send(command, cancellationToken);
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
        [HttpGet("getplieguesfromuser")]
        public async Task<IActionResult> GetPlieguesFromUser(Guid UidUsuario, CancellationToken cancellationToken)
        {
            try{
            var query=new ObtenerPlieguesQuery(UidUsuario);
            var resultado=await sender.Send(query,cancellationToken);
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
    }
       
    }
    

