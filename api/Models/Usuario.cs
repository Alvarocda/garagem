using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        public int CodUsuario {get;set;}
        [Required(ErrorMessage = "Por favor, informe um email para o usuário")]
        public string Email {get;set;}
        [NotMapped]
        [Required(ErrorMessage = "Por favor, informe uma senha para o usuário")]
        public string SenhaString {get;set;}
        public byte[] Senha {get;set;}
        public byte[] Chave {get;set;}
        [Required(ErrorMessage = "Por favor, informe um nome para o usuário")]
        public string Nome {get;set;}
        public string Ativo {get;set;} = "S";
    }
}