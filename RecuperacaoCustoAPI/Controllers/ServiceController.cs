using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using RecuperacaoCustoAPI.Properties;

namespace RecuperacaoCustoAPI.Controllers
{
    public class ServiceController : ApiController
    {
        private Contexto db = new Contexto();
        [HttpGet]
        [Route("api/Base64/{texto}")]
        public string ConverteToBase64(string texto)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(texto));
        }

        [HttpGet]
        [Route("EnviarSenhas")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EnviaSenhas()
        {
            foreach (Usuario u in db.Usuario)
            {
                if (u.Email != null && !u.SenhaEnviada.Value)
                {
                    byte[] data = Convert.FromBase64String(u.Senha);
                    string senha = Encoding.UTF8.GetString(data);
                    SendEmail.Send(u.Email, "Acesso ao Sistema de Recuperação de Custos", string.Format(Resources.EnvioSenha, u.Login, senha, "http://10.200.51.46:8095"));
                }
            }
            return Ok();
        }
    }
}
