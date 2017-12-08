using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class UsuarioDTO
    {

        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
    }
}