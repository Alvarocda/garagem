using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("veiculos")]
    public class Veiculo : EntityBase
    {
        public int ModeloId {get;set;}
        public Modelo Modelo {get;set;}
        public string Ano {get;set;}
        public int KM {get;set;}
        public decimal Valor {get;set;}
        public string Observacao {get;set;}
        public string Cor {get;set;}
        public int TipoVeiculoId {get;set;}
        public TipoVeiculo TipoVeiculo {get;set;}
        // Status
        // D = Disponivel para venda
        // V = Vendido
        // R = Removido
        public string Status {get;set;} = "D";
        public Fabricante Fabricante {get;set;}
    }
}