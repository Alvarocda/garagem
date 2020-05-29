using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class TipoVeiculo
    {
        [Key]
        public int CodTipoVeiculo {get;set;}
        public string Nome {get;set;}
    }
}