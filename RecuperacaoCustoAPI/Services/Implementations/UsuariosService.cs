using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.Exceptions;
using System.Text;
using RecuperacaoCustoAPI.Service;
using System.Threading;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IGenericRepository<string, Usuario, UsuarioDTO> _userRep;
        private readonly IGenericRepository<string, CR, CRDTO> _crsRep;
        private readonly IAuthRepository _authRep;

        public UsuariosService(IGenericRepository<string, Usuario, UsuarioDTO> rep,
            IGenericRepository<string, CR, CRDTO> crsRep,
            IAuthRepository authRep)
        {
            _userRep = rep;
            _authRep = authRep;
            _crsRep = crsRep;
        }

        public void AlterarSenha(string login, AlteraSenhaDTO senha)
        {
            senha.SenhaAtual = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha.SenhaAtual));
            senha.NovaSenha = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha.NovaSenha));
            senha.Confirmacao = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha.Confirmacao));

            if (!_authRep.Checkuser(login, senha.SenhaAtual))
                throw new SenhaIncorretaException();

            _authRep.AlteraSenha(login, senha.NovaSenha);
        }

        public UsuarioDTO Delete(string login)
        {
            try { 
                return _userRep.Delete(login);
            } catch (Exception e)
            {
                ICollection<CRDTO> crs = GetCRsGestao(login);
                if (crs.Count > 0)
                {
                    string crsStr = crs.Select(x => x.Codigo).Aggregate((total, parte) => total + ", " + parte.Trim());
                    if (crs.Count == 1)
                        throw new GestorDeCRException("O usuário é gestor do seguinte CR: " + crsStr + ". Associe esse CR a outro usuário para então fazer a exclusão.");
                    else
                        throw new GestorDeCRException("O usuário é gestor dos seguintes CRs: " + crsStr + ". Associe esses CRs a outro(s) usuário(s) para então fazer a exclusão.");
                } else throw e;
            }
        }

        private ICollection<CRDTO> GetCRsGestao(string login)
        {
            return _crsRep.Query(x => x.ResponsavelLogin == login);

        }

        public UsuarioDTO Find(string login)
        {
            return _userRep.Find(login);
        }

        public ICollection<UsuarioDTO> List()
        {
            return _userRep.List();
        }

        public ICollection<UsuarioDTO> ListGestores()
        {
            return _userRep.Query(x => x.Perfil == "Gestor");
        }

        

        public void Save(UsuarioDTO usuario)
        {
            if (!_userRep.Existe(usuario.Login)) { 
                string senha = _authRep.CriarUsuario(usuario);
                EmailDTO email = new EmailDTO
                {
                    Subject = "Acesso ao Sistema de Recuperação de Custos e Transferência de Receita"
                };

                email.Message = string.Format(DateTime.Now.Hour < 12 ? "Bom dia" : (DateTime.Now.Hour < 19 ? "Boa tarde" : "Boa noite") + ", {0}.<br><br>" +
                        "Foi criado um usuário para seu acesso ao sistema de Recuperação de Custos e Transferência de Receita:<br>" +
                        "Usuário: <strong>" + usuario.Login + "</strong><br>" +
                        "Senha: <strong>" + senha + "</strong><br><br>" +
                        "Segue o link para acesso: <a href='{1}'>{1}</a><br><br>", usuario.Nome.Split(' ')[0], email.LinkSistema);

                ((List<string>)email.To).Add(usuario.Email);

                Thread th = new Thread(SendEmail.Send);
                th.Start(email);

            } else
                _authRep.AlterarUsuario(usuario);
        }

        public UsuarioDTO Update(UsuarioDTO usuario)
        {
            return _authRep.AlterarUsuario(usuario);
        }

        public void RecuperarSenha(string login)
        {
            Usuario usuario = _authRep.RecuperarSenha(login);
            string senha = Encoding.UTF8.GetString(Convert.FromBase64String(usuario.Senha));
            EmailDTO email = new EmailDTO
            {
                Subject = "Recuperação de Senha - Sistema de Recuperação de Custos e Transferência de Receita"
            };

            email.Message = string.Format(DateTime.Now.Hour < 12 ? "Bom dia" : (DateTime.Now.Hour < 19 ? "Boa tarde" : "Boa noite") + ", {0}.<br><br>" +
                    "Recuperamos suas informações de acesso:<br>" +
                    "Usuário: <strong>" + login + "</strong><br>" +
                    "Senha: <strong>" + senha + "</strong><br><br>" +
                    "Segue o link para acesso: <a href='{1}'>{1}</a><br><br>", usuario.Nome.Split(' ')[0], email.LinkSistema);

            ((List<string>)email.To).Add(usuario.Email);

            Thread th = new Thread(SendEmail.Send);
            th.Start(email);

        }
    }
}