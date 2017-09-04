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
    
    public partial class RecuperacaoCusto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RecuperacaoCusto()
        {
            this.RecuperacoesMensais = new HashSet<RecuperacaoCustoMes>();
        }
    
        public string CROrigem { get; set; }
        public string CRDestino { get; set; }
        public System.DateTime DataHora { get; set; }
        public Nullable<bool> Aprovado { get; set; }
        public string Resposta { get; set; }
        public Nullable<System.DateTime> DataHoraAprovacao { get; set; }
        public string TipoRecuperacao { get; set; }
        public string Motivo { get; set; }
        public int Codigo { get; set; }
        public int CodCiclo { get; set; }
    
        public virtual CR Destino { get; set; }
        public virtual CR Origem { get; set; }
        public virtual Ciclo Ciclo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecuperacaoCustoMes> RecuperacoesMensais { get; set; }
    }
}