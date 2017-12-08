using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Repository.Interfaces
{
    public interface IAuthRepository
    {
        void AlteraSenha(string login, string novaSenha);
        bool Checkuser(string login, string senha);
        UsuarioDTO AlterarUsuario(UsuarioDTO usuario);
        string CriarUsuario(UsuarioDTO usuario);
        Usuario RecuperarSenha(string login);
    }
}
