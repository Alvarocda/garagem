using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("usuarios")]
    public class Usuario : EntityBase
    {
        [Required(ErrorMessage = "Por favor, informe um email para o usuário")]
        public string Email {get;set;}
        [JsonIgnore]
        public byte[] Senha {get;set;}
        [JsonIgnore]
        public byte[] Chave {get;set;}
        [Required(ErrorMessage = "Por favor, informe um nome para o usuário")]
        public string Nome {get;set;}
        public string Role {get;set;} = "usuario";
    }
}