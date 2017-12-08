using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Exceptions
{
    public class SenhaIncorretaException: Exception
    {
        public SenhaIncorretaException() : base("Senha incorreta!") { }
        public SenhaIncorretaException(string message) : base(message) { }
    }
}