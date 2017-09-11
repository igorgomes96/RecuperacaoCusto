using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class AlteraSenhaDTO
    {
        public AlteraSenhaDTO () { }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string Confirmacao { get; set; }
    }
}