using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace api.Models
{
    public class EntityBase
    {
        [Key]
        public int Id {get;set;}
        [JsonIgnore]
        public int CriadoPor {get;set;}
        [JsonIgnore]
        public DateTime CriadoEm { get; set; }
        [JsonIgnore]
        public int AtualizadoPor {get;set;}
        [JsonIgnore]
        public DateTime AtualizadoEm { get; set; }
        [JsonIgnore]
        public int DesativadoPor {get;set;}
        [JsonIgnore]
        public DateTime DesativadoEm { get; set; }
        [JsonIgnore]
        public bool Ativo {get;set;} = true;
    }
}