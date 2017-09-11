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
            Nome = user.Nome;
            Email = user.Email;
            Perfil = user.Perfil;
        }

        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
    }
}