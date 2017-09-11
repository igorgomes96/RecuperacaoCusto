using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Exceptions
{
    public class CRDestinoNaoEncontradoException : ExcelException
    {
        public string CR { get; set; }

        public CRDestinoNaoEncontradoException(string cr, string aba, int linha) : base(aba, linha)
        {
            CR = cr;
        }
    }
}