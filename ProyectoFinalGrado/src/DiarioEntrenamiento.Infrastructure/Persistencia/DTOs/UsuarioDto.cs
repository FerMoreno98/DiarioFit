namespace DiarioEntrenamiento.Infrastructure.Persistencia.DTOs;

    internal class UsuarioDTO
    {
        public Guid Uid { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento{get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
    }