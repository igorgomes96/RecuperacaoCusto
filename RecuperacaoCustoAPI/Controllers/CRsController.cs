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
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Filters;
using RecuperacaoCustoAPI.Services.Interfaces;
using RecuperacaoCustoAPI.Exceptions;

namespace RecuperacaoCustoAPI.Controllers
{
    [AuthenticationFilter]
    public class CRsController : ApiController
    {
        private readonly ICRService _crsService;

        public CRsController(ICRService crsService)
        {
            _crsService = crsService;
        }

        // GET: api/CRs
        public IEnumerable<CRDTO> GetCRs(string search = null)
        {
            return _crsService.List(search);
        }

        // GET: api/CRs/5
        [ResponseType(typeof(CRDTO))]
        public IHttpActionResult GetCR(string id)
        {
            try
            {
                return Ok(_crsService.Find(id));
            } catch (NaoEncontradoException<CR>)
            {
                return Content(HttpStatusCode.NotFound, "CR não encontrado!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST: api/CRs
        [ResponseType(typeof(CRDTO))]
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult PostCR(CRDTO cR)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                return CreatedAtRoute("DefaultApi", new { id = cR.Codigo }, _crsService.Save(cR));
            } catch (ConflitoException<CR>)
            {
                return Content(HttpStatusCode.Conflict, "CR já cadastrado!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE: api/CRs/5
        [ResponseType(typeof(CRDTO))]
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult DeleteCR(string id)
        {
            try
            {
                return Ok(_crsService.Delete(id));
            } catch (NaoEncontradoException<CR>)
            {
                return Content(HttpStatusCode.NotFound, "CR não encontrado!");
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}