using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("fabricantes")]
    public class Fabricante : EntityBase
    {
        [MaxLength(50)]
        [Required]
        public string Nome { get; set; }
        [ForeignKey("FabricanteId")]
        public ObservableCollection<Modelo> Modelos { get; set; }
    }
}