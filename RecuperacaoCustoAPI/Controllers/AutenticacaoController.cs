using RecuperacaoCustoAPI.Filters;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace RecuperacaoCustoAPI.Controllers
{
    public class AutenticacaoController : ApiController
    {
        [HttpPost]
        [Route("api/Autentica")]
        [AllowAnonymous]
        public IHttpActionResult Login(Usuario usuario)
        {
            if (usuario == null || usuario.Login == null || usuario.Senha == null) return BadRequest();

            Contexto db = new Contexto();
            Usuario user = db.Usuario.Find(usuario.Login);

            if (user == null) return BadRequest("Usuário não cadastrado!");
            if (user.Senha != Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario.Senha))) return BadRequest("Senha incorreta!");

            string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(usuario.Login + ":" + usuario.Senha));

            //Verifica se já existe a sessão. Se sim, aumenta a data de expiração, caso contrário, cria uma nova sessão
            Sessao s = db.Sessao.Find(token);
            if (s == null)
                db.Sessao.Add(new Sessao { Chave = token, LoginUsuario = user.Login, Inicio = DateTime.Now, Fim = DateTime.Now.AddHours(1.0) });
            else
                s.Fim = DateTime.Now.AddHours(1.0);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            var retorno = new {
                Token = token,
                Usuario = user.Nome,
                Login = user.Login,
                Perfil = user.Perfil
            };

            db.Dispose();

            return Ok(retorno);
        }

        [HttpPost]
        [Route("api/Logout")]
        [AuthenticationFilter]
        public IHttpActionResult Logout()
        {
            Contexto db = new Contexto();
            AuthenticationHeaderValue authorization = Request.Headers.Authorization;
            if (authorization != null)
            {
                if (authorization.Scheme == "Basic")
                {
                    Sessao s = db.Sessao.Find(authorization.Parameter);
                    if (s != null)
                        db.Sessao.Remove(s);
                    db.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
