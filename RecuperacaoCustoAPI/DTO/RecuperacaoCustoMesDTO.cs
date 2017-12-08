using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class RecuperacaoCustoMesDTO
    {
        public int CodRecuperacao { get; set; }
        public int CodMesCiclo { get; set; }
        public float Valor { get; set; }

        public MesCicloDTO Mes { get; set; }
    }
}