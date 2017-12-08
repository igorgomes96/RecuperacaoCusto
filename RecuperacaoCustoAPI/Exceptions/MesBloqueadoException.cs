using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Exceptions
{
    public class MesBloqueadoException : Exception
    {
        public MesBloqueadoException() : base("Mês de referência bloqueado!") { }
        public MesBloqueadoException(string message) : base(message) { }
    }
}