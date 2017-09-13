using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace RecuperacaoCustoAPI.Controllers
{
    public class ServiceController : ApiController
    {
        [HttpGet]
        [Route("api/Base64/{texto}")]
        public string ConverteToBase64(string texto)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(texto));
        }
    }
}
