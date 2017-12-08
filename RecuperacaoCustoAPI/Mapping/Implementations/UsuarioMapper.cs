using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class UsuarioMapper : ISingleMapper<Usuario, UsuarioDTO>
    {
        public UsuarioDTO Map(Usuario source)
        {
            return source == null ? null : new UsuarioDTO
            {
                Login = source.Login,
                Nome = source.Nome,
                Email = source.Email,
                Perfil = source.Perfil
            };
        }

        public Usuario Map(UsuarioDTO destination)
        {
            return destination == null ? null : new Usuario
            {
                Login = destination.Login,
                Nome = destination.Nome,
                Email = destination.Email,
                Perfil = destination.Perfil
            };
        }
    }
}