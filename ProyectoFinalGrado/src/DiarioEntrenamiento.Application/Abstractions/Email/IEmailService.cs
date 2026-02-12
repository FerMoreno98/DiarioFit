
namespace DiarioEntrenamiento.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(string email, string asunto, string cuerpo);
}