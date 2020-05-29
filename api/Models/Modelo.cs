using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("modelos")]
    public class Modelo
    {
        [Key]
        public int CodModelo {get;set;}
        public int CodFabricante {get;set;}
        public Fabricante Fabricante {get;set;}
        public string Nome {get;set;}
    }
}