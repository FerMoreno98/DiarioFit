using System.Text;
using DiarioEntrenamiento.Application.Abstractions.Security;
using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DiarioEntrenamiento.Infrastructure.Security;
// https://www.youtube.com/watch?v=6DWJIyipxzw
internal sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{

    public string Crear(Usuario usuario)
    {
        string secretKey = configuration["Jwt:Secrets"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub,usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,usuario.Email.Value),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };
        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);
        return token;
    }
}