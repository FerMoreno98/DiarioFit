
using System.Security.Claims;
using DiarioEntrenamiento.Application.Exceptions;
using DiarioEntrenamiento.Application.Usuarios.CrearUsuario;
using DiarioEntrenamiento.Application.Usuarios.Login;
using DiarioEntrenamiento.Application.Usuarios.ModificarUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiarioEntrenamiento.Api.Controllers.Usuarios;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly ISender sender;

    public UsuariosController(ISender sender)
    {
        this.sender = sender;
    }
    [HttpPost]
    public async Task<IActionResult> CrearUsuario(UsuariosCrearRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new CrearUsuarioCommand(
                request.Uid,
                request.Nombre,
                request.Apellidos,
                request.FechaNacimiento,
                request.Email,
                request.Contrasena);
            var resultado = await sender.Send(command, cancellationToken);
            if (resultado.IsFailure)
            {
                return BadRequest(resultado.Error);
            }
            return CreatedAtAction(nameof(CrearUsuario), new { id = resultado.Value }, resultado.Value);
        }catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UsuariosLoginRequest request, CancellationToken cancellationToken)
    {
        var query = new LoginQuery(request.Email, request.Contrasena);
        var resultado = await sender.Send(query, cancellationToken);
        if (resultado.IsFailure)
        {
            return Unauthorized(new { message = "Credenciales incorrectas" });
        }
        return new JsonResult(new {token=resultado.Value});
    }
    [Authorize]
    [HttpPost("modificar")]
    public async Task<IActionResult> ModificarUsuario(UsuarioModificarRequest request,CancellationToken cancellationToken)
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid guid = Guid.Parse(uid);
        var command = new ModificarUsuarioCommand(guid, request.Nombre, request.Apellidos,request.FechaNacimiento);
        var resultado = await sender.Send(command, cancellationToken);
        if (resultado.IsSuccess)
        {
            return Ok(resultado.Value);
        }
        return BadRequest(resultado.Error);
    }
}