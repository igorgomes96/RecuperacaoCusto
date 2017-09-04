using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class CicloDTO
    {
        public CicloDTO() {

        }
        public CicloDTO(Ciclo c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            Descricao = c.Descricao;
            DataInicio = c.DataInicio;
            DataFim = c.DataFim;
        }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public System.DateTime DataInicio { get; set; }
        public System.DateTime DataFim { get; set; }
    }
}