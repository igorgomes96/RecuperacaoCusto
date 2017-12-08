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
    public class CategoriasCRController : ApiController
    {
        private readonly ICategoriaCRService _categoriasService;
        

        public CategoriasCRController(ICategoriaCRService categoriasService)
        {
            _categoriasService = categoriasService;
        }

        [ResponseType(typeof(ICollection<CategoriaCRDTO>))]
        public IHttpActionResult Get()
        {
            return Ok(_categoriasService.List());
        }

        [ResponseType(typeof(CategoriaCRDTO))]
        public IHttpActionResult Post(CategoriaCRDTO categoria)
        {
            try
            {
                return Ok(_categoriasService.Save(categoria));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [ResponseType(typeof(CategoriaCRDTO))]
        public IHttpActionResult Delete(string categoria)
        {
            try
            {
                return Ok(_categoriasService.Delete(categoria));
            }
            catch (NaoEncontradoException<CategoriaCR>)
            {
                return Content(HttpStatusCode.NotFound, "Categoria não encontrada!");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



    }
}
