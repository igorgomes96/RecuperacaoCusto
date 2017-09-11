using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class RecuperacaoCustoDTO
    {
        public RecuperacaoCustoDTO() { }
        public RecuperacaoCustoDTO(RecuperacaoCusto rec)
        {
            if (rec == null) return;
            CRDestino = rec.CRDestino;
            CRDestino = rec.CRDestino;
            DataHora = rec.DataHora;
            Aprovado = rec.Aprovado;
            Resposta = rec.Resposta;
            DataHoraResposta = rec.DataHoraResposta;
            TipoRecuperacaoCod = rec.TipoRecuperacaoCod;
            Motivo = rec.Motivo;
            CodCiclo = rec.CodCiclo;
            if (rec.TipoRecuperacao != null)
            {
                Tipo = rec.TipoRecuperacao.Tipo;
                Conta = rec.TipoRecuperacao.Conta;
            }
        }

        public string CROrigem { get; set; }
        public string CRDestino { get; set; }
        public System.DateTime DataHora { get; set; }
        public Nullable<bool> Aprovado { get; set; }
        public string Resposta { get; set; }
        public Nullable<System.DateTime> DataHoraResposta { get; set; }
        public int TipoRecuperacaoCod { get; set; }
        public string Tipo { get; set; }
        public string Conta { get; set; }
        public string Motivo { get; set; }
        public int Codigo { get; set; }
        public int CodCiclo { get; set; }
    }
}