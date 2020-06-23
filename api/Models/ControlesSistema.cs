using System;
using Newtonsoft.Json;

namespace api.Models
{
    public class ControlesSistema
    {
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