using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Exceptions
{
    public class TipoRecuperacaoNaoEncontradoException : ExcelException
    {
        public string Tipo { get; set; }

        public TipoRecuperacaoNaoEncontradoException(string tipo, string aba, int linha) : base(aba, linha)
        {
            Tipo = tipo;
        }
    }
}