using api.Models;

namespace api.DTO
{
    public class UsuarioDTO : EntityBase
    {
        public string Email { get; set; }
        public string SenhaString { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; } = "usuario";
    }
}