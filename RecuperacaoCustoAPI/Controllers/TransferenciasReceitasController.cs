using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Exceptions;
using RecuperacaoCustoAPI.Filters;
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
    [AuthenticationFilter]
    public class TransferenciasReceitasController : ApiController
    {
        private readonly ITransferenciaReceitaService _transferenciaReceitaService;

        public TransferenciasReceitasController(ITransferenciaReceitaService transferenciaReceitaService)
        {
            _transferenciaReceitaService = transferenciaReceitaService;
        }

        [ResponseType(typeof(IEnumerable<TransferenciaReceitaDTO>))]
        public IHttpActionResult Get()
        {
            return Ok(_transferenciaReceitaService.List(x => x.LoginUsuario == User.Identity.Name));
        }

        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_transferenciaReceitaService.Find(id));
            } catch (NaoEncontradoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Post(TransferenciaReceitaDTO transf)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, ModelState);

            try
            {
                transf.LoginUsuario = User.Identity.Name;
                return Ok(_transferenciaReceitaService.Save(transf));
            } catch (ConflitoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.Conflict, e.Message);
            }
            catch (NaoEncontradoException<string> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Put(int id, TransferenciaReceitaDTO transf)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, ModelState);

            if (transf.Codigo != id)
                return Content(HttpStatusCode.BadRequest, "Inconsistência de parâmetro!");

            try
            {
                return Ok(_transferenciaReceitaService.Update(transf));
            } catch (NaoEncontradoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(_transferenciaReceitaService.Delete(id));
            }
            catch (NaoEncontradoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
