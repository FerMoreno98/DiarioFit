using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using DiarioEntrenamiento.Domain.Usuarios.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiarioEntrenamiento.Infrastructure.Configurations;

internal sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(usuario => usuario.Id);
        builder.Property(usuario => usuario.Nombre)
        .HasMaxLength(200)
        .HasConversion(nombre => nombre.Value, value => new Nombre(value));
        builder.Property(usuario => usuario.Apellidos)
        .HasMaxLength(300)
        .HasConversion(apellidos => apellidos.Value, value => new Apellidos(value));
        builder.Property(usuario => usuario.FechaNacimiento)
        .HasConversion(fechaNacimiento => fechaNacimiento.Value, value => new FechaNacimiento(value));
        builder.Property(usuario => usuario.Email)
        .HasConversion(email => email.Value, value => Domain.Usuarios.ValueObjects.Email.Crear(value).Value);
        builder.Property(usuario=>usuario.Contrasena)
        .HasConversion(contrasena=>contrasena.Value, value => ContrasenaHash.Crear(value).Value); 
    }
}