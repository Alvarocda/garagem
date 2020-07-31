using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("imagens")]
    public class Imagem : EntityBase
    {
        public string URL { get; set; }
        [Required]
        public int VeiculoId { get; set; }
    }
}