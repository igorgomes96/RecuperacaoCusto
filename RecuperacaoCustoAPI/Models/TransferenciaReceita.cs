//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecuperacaoCustoAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransferenciaReceita
    {
        public int Codigo { get; set; }
        public string CROrigem { get; set; }
        public string CRDestino { get; set; }
        public string RegimeTributacao { get; set; }
        public float ISS { get; set; }
        public float Valor { get; set; }
        public string Intercompany { get; set; }
        public string NF { get; set; }
        public System.DateTime DataEmissao { get; set; }
        public string RazaoSocial { get; set; }
        public string Historico { get; set; }
        public System.DateTime DataHora { get; set; }
        public string LoginUsuario { get; set; }
        public float CPRB { get; set; }
    
        public virtual CR CROrigemObj { get; set; }
        public virtual CR CRDestinoObj { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
