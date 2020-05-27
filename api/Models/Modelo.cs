using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("modelos")]
    public class Modelo
    {
        [Key]
        public int codmodelo {get;set;}
        public int codfabricante {get;set;}
        public Fabricante Fabricante {get;set;}
        public string nome {get;set;}
    }
}