using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Filters;
using RecuperacaoCustoAPI.DTO;
using System.Text;
using RecuperacaoCustoAPI.Service;
using RecuperacaoCustoAPI.Properties;

namespace RecuperacaoCustoAPI.Controllers
{
    [AuthenticationFilter]
    public class UsuariosController : ApiController
    {
        private Contexto db = new Contexto();

        [Authorize(Roles = "Administrador")]
        // GET: api/Usuarios
        public IEnumerable<UsuarioDTO> GetUsuarios()
        {
            return db.Usuario.ToList()
                    .Select(x => new UsuarioDTO(x));
        }

        //[HttpGet]
        //[Route("api/AlteraTodasSenhas")]
        //public void AlteraTodasSenhas()
        //{
        //    IDictionary<string, string> senhas = new Dictionary<string, string>();
        //    foreach (Usuario u in db.Usuario.ToList())
        //    {
        //        senhas.Add(u.Login, Convert.ToBase64String(Encoding.UTF8.GetBytes(u.Senha)));
        //    }

        //    foreach (string key in senhas.Keys)
        //    {
        //        db.Usuario.Find(key).Senha = senhas[key];
        //        db.SaveChanges();
        //    }

        //}

        //[HttpGet]
        //[Route("EnviarSenhas")]
        //[ResponseType(typeof(void))]
        //public IHttpActionResult EnviaSenhas()
        //{
        //    foreach (Usuario u in db.Usuario)
        //    {
        //        if (u.Email != null && !u.SenhaEnviada.Value)
        //        {
        //            byte[] data = Convert.FromBase64String(u.Senha);
        //            string senha = Encoding.UTF8.GetString(data);
        //            SendEmail.Send(u.Email, "Acesso ao Sistema de Recuperação de Custos", string.Format(Resources.EnvioSenha, u.Login, senha, "http://10.200.51.46:8095"));
        //        }
        //    }
        //    return Ok();
        //}

        // GET: api/Usuarios/5
        [ResponseType(typeof(UsuarioDTO))]
        public IHttpActionResult GetUsuario(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(new UsuarioDTO(usuario));
        }

        [HttpPut]
        [Route("api/Usuarios/AlteraSenha/{login}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlteraSenha (string login, AlteraSenhaDTO senha)
        {
            Usuario user = db.Usuario.Find(login);
            if (user == null) return NotFound();

            senha.SenhaAtual = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha.SenhaAtual));
            senha.NovaSenha = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha.NovaSenha));
            senha.Confirmacao = Convert.ToBase64String(Encoding.UTF8.GetBytes(senha.Confirmacao));

            if (user.Senha != senha.SenhaAtual)
                return BadRequest("Senha incorreta!");

            if (senha.NovaSenha != senha.Confirmacao)
                return BadRequest("As senhas não correspondem!");

            user.Senha = senha.NovaSenha;

            try
            {
                db.SaveChanges();
            } catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(string id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Login)
            {
                return BadRequest();
            }

            Usuario user = db.Usuario.Find(usuario.Login);
            if (user == null) return NotFound();

            usuario.Senha = user.Senha;
            db.Entry(user).State = EntityState.Detached;
            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(UsuarioDTO))]
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuario.Add(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.Login))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = usuario.Login }, new UsuarioDTO(usuario));
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(UsuarioDTO))]
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult DeleteUsuario(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            UsuarioDTO u = new UsuarioDTO(usuario);
            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return Ok(u);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(string id)
        {
            return db.Usuario.Count(e => e.Login == id) > 0;
        }
    }
}