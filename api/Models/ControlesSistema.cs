using System;

namespace api.Models
{
    public class ControlesSistema
    {
        public int CriadoPor {get;set;}
        public DateTime CriadoEm { get; set; }
        public int AtualizadoPor {get;set;}
        public DateTime AtualizadoEm { get; set; }
        public int DesativadoPor {get;set;}
        public DateTime DesativadoEm { get; set; }
        public bool Ativo {get;set;} = true;
    }
}