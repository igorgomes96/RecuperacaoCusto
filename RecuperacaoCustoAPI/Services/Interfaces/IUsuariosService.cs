using RecuperacaoCustoAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Services.Interfaces
{
    public interface IUsuariosService
    {

        ICollection<UsuarioDTO> List();
        ICollection<UsuarioDTO> ListGestores();
        UsuarioDTO Find(string login);
        UsuarioDTO Delete(string login);
        /// <summary>
        /// Cria o usuário, com uma senha aleatória, e retorna a senha
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        void Save(UsuarioDTO usuario);
        UsuarioDTO Update(UsuarioDTO usuario);
        void AlterarSenha(string login, AlteraSenhaDTO senha);
        void RecuperarSenha(string login);


    }
}
