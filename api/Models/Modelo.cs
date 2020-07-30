using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("modelos")]
    public class Modelo : EntityBase
    {
        public string Nome {get;set;}
        public int FabricanteId {get;set;}
        public Fabricante Fabricante {get;set;}
        
    }
}