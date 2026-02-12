using System.Net;
using System.Net.Mail;
using DiarioEntrenamiento.Application.Abstractions.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DiarioEntrenamiento.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
//https://www.youtube.com/watch?v=xg90FK3MwKU
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration= configuration;
    }
    public async Task SendAsync(string email, string asunto, string cuerpo)
    {
        var emailEmisor = _configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
        var password = _configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PASSWORD");
        var Host = _configuration.GetValue<string>("CONFIGURACIONES_EMAIL:HOST");
        var Puerto = _configuration.GetValue<int>("CONFIGURACIONES_EMAIL:PUERTO");
        var smtpCliente = new SmtpClient(Host, Puerto);
        smtpCliente.EnableSsl = true;
        smtpCliente.UseDefaultCredentials = false;
        smtpCliente.Credentials = new NetworkCredential(emailEmisor, password);
        var mensaje = new MailMessage(emailEmisor!, email, asunto, cuerpo);
        await smtpCliente.SendMailAsync(mensaje);
    }
}