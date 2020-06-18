using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class TipoVeiculo : ControlesSistema
    {
        [Key]
        public int Id {get;set;}
        public string Nome {get;set;}
    }
}