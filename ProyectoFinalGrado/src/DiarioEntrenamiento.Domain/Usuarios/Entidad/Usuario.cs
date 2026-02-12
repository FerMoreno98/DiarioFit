
using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Usuarios.Events;
using DiarioEntrenamiento.Domain.Usuarios.ValueObjects;

namespace DiarioEntrenamiento.Domain.Usuarios.Entidad;

public sealed class Usuario : Entity<Guid>
{
    private Usuario
    (
        Guid uid,
        Nombre nombre,
        Apellidos apellidos,
        FechaNacimiento fechaNacimiento,
        Email email,
        ContrasenaHash contrasena
        ) : base(uid)
    {
        Nombre = nombre;
        Apellidos = apellidos;
        FechaNacimiento = fechaNacimiento;
        Email = email;
        Contrasena = contrasena;
    }


    public Nombre Nombre { get; private set; }
    public Apellidos Apellidos { get; private set; }
    public FechaNacimiento FechaNacimiento { get; private set; }
    public Email Email { get; private set; }
    public ContrasenaHash Contrasena { get; private set; }

    public static Usuario Crear
    (
    Nombre nombre,
    Apellidos apellidos,
    FechaNacimiento fechaNacimiento,
    Email email,
    ContrasenaHash contrasena
    )
    {
        Usuario usuario = new Usuario(Guid.NewGuid(), nombre, apellidos, fechaNacimiento, email, contrasena);
        usuario.RaiseDomainEvents(new UsuarioCreadoDomainEvent(usuario.Email.Value));
        return usuario;
    }

    public static Usuario Reconstruir(Guid uid,string nombre, string apellidos, DateOnly FechaNacimiento,string email,string hash)
    {
        Nombre nom = new Nombre(nombre);
        Apellidos ape = new Apellidos(apellidos);
        FechaNacimiento fecha = new FechaNacimiento(FechaNacimiento);
        Email ema = Email.Crear(email).Value;
        ContrasenaHash contrasenaHash = ContrasenaHash.Crear(hash).Value;

        Usuario usuario = new Usuario(uid, nom, ape, fecha, ema, contrasenaHash);
    
        return usuario;
    }
    
    
}