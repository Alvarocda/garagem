using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("fabricantes")]
    public class Fabricante : ControlesSistema
    {
        [Key]
        public int Id {get;set;}
        public string Nome {get;set;}
    }
}