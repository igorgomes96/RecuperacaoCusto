using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class CicloMesesDTO
    {
        public CicloMesesDTO()
        {
            Meses = new HashSet<MesCicloDTO>();
        }
        public CicloMesesDTO(Ciclo c)
        {
            if (c == null) return;
            Codigo = c.Codigo;
            Descricao = c.Descricao;
            DataInicio = c.DataInicio;
            DataFim = c.DataFim;
            if (c.MesesCiclos != null)
            {
                Meses = new HashSet<MesCicloDTO>();
                Meses = c.MesesCiclos.Select(x => new MesCicloDTO(x));
            }
        }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public System.DateTime DataInicio { get; set; }
        public System.DateTime DataFim { get; set; }
        public IEnumerable<MesCicloDTO> Meses { get; set; }
    }
}