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
    
    public partial class MesCiclo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MesCiclo()
        {
            this.Recuperacoes = new HashSet<RecuperacaoCustoMes>();
        }
    
        public int Codigo { get; set; }
        public System.DateTime Mes { get; set; }
        public int CodCiclo { get; set; }
    
        public virtual Ciclo Ciclo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecuperacaoCustoMes> Recuperacoes { get; set; }
    }
}
