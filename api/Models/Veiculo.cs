using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Veiculo
    {
        [Key]
        public int CodVeiculo {get;set;}
        public int CodModelo {get;set;}
        public Modelo Modelo {get;set;}
        public string Ano {get;set;}
        public int KM {get;set;}
        public decimal Valor {get;set;}
        public string Observacao {get;set;}
        public string Cor {get;set;}
        public int CodTipoVeiculo {get;set;}
        public TipoVeiculo TipoVeiculo {get;set;}
    }
}