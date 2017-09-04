using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class UsuarioDTO
    {
        public UsuarioDTO() { }
        public UsuarioDTO(Usuario user)
        {
            if (user == null) return;
            Login = user.Login;
            Senha = user.Senha;
            Nome = user.Nome;
            Email = user.Email;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}