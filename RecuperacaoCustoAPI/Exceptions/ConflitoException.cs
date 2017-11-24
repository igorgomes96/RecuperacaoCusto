using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Exceptions
{
    public class ConflitoException<T>: Exception
    {
        public ConflitoException() : base(typeof(T) + " já cadastrado!") { }
    }
}