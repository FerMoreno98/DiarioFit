using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Usuarios.Errors;

public static class UsuarioErrors
{
    public static readonly Error EmailInexistente = new Error("Email.Inexistente", "El email que has ingresado no existe"); 
    public static readonly Error ContrasenaIncorrecta=new Error("Contrasena.Incorrecta","Contraseña incorrecta"); 
}