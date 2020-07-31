using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("veiculos")]
    public class Veiculo : EntityBase
    {
        [Required]
        public int ModeloId { get; set; }
        public Modelo Modelo { get; set; }
        [Required]
        [MaxLength(9)]
        public string Ano { get; set; }
        [Required]
        public int KM { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [MaxLength(500)]
        public string Observacao { get; set; }
        [Required]
        [MaxLength(30)]
        public string Cor { get; set; }
        [Required]
        public int TipoVeiculoId { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
        // Status
        // D = Disponivel para venda
        // V = Vendido
        // R = Removido
        [Required]
        [MaxLength(1)]
        public string Status { get; set; } = "D";
        public Fabricante Fabricante { get; set; }
    }
}