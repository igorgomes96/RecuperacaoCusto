using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Exceptions
{
    public class GestorDeCRException : Exception
    {
        public GestorDeCRException() : base("O usuário atualmente é gestor de algum(ns) CR(s)!") { }
        public GestorDeCRException(string message) : base(message) { }
    }
}