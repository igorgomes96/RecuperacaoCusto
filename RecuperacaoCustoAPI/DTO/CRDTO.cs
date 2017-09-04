using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class CRDTO
    {
        public CRDTO() { }
        public CRDTO(CR cr)
        {
            if (cr == null) return;
            Codigo = cr.Codigo;
            Descricao = cr.Descricao;
            ResponsavelLogin = cr.ResponsavelLogin;
        }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string ResponsavelLogin { get; set; }
    }
}