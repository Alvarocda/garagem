using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("tipos_veiculo")]
    public class TipoVeiculo : EntityBase
    {
        [MaxLength(50)]
        public string Nome { get; set; }
    }
}