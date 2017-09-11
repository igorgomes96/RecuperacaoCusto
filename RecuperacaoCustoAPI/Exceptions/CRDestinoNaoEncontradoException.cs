using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Exceptions
{
    public class CROrigemNaoEncontradoException : ExcelException
    {
        public string CR { get; set; }

        public CROrigemNaoEncontradoException(string cr, string aba, int linha) : base(aba, linha)
        {
            CR = cr;
        }
    }
}