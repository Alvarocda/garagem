using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("fabricantes")]
    public class Fabricante
    {
        [Key]
        public int CodFabricante {get;set;}
        public string Nome {get;set;}
    }
}