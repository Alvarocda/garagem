using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("modelos")]
    public class Modelo : ControlesSistema
    {
        [Key]
        public int Id {get;set;}
        public int FabricanteId {get;set;}
        public Fabricante Fabricante {get;set;}
        public string Nome {get;set;}
    }
}