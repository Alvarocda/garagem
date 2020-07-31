using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("modelos")]
    public class Modelo : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [Required]
        public int FabricanteId { get; set; }
        public Fabricante Fabricante { get; set; }

    }
}