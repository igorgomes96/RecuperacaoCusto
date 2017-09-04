using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class MesCicloDTO
    {
        public MesCicloDTO() { }
        public MesCicloDTO(MesCiclo m)
        {
            if (m == null) return;
            Codigo = m.Codigo;
            Mes = m.Mes;
            CodCiclo = m.CodCiclo;
        }
        public int Codigo { get; set; }
        public System.DateTime Mes { get; set; }
        public int CodCiclo { get; set; }
    }
}