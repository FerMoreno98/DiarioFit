using Dapper;
using DiarioEntrenamiento.Application.Abstractions.Data;
using DiarioEntrenamiento.Domain.Usuarios;
using DiarioEntrenamiento.Domain.Usuarios.Entidad;
using DiarioEntrenamiento.Domain.Usuarios.ValueObjects;
using DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;


namespace DiarioEntrenamiento.Infrastructure.Persistencia.Repositories;

internal sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly IUnitOfWork _uow;
    private readonly ISqlConnectionFactory _connectionFactory;

    public UsuarioRepository(IUnitOfWork uow, ISqlConnectionFactory connectionFactory)
    {
        _uow = uow;
        _connectionFactory = connectionFactory;
    }

    public async Task AddAsync(Usuario usuario,CancellationToken cancellationToken)
    {
        string sql = @"
        INSERT INTO ""Usuarios""(""Uid"",""Nombre"",""Apellidos"",""FechaNacimiento"",""Email"",""Contrasena"")
        VALUES(@Uid,@Nombre,@Apellidos,@FechaNacimiento,@Email,@ContrasenaHash)";
        var parametros = new
        {
            Uid = usuario.Id,
            Nombre = usuario.Nombre.Value,
            Apellidos = usuario.Apellidos.Value,
            FechaNacimiento = usuario.FechaNacimiento.Value.ToDateTime(TimeOnly.MinValue),
            Email = usuario.Email.Value,
            ContrasenaHash = usuario.Contrasena.Value
        };
        
        await _uow.Connection.ExecuteAsync(new CommandDefinition(sql,parametros,_uow.Transaction,cancellationToken:cancellationToken));

    }

    public async Task<bool> esEmailUnico(string email, CancellationToken cancellationToken)
    {
        string sql = @"SELECT 1 FROM ""Usuarios"" where ""Email""=@Email";
        var parametros = new
        {
            Email = email,
        };
        var resultado = await _uow.Connection.ExecuteScalarAsync<int?>(new CommandDefinition(sql, parametros, _uow.Transaction, cancellationToken: cancellationToken));
        return resultado == null;
    }

    public async Task<Usuario?> GetByEmailAsync(string Email)
    {
        string sql = @"Select
         ""Uid"", 
         ""Nombre"",
         ""Apellidos"",
         ""FechaNacimiento"",
         ""Email"",
         ""Contrasena"" 
         from ""Usuarios"" where ""Email""=@Email";
        var parametros = new
        {
            Email = Email,
        };
        UsuarioDTO? usuario = null;
        if (_uow.Transaction is not null)
        {
            usuario = await _uow.Connection.QueryFirstOrDefaultAsync<UsuarioDTO>(sql, parametros, _uow.Transaction);
        }
        else
        {
            using var connection = await _connectionFactory.CrearConexion();
            usuario = connection.QueryFirstOrDefault<UsuarioDTO>(sql, parametros);
        }
        
        
        if (usuario is null)
        {
            return null;
        }
        Usuario usuarioEntidad = Usuario.Reconstruir(
            usuario.Uid,
            usuario.Nombre,
            usuario.Apellidos,
            DateOnly.FromDateTime(usuario.FechaNacimiento),
            usuario.Email,
            usuario.Contrasena);
        return usuarioEntidad;

    }


    public async Task ModificarUsuario(Guid uid,Nombre nombre,Apellidos apellidos,FechaNacimiento fechaNacimiento, CancellationToken cancellationToken)
    {
        string sql = @"UPDATE ""Usuarios"" 
        SET ""Nombre""=@Nombre,
        ""Apellidos""=@Apellidos,
        ""FechaNacimiento""=@FechaNacimiento
        WHERE ""Uid""=@Uid";
        var parametros = new
        {
            Uid = uid,
            Nombre = nombre.Value,
            Apellidos = apellidos.Value,
            FechaNacimiento = fechaNacimiento.Value.ToDateTime(TimeOnly.MinValue)
        };
        if (_uow.Connection is not null)
        {
            await _uow.Connection.ExecuteAsync(new CommandDefinition(sql, parametros, _uow.Transaction, cancellationToken: cancellationToken));
        }
        else
        {
            using var connection = await _connectionFactory.CrearConexion(cancellationToken);
            await connection.ExecuteAsync(sql, parametros);
        }
    }
}