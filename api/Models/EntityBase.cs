using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api.Models
{
    public class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonIgnore]
        public int CriadoPor { get; set; }
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        [JsonIgnore]
        public int? AtualizadoPor { get; set; }
        [JsonIgnore]
        public DateTime? AtualizadoEm { get; set; }
        [JsonIgnore]
        public int? DesativadoPor { get; set; }
        [JsonIgnore]
        public DateTime? DesativadoEm { get; set; }
        [DefaultValue(true)]
        public bool Ativo { get; set; } = true;
    }
}