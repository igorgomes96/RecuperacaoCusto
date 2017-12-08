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
    
    public partial class CR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CR()
        {
            this.RecuperacoesDestino = new HashSet<RecuperacaoCusto>();
            this.RecuperacoesOrigem = new HashSet<RecuperacaoCusto>();
            this.TransfReceitaOrigem = new HashSet<TransferenciaReceita>();
            this.TransfReceitaDestino = new HashSet<TransferenciaReceita>();
        }
    
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string ResponsavelLogin { get; set; }
        public string Categoria { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecuperacaoCusto> RecuperacoesDestino { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecuperacaoCusto> RecuperacoesOrigem { get; set; }
        public virtual Usuario Responsavel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferenciaReceita> TransfReceitaOrigem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferenciaReceita> TransfReceitaDestino { get; set; }
        public virtual CategoriaCR CategoriaCR { get; set; }
    }
}
