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
using RecuperacaoCustoAPI.Services.Interfaces;
using RecuperacaoCustoAPI.Exceptions;

namespace RecuperacaoCustoAPI.Controllers
{
    [AuthenticationFilter]
    public class UsuariosController : ApiController
    {

        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [Authorize(Roles = "Administrador")]
        // GET: api/Usuarios
        public IEnumerable<UsuarioDTO> GetUsuarios()
        {
            return _usuariosService.List();
        }

        [Authorize(Roles = "Administrador")]
        [Route("api/Usuarios/Gestores")]
        public IHttpActionResult GetGestores()
        {
            try
            {
                return Ok(_usuariosService.ListGestores());
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // GET: api/Usuarios/5
        [ResponseType(typeof(UsuarioDTO))]
        public IHttpActionResult GetUsuario(string id)
        {
            try
            {
                return Ok(_usuariosService.Find(id));
            } catch (NaoEncontradoException<Usuario>)
            {
                return Content(HttpStatusCode.NotFound, "Usuário não encontrado!");
            } catch (Exception e) {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("api/Usuarios/AlteraSenha/{login}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlteraSenha (string login, AlteraSenhaDTO senha)
        {
            if (senha.NovaSenha != senha.Confirmacao)
                return BadRequest("As senhas não correspondem!");

            try
            {
                _usuariosService.AlterarSenha(login, senha);
                return Ok();
            } catch (SenhaIncorretaException)
            {
                return Content(HttpStatusCode.BadRequest, "Senha incorreta!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(string id, UsuarioDTO usuario)
        {
            if (id != usuario.Login)
                return Content(HttpStatusCode.BadRequest, "Parâmetros incosistentes!");

            try
            {
                _usuariosService.Update(usuario);
                return Ok();
            } catch(NaoEncontradoException<Usuario>)
            {
                return Content(HttpStatusCode.NotFound, "Usuário não encontrado!");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST: api/Usuarios
        [ResponseType(typeof(UsuarioDTO))]
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult PostUsuario(UsuarioDTO usuario)
        {
            try
            {
                _usuariosService.Save(usuario);
                return Ok();
            } catch (ConflitoException<Usuario>)
            {
                return Content(HttpStatusCode.Conflict, "Já existe um usuário cadastrado com esse mesmo login!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        [ResponseType(typeof(void))]
        [Authorize(Roles = "Administrador")]
        [Route("api/Usuarios/{login}/RecuperarSenha")]
        public IHttpActionResult PostRecuperarSenha(string login)
        {
            try
            {
                _usuariosService.RecuperarSenha(login);
                return Ok();
            } catch (NaoEncontradoException<Usuario>)
            {
                return Content(HttpStatusCode.NotFound, "Usuário não encontrado!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(UsuarioDTO))]
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult DeleteUsuario(string id)
        {
            try
            {
                return Ok(_usuariosService.Delete(id));
            } catch (NaoEncontradoException<Usuario>)
            {
                return Content(HttpStatusCode.NotFound, "Usuário não cadastrado!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}