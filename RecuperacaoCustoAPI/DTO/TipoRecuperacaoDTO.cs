using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class TipoRecuperacaoDTO
    {
        public TipoRecuperacaoDTO() { }
        public TipoRecuperacaoDTO(TipoRecuperacao t) {
            if (t == null) return;
            Codigo = t.Codigo;
            Tipo = t.Tipo;
            Conta = t.Conta;
        }
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public string Conta { get; set; }
    }
}