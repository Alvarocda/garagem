using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("fabricantes")]
    public class Fabricante
    {
        [Key]
        public int codfabricante {get;set;}
        public string nome {get;set;}
    }
}