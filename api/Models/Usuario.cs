using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int codusuario {get;set;}
        [Required(ErrorMessage = "Por favor, informe um email para o usuário")]        
        public string email {get;set;}
        [NotMapped]
        [Required(ErrorMessage = "Por favor, informe uma senha para o usuário")]
        public string senhaString {get;set;}        
        public byte[] senha {get;set;}
        public byte[] chave {get;set;}
        [Required(ErrorMessage = "Por favor, informe um nome para o usuário")]
        public string nome {get;set;}
    }
}