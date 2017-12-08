using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Exceptions;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RecuperacaoCustoAPI.Controllers
{
    public class MesesBloqueadosTransfReceitaController : ApiController
    {
        private readonly IMesesBloqueadosService _mesesService;

        public MesesBloqueadosTransfReceitaController(IMesesBloqueadosService mesesService)
        {
            _mesesService = mesesService;
        }

        [Route("api/MesesBloqueadosTransfReceita/Anos")]
        [ResponseType(typeof(ICollection<int>))]
        public IHttpActionResult GetAnos()
        {
            return Ok(_mesesService.GetAnos());
        }

        [ResponseType(typeof(ICollection<MesBloqueadoTransfReceitaDTO>))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_mesesService.List());
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("api/MesesBloqueadosTransfReceita/Desbloquear")]
        public IHttpActionResult PostDesbloquear([FromBody]DateTime mes)
        {
            try
            {
                _mesesService.Delete(mes);
                return Ok();
            } catch (NaoEncontradoException<MesBloqueadoTransfReceita>)
            {
                return Content(HttpStatusCode.NotFound, "Mês bloqueado não encontrado!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public IHttpActionResult Post([FromBody]DateTime mes)
        {
            try
            {
                _mesesService.BloquearMes(mes);
                return Ok();
            } catch (ConflitoException<MesBloqueadoTransfReceita>)
            {
                return Content(HttpStatusCode.Conflict, "Esse mês já está bloqueado!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
