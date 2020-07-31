using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("usuarios")]
    public class Usuario : EntityBase
    {
        [Required(ErrorMessage = "Por favor, informe um email para o usuário")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [JsonIgnore]
        public byte[] Senha { get; set; }
        [Required]
        [JsonIgnore]
        public byte[] Chave { get; set; }
        [Required(ErrorMessage = "Por favor, informe um nome para o usuário")]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "usuario";
    }
}